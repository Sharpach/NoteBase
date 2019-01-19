namespace IRO.Task.NoteBase.Entities
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public byte[] PasswordHash { get; set; } // byte[32]
        public byte[] PasswordSalt { get; set; } // byte[32]
    }
}
