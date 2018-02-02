using System.Collections.Generic;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserLogic
    {
        bool AddUser(User user);
        List<User> List();
        bool Login(uint userID);
        User ActiveUser { get; }
    }
}
