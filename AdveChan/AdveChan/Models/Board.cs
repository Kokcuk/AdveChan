using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Models
{
    public sealed class Board:EntityBase
    {
        public string Name { get; set; }
        public List<Thread> Threads { get; set; } 
    }
}