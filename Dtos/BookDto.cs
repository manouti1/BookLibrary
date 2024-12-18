using BookLibrary.Domain;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Dtos
{
    public class BookDto
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }
        [Required]
        [MinLength(1)]
        public string Author { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
