using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AdveChan.Models;
using AdveChan.ViewModels;

namespace AdveChan.Controllers
{
    public class ThreadController : Controller
    {
        private readonly ChanContext _chanContext;

        public ThreadController()
        {
            _chanContext=new ChanContext();
        }

        [HttpPost]
        public ActionResult Create(int id, string email, string title, string content, string url)
        {
            var board = _chanContext.Boards.FirstOrDefault(x => x.Id == id);
            var thread = new Thread
            {
                BoardId = id,
                Board = board,
                Posts = new List<Post>()
            };
            thread.Posts.Add(new Post
            {
                ThreadId = thread.Id,
                Email = email,
                Title = title,
                Time = DateTime.Now,
                ImagesUrl = url,
                Content = content
            });
            _chanContext.Threads.Add(thread);
            _chanContext.SaveChanges();
            return Redirect("/Board/ShowThreads/" + id);
        }

        public ActionResult ShowPosts(int id)
        {
            TempData["ThreadId"] = id;
            List < Post > posts= _chanContext.Posts.Where(x => x.ThreadId == id).ToList();
            var model = new PostModel
            {
                Posts = posts
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult AddPost(int id, Post post, HttpPostedFileBase image)
        {
            var thread = _chanContext.Threads.FirstOrDefault(x => x.Id == id);
            var newPost = AddingPost(post, thread);
            var amountOfPosts = _chanContext.Posts.Where(x => x.ThreadId == thread.Id).Count();
            if (amountOfPosts > 500||post.Title=="sage")
            {               
                newPost.Time=new DateTime(2000,01,01,11,11,11);
            }
            _chanContext.Posts.Add(newPost);
            _chanContext.SaveChanges();
            return Redirect("/Thread/ShowPosts/" + id);
        }

        private Post AddingPost(Post post, Thread thread)
        {
            var retPost = new Post
            {
                Thread = thread,
                ThreadId = post.ThreadId,
                Email = post.Email,
                Title = post.Title,
                Time = DateTime.Now,
                ImagesUrl = post.ImagesUrl,
                Content = post.Content
            };
            return retPost;
        }
    }
}