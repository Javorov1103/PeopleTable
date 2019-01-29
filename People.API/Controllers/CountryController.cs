using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using People.API.Data.Contracts;
using People.API.Models;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepo repo;
        public CountryController(ICountryRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            return this.repo.GetAll().ToList();
        }
    }
}