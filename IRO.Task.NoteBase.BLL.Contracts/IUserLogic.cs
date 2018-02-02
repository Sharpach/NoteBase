using IRO.Task.NoteBase;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.BLL.Contracts
{
    public interface IUserLogic
    {
        bool AddUser(User user);
        List<User> List();
    }
}
