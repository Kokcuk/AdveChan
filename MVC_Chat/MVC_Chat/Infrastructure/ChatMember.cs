using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Chat.Infrastructure
{
    public class ChatMember
    {
        public string Name;
        public DateTime LoginTime;
        public DateTime LastPing;
    }
}