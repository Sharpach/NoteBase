using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRO.Task.NoteBase.DAL.EF
{
    public class UserRepository : IRepository<User>
    {
        private IMainContext db;

        public UserRepository(IMainContext context)
        {
            db = context;
        }

        public void Create(User item)
        {
            db.Users.Add(item);
        }

        public void Delete(long id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public User Get(long id)
        {
            return db.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public void Update(User item)
        {
            //db.Entry(item).State = EntityState.Modified;
            db.Users.Update(item);
        }
    }
}
