using System.Collections.Generic;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        public User ActiveUser { get; private set; }
        public bool AddUser(User user)
        {
            return false;
        }
        public bool Login(uint userID)
        {
            return false;
        }
        public List<User> List()
        {
            throw new System.NotImplementedException();
        }
    }
}
