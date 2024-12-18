using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Domain
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}

