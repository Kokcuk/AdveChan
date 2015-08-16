using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdveChan.Models;
using AdveChan.ViewModels;
using MongoRepository;

namespace AdveChan.Controllers
{
    using Projections;

    public class BoardController : Controller
    {
        private readonly ChanContext _chanContext;
        private readonly MongoRepository<Cash> _threadsCash;

        public BoardController()
        {
            _chanContext = new ChanContext();
            _threadsCash = new MongoRepository<Cash>();
        }

        public ActionResult ShowThreads(int id, int? page)
        {
            int pageSize = 10;
            ViewData["BoardId"] = id;

            if (_threadsCash.Any(x => x.CashedBoardId == id))
            {
                var cashedThreadsAmount =
                    _threadsCash.Where(x => x.CashedBoardId == id).Select(x => x.CashedThreads).ToList().Count;
                if (cashedThreadsAmount <= 50)
                {
                    UpdateCash(id);
                }
            }
            else
            {
                AddToCash(id);
            }
            var model = new ThreadModel
            {
                Threads = _threadsCash.FirstOrDefault(x => x.CashedBoardId == id).CashedThreads
                    .OrderByDescending(x => x.Update)
                    .Skip((page*pageSize) ?? 0).Take(pageSize).ToList(),
                BoardsName = _chanContext.Boards.Where(x => x.Id == id).Select(x => x.Name).FirstOrDefault()
            };
            return View(model);
        }

        private void AddToCash(int id)
        {
            List<ThreadWithPosts> threadsWithPost = _chanContext.Threads
                .Where(x => x.BoardId == id).Select(x => new ThreadWithPosts
                {
                    Id = x.Id,
                    LastPosts = x.Posts.OrderByDescending(p => p.Time).Take(3).ToList(),
                    OpPost = x.Posts.OrderBy(p => p.Time).Take(1).FirstOrDefault(),
                    Update = x.Posts.OrderByDescending(p => p.Time).Take(1).FirstOrDefault().Time
                }).OrderByDescending(x => x.Update).ToList();
            var entityToCash = new Cash
            {
                CashedThreads = threadsWithPost,
                CashedBoardId = id
            };
            _threadsCash.Add(entityToCash);
        }

        private void UpdateCash(int id)
        {
            List<ThreadWithPosts> threadsWithPost = _chanContext.Threads
                .Where(x => x.BoardId == id).Select(x => new ThreadWithPosts
                {
                    Id = x.Id,
                    LastPosts = x.Posts.OrderByDescending(p => p.Time).Take(3).ToList(),
                    OpPost = x.Posts.OrderBy(p => p.Time).Take(1).FirstOrDefault(),
                    Update = x.Posts.OrderByDescending(p => p.Time).Take(1).FirstOrDefault().Time
                }).OrderByDescending(x => x.Update).ToList();

            var entityToUpdate = _threadsCash.FirstOrDefault(x => x.CashedBoardId == id);
            entityToUpdate.CashedBoardId = id;
            entityToUpdate.CashedThreads = threadsWithPost;
            _threadsCash.Update(entityToUpdate);
        }

        public ActionResult ShowAddingNewThread(int id)
        {
            ViewData["BId"] = id;
            return View();
        }
    }
}