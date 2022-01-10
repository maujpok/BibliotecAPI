using System.ComponentModel.DataAnnotations;

namespace BibliotecAPI.DTOs
{
    public class EditorialCreationDTO
    {
        [Required]
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}