using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Models
{
    public sealed class Ban:EntityBase
    {
        public string IpAdress { get; set; }
        public DateTime Terms { get; set; }
    }
}