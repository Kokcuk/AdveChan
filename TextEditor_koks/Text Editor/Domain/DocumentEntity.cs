namespace TextEditor.Domain
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] CompressedContent { get; set; }
    }
}
