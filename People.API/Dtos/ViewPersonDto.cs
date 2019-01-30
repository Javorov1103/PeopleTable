using System.Collections.Generic;

namespace People.API.Dtos
{
    public class ViewPersonDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        
        public string EGN { get; set; }

        
        public decimal Height { get; set; }

        
        public decimal Weight { get; set; }

        public IList<string> Countries { get; set; }
    }
}