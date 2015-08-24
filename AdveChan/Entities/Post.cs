namespace AdveChan.Entities
{
    using System;

    public sealed class Post:EntityBase
    {
        public Thread Thread { get; set; }
        public long ThreadId { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public string Attachments { get; set; }
        public string Content { get; set; }
        public string Ip { get; set; }
    }
}