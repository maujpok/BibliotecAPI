using System.ComponentModel.DataAnnotations;

namespace BibliotecAPI.DTOs
{
    public class AuthorCreationDTO
    {
        [Required]
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string BirthCity {  get; set; }

    }
}