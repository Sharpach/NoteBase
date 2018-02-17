namespace IRO.Task.NoteBase.Entities
{
    public class Book
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public virtual long OwnerId { get; set; }
    }
}
