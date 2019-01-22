using System.ComponentModel.DataAnnotations;

namespace People.API.Dtos
{
    public class CreatePersonDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string EGN { get; set; }

        public decimal Height { get; set; }
    }
}