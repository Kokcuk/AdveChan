using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.Models
{
    public sealed class Post:EntityBase
    {
        public Thread Thread { get; set; }
        public int ThreadId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public DateTime Time { get; set; }
        public string ImagesUrl { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
    }
}