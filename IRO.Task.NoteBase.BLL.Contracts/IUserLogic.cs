using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserLogic
    {
        bool AddUser(User user);
        bool DeleteUser(int userId);
        List<User> GetAll();
        User GetById(int userId);
        bool Login(int userId);
        User ActiveUser { get; }
    }
}
