using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace MVC_Chat.Models
{
    public class User
    {
        [Key]
        [Required(ErrorMessage = "Input your login")]
        public string Nickname { get; set; }

        [Required(ErrorMessage = "Input your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Input your e-mail")]
        [RegularExpression(@".+\@.+\..+", ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; }
    }
}