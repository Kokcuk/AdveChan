namespace AdveChan.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [Required]
        [MinLength(3)]
        public string Login { get; set; }

        [Required]
        [MinLength(3)]
        public string Password { get; set; }
    }
}