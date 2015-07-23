using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdveChan.Models;

namespace AdveChan.Controllers
{
    public class TestController : Controller
    {
        private readonly ChanContext _chanContext;

        public TestController()
        {
            _chanContext = new ChanContext();
        }

        public ActionResult Seed()
        {
            var board = new Board
            {
                Name = "b",
                Threads = new List<Thread>()
            };
            _chanContext.Boards.Add(board);

            var thread = new Thread
            {
                Board = board,
                Posts = new List<Post>()
            };
            _chanContext.Threads.Add(thread);

            for (var i = 0; i < 10; i++)
            {
                var post = new Post
                {
                    Email = "AllahAkbar@hui",
                    Thread = thread,
                    Time = DateTime.Now,
                    Title = "pidor",
                    Content = "ALLAHAKBARALLAHABAR"
                };
                _chanContext.Posts.Add(post);
            }
            _chanContext.SaveChanges();

            return null;
        }
    }
}