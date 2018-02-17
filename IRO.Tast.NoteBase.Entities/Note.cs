namespace IRO.Task.NoteBase.Entities
{
    public class Note
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public virtual long ParentBookId { get; set; }
    }
}
