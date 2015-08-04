using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Chat.Infrastructure
{
    public class ChatMessage
    {
        public ChatMember Member;
        public DateTime Date = DateTime.Now;
        public string Text = String.Empty;
    }
}