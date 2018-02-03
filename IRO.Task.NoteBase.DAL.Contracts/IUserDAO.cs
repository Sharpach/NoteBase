using IRO.Task.NoteBase.Entities;
using System.Collections.Generic;

namespace IRO.Task.NoteBase.DAL.Contracts
{
    public interface IUserDao
    {
        bool AddUser(User user);
        User GetById(uint userId);
        List<User> GetAll();
    }
}