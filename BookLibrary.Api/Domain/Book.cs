using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain
{
    public class Book
    {
        public int Id { get; set; }
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
