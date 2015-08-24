using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Models
{
    public sealed class Thread:EntityBase
    {
        public int BoardId { get; set; }
        public Board Board { get; set; }
        public List<Post> Posts { get; set; }
    }
}