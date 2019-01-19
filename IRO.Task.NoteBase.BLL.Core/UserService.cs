using IRO.Task.NoteBase.BLL.Contracts;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;
using System.Text;

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

        public bool AddUser(string username, string password)
        {
            var rand = new Random();
            byte[] salt  = new byte[32];
            
            rand.NextBytes(salt);
            
            User user = new User
            {
                Name = username, 
                PasswordHash = 
                    HashPassword(password,salt), 
                PasswordSalt = salt
            };
            
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

        public List<User> GetAll() => Database.Users.GetAll().ToList();

        public User GetById(long userId) => Database.Users.Get(userId);

        public User GetByName(string username) => Database.Users.Find((user) => user.Name == username).FirstOrDefault();

        public bool Login(long userId, string password)
        {
            var user = Database.Users.Get(userId);

            if (user == null)
                throw new ArgumentException("User with that name was not found.");

            if (user.PasswordHash.SequenceEqual(HashPassword(password, user.PasswordSalt)))
                return false;

            ActiveUser = user;
            return true;
        }

        private static byte[] HashPassword(string password, byte[] salt)
        {
            byte[] passwordHash;

            using (var sha256 = SHA256.Create())
            {
                var bytes = new List<byte>(Encoding.UTF8.GetBytes(password));
                bytes.AddRange(salt);
                passwordHash = sha256.ComputeHash(bytes.ToArray());
            }

            return passwordHash;
        }
    }
}
