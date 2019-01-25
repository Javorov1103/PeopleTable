using AutoMapper;
using People.API.Dtos;
using People.API.Models;

namespace People.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        // Fill our bindings 
        public AutoMapperProfiles()
        {
            CreateMap<CreatePersonDto,Person>();
            CreateMap<EditPersonDto,Person>();
        }
    }
}