using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using People.API.Data.Contracts;
using People.API.Dtos;
using People.API.Models;

namespace People.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepo repo;

        public PersonController(IPersonRepo repo)
        {
            this.repo = repo;
        }

        // Return all people from the Data Base via repo
        [HttpGet]
        public ActionResult<IEnumerable<Person>> GetAll()
        {
            return repo.GetAll().ToList();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreatePersonDto model) {
            this.repo.Create(model);

            return StatusCode(201);
        }
    }
}