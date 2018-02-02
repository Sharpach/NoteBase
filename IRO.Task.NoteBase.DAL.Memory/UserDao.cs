﻿using System;
using System.Collections.Generic;
using IRO.Task.NoteBase.DAL.Contracts;
using IRO.Task.NoteBase.Entities;

namespace IRO.Task.NoteBase.DAL.Memory
{
    public class UserDao : IUserDao
    {
        private List<User> Memory { get; set; }
        public bool AddUser(User user)
        {

            if (Memory.Count == 0) user.Id = 0;
            else user.Id = Memory.FindLast(x => true).Id + 1;
            return true;
        }

        public User GetUserById(uint userId)
        {
            return Memory.Find(x => x.Id == userId);
        }

        public List<User> GetUsersList()
        {
            return Memory;
        }
    }
}
