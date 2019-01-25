using System.Collections.Generic;
using System.Data.SqlClient;
using People.API.Data.Contracts;
using People.API.Models;
using Dapper;
using People.API.Dtos;

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
            string sql = "INSERT INTO Person (FirstName, LastName, EGN, Height, Weight) VALUES (@FirstName, @LastName, @EGN, @Height, @Weight)";
            
           using(var connection= new SqlConnection(connectionString))
           {
               connection.Execute(sql, new {FirstName = model.FirstName,LastName = model.LastName,
               EGN = model.EGN, Height =model.Height, Weight = model.Weight
               });
           }
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
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