using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserService
    {
        bool AddUser(User user);
        bool DeleteUser(long userId);
        List<User> GetAll();
        User GetById(long userId);
        bool Login(long userId);
        User ActiveUser { get; }
    }
}
