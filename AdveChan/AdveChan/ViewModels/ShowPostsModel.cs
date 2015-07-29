using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdveChan.Models;

namespace AdveChan.ViewModels
{
    public class ShowPostsModel
    {
        public int ThreadId { get; set; }
        public List<Post> Posts { get; set; } 
    }
}