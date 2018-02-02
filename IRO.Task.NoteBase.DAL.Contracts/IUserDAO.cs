using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IUserDAO
    {
        bool AddUser(User user);
        bool GetUserByID(uint userID);
        bool GetUsersList();
    }
}
