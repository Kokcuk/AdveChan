using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;
using MVC_Chat.Models;

namespace MVC_Chat.Infrastructure
{
    public class UserContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}