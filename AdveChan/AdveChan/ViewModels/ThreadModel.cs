using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdveChan.Models;
using AdveChan.Projections;

namespace AdveChan.ViewModels
{
    public class ThreadModel
    {
        public List<ThreadWithPosts> Threads { get; set; }
        public string BoardsName { get; set; }
    }
}