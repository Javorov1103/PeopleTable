using System.ComponentModel.DataAnnotations;

namespace People.API.Dtos
{
    public class EditPersonDto
    {
        [Required]
        public int Id { get; set; }

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

        public string[] Countries { get; set; }
    }
}