using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public User ActiveUser { get; private set; }

        public bool AddUser(User user)
        {
            if (Database.Users.GetAll().Any(x => x.Id == user.Id))
            {
                return false;
            }
            Database.Users.Create(user);
            Database.Save();
            return true;
        }

        public bool DeleteUser(long userId)
        {
            var user = GetById(userId);
            if (user == null) return false;
            Database.Users.Delete(userId);
            Database.Save();
            ActiveUser = null;
            return true;
        }

        public List<User> GetAll()
        {
            return Database.Users.GetAll().ToList();
        }

        public User GetById(long userId)
        {
            return Database.Users.Get(userId);
        }

        public bool Login(long userId)
        {
            var loggedUser = Database.Users.Get(userId);
            if (loggedUser == null)
                return false;
            ActiveUser = loggedUser;
            return true;
        }
    }
}
