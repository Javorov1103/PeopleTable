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

        // Get all countries from Db
        public IEnumerable<Country> GetAll()
        {
            IEnumerable<Country> countries = null;

            using(var connection = new SqlConnection(connectionString))
            {
                countries = connection.Query<Country>("SELECT Id, CountryName FROM Countries");
            }

            return countries;
        }
    }
}