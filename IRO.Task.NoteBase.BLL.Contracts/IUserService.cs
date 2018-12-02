using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserService
    {
        bool AddUser(string username, string password);
        bool DeleteUser(long userId);

        List<User> GetAll();
        User GetById(long userId);
        User GetByName(string username);

        bool Login(long userId, string password);
        User ActiveUser { get; }
    }
}
