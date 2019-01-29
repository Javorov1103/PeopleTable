using System.Collections.Generic;
using People.API.Models;

namespace People.API.Data.Contracts
{
    public interface ICountryRepo
    {
         IEnumerable<Country> GetAll();
    }
}