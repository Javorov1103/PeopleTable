using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using People.API.Data.Contracts;
using People.API.Models;

namespace People.API.Data
{
    public class CountryRepo : ICountryRepo
    {
        private readonly string connectionString;

        public CountryRepo(string connectionString)
        {
            this.connectionString = connectionString;
        }

        
        public IEnumerable<Country> GetAll()
        {
            IEnumerable<Country> coutries = null;

            using(var connection = new SqlConnection(connectionString))
            {
                coutries = connection.Query<Country>("SELECT Id, CountryName FROM Countries");
            }

            return coutries;
        }
    }
}