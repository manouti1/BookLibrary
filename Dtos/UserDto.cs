using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Dtos
{
    public class UserDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
