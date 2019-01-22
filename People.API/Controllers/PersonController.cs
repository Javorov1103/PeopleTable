using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using People.API.Data.Contracts;
using People.API.Models;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController
    {
        private readonly IPersonRepo repo;

        public PersonController(IPersonRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return repo.GetAll().ToList();
        }
    }
}