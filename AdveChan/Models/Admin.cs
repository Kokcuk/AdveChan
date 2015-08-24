using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Models
{
    public sealed class Admin:EntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}