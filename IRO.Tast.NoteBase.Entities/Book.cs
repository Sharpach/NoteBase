namespace IRO.Task.NoteBase.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual int OwnerId { get; set; }
    }
}
