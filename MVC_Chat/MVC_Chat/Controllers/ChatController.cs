using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MVC_Chat.Filtres;
using MVC_Chat.Infrastructure;
using MVC_Chat.Models;

namespace MVC_Chat.Controllers
{
    public class ChatController : Controller
    {
        private static ChatModel _chatModel;

        [CustAuth]
        public ActionResult Index(string member, bool? logOn, bool? logOff, string chatMessage)
        {
            try
            {
                if (_chatModel==null)
                    _chatModel= new ChatModel();

                if (_chatModel.Messages.Count > 100)
                    _chatModel.Messages.RemoveRange(0, 90);

                if (!Request.IsAjaxRequest())
                {
                    ViewData["nick"] = TempData["nickname"];
                    return View(_chatModel);
                }
                else if(logOn!=null && (bool)logOn)
                {
                    if (_chatModel.Members.FirstOrDefault(x => x.Name == member) != null)
                    {
                        ChatMember _member = _chatModel.Members.FirstOrDefault(x => x.Name == member);
                        _chatModel.Members.Remove(_member);
                        ViewData["nick"] = TempData["nickname"];
                        return View(_chatModel);
                    }
                    else if (_chatModel.Members.Count>10)
                    {
                        throw new Exception("There are maximimum members in chat");
                    }
                    else
                    {
                        _chatModel.Members.Add(new ChatMember
                        {
                            Name = member, LoginTime = DateTime.Now, LastPing = DateTime.Now
                        });
                        _chatModel.Messages.Add(new ChatMessage()
                        {
                            Text = member + " entered the chat!",
                            Date = DateTime.Now
                        });
                        return PartialView("ChatRoom", _chatModel);
                    }
                }
                else if (logOff != null && (bool) logOff)
                {
                    LogOff(_chatModel.Members.FirstOrDefault(x => x.Name == member));
                    return Redirect("Home/Index");
                }
                else
                {
                    ChatMember currMember = _chatModel.Members.FirstOrDefault(x => x.Name == member);
                    currMember.LastPing=DateTime.Now;
                    List<ChatMember> toRemove = new List<ChatMember>();
                    foreach (ChatMember mmber in _chatModel.Members)
                    {
                        TimeSpan span = DateTime.Now - mmber.LastPing;
                        if (span.TotalSeconds > 120)
                            toRemove.Add(mmber);
                    }
                    foreach (ChatMember cm in toRemove)
                    {
                        LogOff(cm);
                    }
                    if (!string.IsNullOrEmpty(chatMessage))
                    {
                        _chatModel.Messages.Add(new ChatMessage()
                        {
                            Member = currMember,
                            Text = chatMessage,
                            Date = DateTime.Now
                        });
                    }
                    return PartialView("History", _chatModel);
                }
            }
            catch (Exception ex)
            {

                Response.StatusCode = 500;
                return Content(ex.Message);
            }
        }

        public void LogOff(ChatMember member)
        {
            _chatModel.Members.Remove(member);
            _chatModel.Messages.Add(new ChatMessage()
            {
                Text = member.Name+ " left the chat",
                Date = DateTime.Now
            });
        }
    }
}