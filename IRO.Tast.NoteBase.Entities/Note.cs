namespace IRO.Task.NoteBase.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual int ParentBookId { get; set; }
    }
}
