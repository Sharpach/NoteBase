using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserLogic
    {
        bool AddUser(User user);
        bool DeleteUser(long userId);
        List<User> GetAll();
        User GetById(long userId);
        bool Login(long userId);
        User ActiveUser { get; }
    }
}
