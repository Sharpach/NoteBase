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
            _dbSet.Add(user);
            _context.SaveChanges();
            return true;
        }

        public bool Login(int userId)
        {
            User loggedUser = _dbSet.First(x => x.Id == userId);
            if(loggedUser == null)
                return false;
            ActiveUser = loggedUser;
            return true;
        }

        public User GetById(int userId) => _dbSet.FirstOrDefault(x => x.Id == userId);

        public List<User> GetAll() => _dbSet.ToList();
    }
}
