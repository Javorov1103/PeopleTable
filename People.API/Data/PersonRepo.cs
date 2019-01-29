using System.Collections.Generic;
using System.Data.SqlClient;
using People.API.Data.Contracts;
using People.API.Models;
using Dapper;
using People.API.Dtos;
using Newtonsoft.Json;
using System.Linq;

namespace People.API.Data
{
    public class PersonRepo : IPersonRepo
    {
        private readonly string connectionString;

        public PersonRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Create(CreatePersonDto model)
        {
            string sqlInsertPerson = @"INSERT INTO Person (FirstName, LastName, EGN, Height, Weight) 
                            VALUES (@FirstName, @LastName, @EGN, @Height, @Weight) 
                            SELECT CAST(SCOPE_IDENTITY() as int)";

            string sqlInsertPersonsCountries = @"INSERT INTO PersonsCountries VALUES(@PersonId,@CountryId)";

            Country[] countries = null;
            
                // If Model's countries are not null we deserialize them to an Array of Countries
            if (model.Countries != null) {
                    countries = JsonConvert.DeserializeObject<Country[]>(model.Countries);
            }
            


           using(var connection= new SqlConnection(connectionString))
           {
               var createdUserId = connection.Query<int>(sqlInsertPerson, new {FirstName = model.FirstName,LastName = model.LastName,
               EGN = model.EGN, Height =model.Height, Weight = model.Weight
               }).Single();

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

            using(var connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, new {
                    FirstName = model.FirstName,LastName = model.LastName,
                    EGN = model.EGN, Height =model.Height, Weight = model.Weight, Id = model.Id
                });
            }
        }

        //Get all people from db
        public IEnumerable<Person> GetAll()
        {
            IEnumerable<Person> people = null;

            using(var connection = new SqlConnection(connectionString))
            {
                people = connection.Query<Person>("SELECT id, FirstName,LastName,EGN,Height,Weight FROM Person");
            }

            return people;
        }
    }
}