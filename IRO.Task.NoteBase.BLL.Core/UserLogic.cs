using System.Collections.Generic;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        public bool AddUser(User user)
        {
            return false;
        }

        public List<User> List()
        {
            throw new System.NotImplementedException();
        }
    }
}
