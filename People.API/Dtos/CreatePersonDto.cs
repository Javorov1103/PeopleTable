using System.ComponentModel.DataAnnotations;

namespace People.API.Dtos
{
    public class CreatePersonDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string EGN { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public decimal Weight { get; set; }
    }
}