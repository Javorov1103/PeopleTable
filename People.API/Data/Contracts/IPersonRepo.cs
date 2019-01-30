using People.API.Models;
using People.API.Dtos;
using System.Collections.Generic;

namespace People.API.Data.Contracts
{
    public interface IPersonRepo
    {
         IEnumerable<ViewPersonDto> GetAll();

         void Create(CreatePersonDto model);

         void Delete(int id);

         void Edit(EditPersonDto model);
    }
}