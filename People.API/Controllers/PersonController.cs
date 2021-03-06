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

        //Controller for Person Object - CRUD methods below
        public PersonController(IPersonRepo repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ViewPersonDto>> GetAll()
        {
            return repo.GetAll().ToList();
        }

        [HttpPost("create")]
        public IActionResult Create(CreatePersonDto model) 
        {
            this.repo.Create(model);

            return StatusCode(200);
        }

        [HttpPut("{id}")]
        public IActionResult Edit(int id,EditPersonDto model) 
        {
            this.repo.Edit(model);

            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            this.repo.Delete(id);

            return StatusCode(202);
        }
    }
}