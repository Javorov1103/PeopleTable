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
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(int id)
        {
            throw new System.NotImplementedException();
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