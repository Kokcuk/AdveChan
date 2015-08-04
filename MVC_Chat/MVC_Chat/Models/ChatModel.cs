using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC_Chat.Infrastructure;

namespace MVC_Chat.Models
{
    public class ChatModel
    {
        public List<ChatMember> Members;
        public List<ChatMessage> Messages;

        public ChatModel()
        {
            Members = new List<ChatMember>();
            Messages = new List<ChatMessage>();

            Messages.Add(new ChatMessage
                {Text = "Chat has been created "+DateTime.Now});
        }
    }
}