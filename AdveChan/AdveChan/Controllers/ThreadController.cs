using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using AdveChan.Models;
using AdveChan.Projections;
using AdveChan.ViewModels;
using BotDetect.Web.UI.Mvc;
using MongoRepository;
namespace AdveChan.Controllers
{
    public class ThreadController : Controller
    {
        private readonly ChanContext _chanContext;
        private readonly MongoRepository<Cash> _threadsCash;

        public ThreadController()
        {
            _chanContext=new ChanContext();
            _threadsCash = new MongoRepository<Cash>();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "SampleCaptcha", "Incorrect CAPTCHA code!")]
        public ActionResult Create(AddThreadModel model)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/StartPage/ShowStartPage");
            }
            if (CheckForBan(Request.UserHostAddress))
            {
                var board = _chanContext.Boards.FirstOrDefault(x => x.Id == model.BoardId);
                var thread = new Thread
                {
                    BoardId = model.BoardId,
                    Board = board,
                    Posts = new List<Post>()
                };
                var url = model.Imgsrc;
                if (url != null) url = url.Remove(0, 1);
                thread.Posts.Add(new Post
                {
                    ThreadId = thread.Id,
                    Email = model.Email,
                    Title = model.Title,
                    Time = DateTime.Now,
                    ImagesUrl = url,
                    Content = model.Content,
                    Ip = Request.UserHostAddress
                });
                _chanContext.Threads.Add(thread);
                _chanContext.SaveChanges();
                List<ThreadWithPosts> threadsList=_threadsCash.FirstOrDefault(x => x.CashedBoardId == model.BoardId)
                    .CashedThreads
                    .OrderByDescending(x => x.Update).Take(50).ToList();
                var cashedEntityToUpdate = _threadsCash.FirstOrDefault(x => x.CashedBoardId == model.BoardId);
                cashedEntityToUpdate.CashedBoardId = model.BoardId;
                cashedEntityToUpdate.CashedThreads = threadsList;
                _threadsCash.Update(cashedEntityToUpdate);
            }
            return Redirect("/Board/ShowThreads/" + model.BoardId);
        }

        public ActionResult ShowPosts(int id)
        {
            List < Post > posts= _chanContext.Posts.Where(x => x.ThreadId == id).ToList();
            var model = new ShowPostsModel
            {
                Posts = posts,
                ThreadId = id
            };
            return View(model);
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode","SampleCaptcha","Incorrect CAPTCHA code!")]
        public ActionResult AddPost(AddPostModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowPosts", "Thread", new {id = model.ThreadId});
            }
            else
            {
                if (CheckForBan(Request.UserHostAddress))
                {
                    var url = model.Imgsrc;
                    if (url != null) url = url.Remove(0, 1);
                    var post = new Post
                    {
                        ThreadId = model.ThreadId,
                        Content = model.Content,
                        Email = model.Email,
                        ImagesUrl = url,
                        Time = DateTime.Now,
                        Title = model.Title,
                        Ip = Request.UserHostAddress
                    };
                    var amountOfPosts = _chanContext.Posts.Count(x => x.ThreadId == model.ThreadId);
                    if (amountOfPosts > 500 || model.Title == "sage")
                    {
                        post.Time = new DateTime(2000, 01, 01, 11, 11, 11);
                    }
                    _chanContext.Posts.Add(post);
                    _chanContext.SaveChanges();

                    CashLastPosts(model.ThreadId,post);
                   
                }
                return RedirectToAction("ShowPosts", "Thread", new {id = model.ThreadId});
            }
        }

        private void CashLastPosts(int threadId, Post post)
        {
            var boardId =
                       _chanContext.Threads
                       .Where(x => x.Id == threadId)
                       .Select(x => x.BoardId)
                       .FirstOrDefault();
            var cashEntity = _threadsCash.FirstOrDefault(x => x.CashedBoardId == boardId);
            if (cashEntity.CashedThreads.Any(x => x.Id == threadId))
            {
                List<Post> listOfLastPosts = cashEntity.CashedThreads
                    .FirstOrDefault(x => x.Id == threadId)
                    .LastPosts.OrderByDescending(x => x.Time).ToList();
                listOfLastPosts.RemoveAt(0);
                listOfLastPosts.Add(post);

                cashEntity.CashedThreads.FirstOrDefault(x => x.Id == threadId).LastPosts = listOfLastPosts;
                cashEntity.CashedBoardId = boardId;
                _threadsCash.Update(cashEntity);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteThread(int id)
        {
            Thread threadToDelete = _chanContext.Threads.FirstOrDefault(x => x.Id == id);
            var boardId = threadToDelete.BoardId;
            _chanContext.Threads.Remove(threadToDelete);
            _chanContext.SaveChanges();

            return Redirect("Board/ShowThreads/"+boardId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeletePost(int id)
        {
            var postToDelete = _chanContext.Posts.FirstOrDefault(x => x.Id == id);
            var threadId = postToDelete.ThreadId;
            _chanContext.Posts.Remove(postToDelete);
            _chanContext.SaveChanges();

            return Redirect("/Thread/ShowPosts/"+threadId);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddBan(string ip, string terms)
        {
            DateTime period;
            switch (terms)
            {
                case "day":
                    period = DateTime.Now.AddDays(1);
                    break;
                case "week":
                    period = DateTime.Now.AddDays(7);
                    break;
                case "month":
                    period = DateTime.Now.AddDays(30);
                    break;
                case "year":
                    period = DateTime.Now.AddDays(365);
                    break;
                default:
                    period = DateTime.Now.AddHours(1);
                    break;
            }
            _chanContext.Bans.Add(new Ban
            {
                IpAdress = ip,
                Terms = period
            });
            _chanContext.SaveChanges();
            return Redirect("StartPage/ShowStartPage");
        }

        private bool CheckForBan(string ip)
        {
            var isBanned = _chanContext.Bans.FirstOrDefault(x => x.IpAdress == ip);
            if (isBanned == null)
            {
                return true;
            }
            if (isBanned.Terms <= DateTime.Now)
            {
                _chanContext.Bans.Remove(isBanned);
                return true;
            }
            return false;
        }
    }
}