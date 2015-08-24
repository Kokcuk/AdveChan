using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdveChan.Projections;
using MongoRepository;

namespace AdveChan.Models
{
    public class Cash:Entity
    {
        public int CashedBoardId { get; set; }
        public List<ThreadWithPosts> CashedThreads { get; set; }
    }
}