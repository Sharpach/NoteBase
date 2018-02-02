using System.Collections.Generic;
using IRO.Tast.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserLogic
    {
        bool AddUser(User user);
        List<User> List();
    }
}
