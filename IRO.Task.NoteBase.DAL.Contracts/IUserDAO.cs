using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IUserDao
    {
        bool AddUser(User user);
        bool GetUserById(uint userId);
        bool GetUsersList();
    }
}
