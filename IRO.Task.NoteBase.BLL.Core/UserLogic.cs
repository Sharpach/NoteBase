using System.Collections.Generic;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.DAL.Memory;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        public User ActiveUser { get; private set; }

        public UserLogic()
        {
            _userDao = new UserDao();
        }

        public bool AddUser(User user)
        {
            return _userDao.AddUser(user);
        }

        public bool Login(uint userId)
        {
            User loggedUser = _userDao.GetUserById(userId);
            if(loggedUser == null)
                return false;
            ActiveUser = loggedUser;
            return true;
        }

        public List<User> List()
        {
            return _userDao.GetUsersList();
        }
    }
}
