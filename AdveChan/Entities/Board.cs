namespace AdveChan.Entities
{
    using System.Collections.Generic;

    public class Board: EntityBase
    {
        public string Name { get; set; }
        public List<Thread> Threads { get; set; } 
    }
}