using AutoMapper;
using People.API.Dtos;
using People.API.Models;

namespace People.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CreatePersonDto,Person>();
        }
    }
}