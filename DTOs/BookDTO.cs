using BibliotecAPI.Entities;

namespace BibliotecAPI.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Publication_Year { get; set; }
        public int Pages_Number { get; set; }


        public AuthorEntity Author { get; set; }
        //public int AuthorId { get; set; }

        public CategoryEntity Category { get; set; }
        //public int CategoryId { get; set; }

        public EditorialEntity Editorial { get; set; }
        //public int EditorialId { get; set; }
    }
}