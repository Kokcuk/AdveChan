using System;

//This is entity of DB's Table, that we will use in our code to simplify it

namespace TextEditor.Domain
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] CompressedContent { get; set; }
        public DateTime TimeOfChanging { get; set; }
    }
}