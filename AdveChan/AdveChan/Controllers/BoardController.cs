using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdveChan.Models;
using AdveChan.ViewModels;
using PagedList;

namespace AdveChan.Controllers
{
    using Projections;

    public class BoardController : Controller
    {
        private readonly ChanContext _chanContext;

        public BoardController()
        {
            _chanContext = new ChanContext();
        }

        public ActionResult ShowThreads(int id)
        {
            TempData["BoardId"] = id;
            List<ThreadWithPosts> threadsWithPost = _chanContext.Threads
                .Where(x => x.BoardId == id).Select(x => new ThreadWithPosts
                {
                    Id = x.Id,
                    LastPosts = x.Posts.OrderByDescending(p => p.Time).Take(3).ToList(),
                    OpPost = x.Posts.OrderBy(p => p.Time).Take(1).FirstOrDefault(),
                    Update = x.Posts.OrderByDescending(p=>p.Time).Take(1).FirstOrDefault().Time
                }).OrderByDescending(x=>x.Update).ToList();
            
            var model = new ThreadModel
            {
                Threads = threadsWithPost,
                BoardsName = _chanContext.Boards.Where(x=>x.Id==id).Select(x=>x.Name).FirstOrDefault()
            };
            return View(model);
        }

        public ActionResult ShowAddingNewThread()
        {
            return View();
        }
    }
}