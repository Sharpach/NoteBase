using System.Collections.Generic;
using System.Data.Entity;
using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.EF;
using IRO.Task.NoteBase.Entities;
using System.Linq;

namespace IRO.Task.NoteBase.BLL.Core
{
    public class UserLogic : IUserLogic
    {
        //TODO: intefraces EF
        private readonly MainContext _context;
        private readonly DbSet<User> _dbSet;
        public User ActiveUser { get; private set; }

        public UserLogic()
        {
            _context = new MainContext();
            _dbSet = _context.Users;
        }

        public bool AddUser(User user)
        {
            if(_dbSet.Any(x => x.Id == user.Id))
            {
                return false;
            }
            _dbSet.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteUser(int userId)
        {
            var record = GetById(userId);
            if (record == null)
                return false;

            var entry = _context.Entry(record);
            entry.State = EntityState.Deleted;
            _context.SaveChanges();
            ActiveUser = null;
            return true;
        }
        public bool Login(int userId)
        {
            var loggedUser = _dbSet.FirstOrDefault(x => x.Id == userId);
            if(loggedUser == null)
                return false;
            ActiveUser = loggedUser;
            return true;
        }

        public User GetById(int userId) => _dbSet.FirstOrDefault(x => x.Id == userId);

        public List<User> GetAll() => _dbSet.ToList();
    }
}
