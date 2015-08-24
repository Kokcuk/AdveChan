using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdveChan.ViewModels
{
    public class AddPostModel
    {
        public int ThreadId { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Imgsrc { get; set; }
    }
}