using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByCode(String code);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
