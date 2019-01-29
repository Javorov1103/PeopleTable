using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using People.API.Models;

namespace People.API.Dtos
{
    public class CreatePersonDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [RegularExpression("\\d{10}")]
        public string EGN { get; set; }

        [Required]
        public decimal Height { get; set; }

        [Required]
        public decimal Weight { get; set; }

        public string Countries { get; set; }
    }
}