using System.ComponentModel.DataAnnotations;

namespace Test.Models
{
    public class LoginPage
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
