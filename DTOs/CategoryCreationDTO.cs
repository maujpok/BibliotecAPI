using System.ComponentModel.DataAnnotations;

namespace BibliotecAPI.DTOs
{
    public class CategoryCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
