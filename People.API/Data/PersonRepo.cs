using System.Collections.Generic;
using System.Data.SqlClient;
using People.API.Data.Contracts;
using People.API.Models;
using Dapper;
using People.API.Dtos;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace People.API.Data
{
    public class PersonRepo : IPersonRepo
    {
        private readonly string connectionString;

        public PersonRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }


        // Create a Person in db
        public void Create(CreatePersonDto model)
        {
            string sqlInsertPerson = @"INSERT INTO Person (FirstName, LastName, EGN, Height, Weight) 
                            VALUES (@FirstName, @LastName, @EGN, @Height, @Weight) 
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            string sqlInsertPersonsCountries = @"INSERT INTO PersonsCountries VALUES(@PersonId,@CountryId)";

            Country[] countries = null;

                // If Model's countries are not null we deserialize them to an Array of Countries
            if (model.Countries != null) 
            {
                    countries = JsonConvert.DeserializeObject<Country[]>(model.Countries);
            }
            
           using(var connection= new SqlConnection(connectionString))
           {
               var createdUserId = connection.Query<int>(sqlInsertPerson, new {FirstName = model.FirstName,LastName = model.LastName,
               EGN = model.EGN, Height =model.Height, Weight = model.Weight}).Single();

            // If countries are not null we add them to the DB
            if(countries !=null)
            {
                for(int i =0; i<countries.Length; i++)
                {
                   connection.Execute(sqlInsertPersonsCountries, new{PersonId = createdUserId, CountryId = countries[i].Id});
                }
            }
              
           }
        }


        // Delete a Person object from the Db
        // After the extended Db with many to many relationship between Persons and Countries,
        // To make the Delete method work => we add "On Delete Cascade" to the PersonsCountries table
        public void Delete(int id)
        {
            string sql = "DELETE FROM Person WHERE Id = @Id;";

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new {Id = id});
            }
        }


        //Edit concrete person
        public void Edit(EditPersonDto model)
        {
            string sql = @"UPDATE Person
            SET FirstName = @FirstName, LastName = @LastName, EGN = @EGN, Height = @Height, Weight = @Weight
            WHERE Id = @Id;";

            var visitedCountries = model.Countries;

            if (visitedCountries != null)
            {
                // Clean up our strings from ' " '
                for(int i =0; i<visitedCountries.Length;i++)
                    {
                        visitedCountries[i] = visitedCountries[i].Replace("\"", string.Empty);
                    }
            }

            // Get all the countries all from DB
            IEnumerable<Country> countries = null;

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new {
                    FirstName = model.FirstName,LastName = model.LastName,
                    EGN = model.EGN, Height =model.Height, Weight = model.Weight, Id = model.Id
                });

                countries = connection.Query<Country>("SELECT Id, CountryName FROM Countries");
            }
            
            // Get the ids of the countries that the user marked as visited
            IList<int> countriesId = new List<int>();


            if (visitedCountries != null)
            {
                for(int i =0; i<visitedCountries.Length;i++)
                {
                countriesId.Add(countries.FirstOrDefault(c => c.CountryName == visitedCountries[i]).Id);
                } 
            }
            

            // If there are no visited countries we break the method
            // But in case that the user just unchecked all the visited countries we execute this query
            if(countriesId.Count() == 0)
            {
                string sqlDelete = "DELETE FROM PersonsCountries WHERE Id_Person =@Id_Person;";
                using(var connection = new SqlConnection(connectionString))
                    {
                        connection.Execute(sqlDelete, new {Id_Person=model.Id});
                    }

                return;
            }

            // If there are marked visited countries we create a string builder to create our query

            StringBuilder sqlUpdateJunctionTable = new StringBuilder();
            sqlUpdateJunctionTable.Append(@"DELETE FROM PersonsCountries WHERE Id_Person =@Id_Person;
                INSERT INTO PersonsCountries (Id_Person, Id_Country) VALUES ");

            for(int i =0; i<countriesId.Count();i++)
            {
                sqlUpdateJunctionTable.Append($"(@Id_Person, {countriesId[i]})");
                if(i != countriesId.Count()-1) {
                    sqlUpdateJunctionTable.Append(",");
                }
            }

            string sqlUpdate = sqlUpdateJunctionTable.ToString();

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sqlUpdate, new {Id_Person=model.Id});
            }
        }

        //Get all people from db
        public IEnumerable<ViewPersonDto> GetAll()
        {
            IEnumerable<ViewPersonDto> people = null;
            IEnumerable<PeopleVisitedCountriesDto> peopleWithVisitedCountries = null;

            using(var connection = new SqlConnection(connectionString))
            {
                people = connection.Query<ViewPersonDto>("SELECT id, FirstName,LastName,EGN,Height,Weight FROM Person");
            
                peopleWithVisitedCountries = connection.Query<PeopleVisitedCountriesDto>(
                @"SELECT Person.Id, Countries.CountryName
                FROM Countries
                INNER JOIN PersonsCountries ON Countries.Id = PersonsCountries.Id_Country 
                INNER JOIN Person ON PersonsCountries.Id_Person = Person.Id");
            }

            foreach(var element in peopleWithVisitedCountries)
            {
                if (people.Any(x => x.Id == element.Id))
                {
                    if (people.FirstOrDefault(x=>x.Id == element.Id).Countries == null)
                    {
                        people.FirstOrDefault(x=>x.Id == element.Id).Countries = new List<string>();
                    }
                    people.FirstOrDefault(x=>x.Id == element.Id).Countries.Add(element.CountryName);
                }
            }

            return people;
        }
    }
}