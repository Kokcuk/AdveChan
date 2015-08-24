namespace AdveChan.Entities
{
    using System.Collections.Generic;

    public class Thread : EntityBase
    {
        public long BoardId { get; set; }
        public Board Board { get; set; }
        public List<Post> Posts { get; set; }
    }
}