﻿using System.Collections.Generic;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Memory
{
    public class UserDao : IUserDao
    {
        private List<User> Memory;
        public UserDao()
        {
            Memory = new List<User>();
        }
        public bool AddUser(User user)
        {
            if (Memory.Count == 0)
                user.Id = 0;
            else
                user.Id = Memory[Memory.Count-1].Id + 1;
            Memory.Add(user);
            return true;
        }
        public User GetById(int userId) => Memory.Find(x => x.Id == userId);

        public List<User> GetAll() => Memory;
    }
}
