using System;

namespace AdveChan.Projections
{
    using System.Collections.Generic;
    using Models;
    public class ThreadWithPosts
    {
        public int Id { get; set; }
        public List<Post> LastPosts { get; set; }
        public Post OpPost { get; set; }
        public DateTime Update { get; set; }
    }
}