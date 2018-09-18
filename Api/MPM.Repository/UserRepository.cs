using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly mpmContext _context;

        public UserRepository(mpmContext context)
        {
            _context = context;
        }

        public void AddUser(User user)
        {
            try
            {
                _context.User.Add(user);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeleteUser(int userId)
        {
            try
            {
                var currentUser = _context.User.Find(userId);
                _context.User.Remove(currentUser);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public User GetUserById(int userId)
        {
            return _context.User.Find(userId);
        }

        public User GetUserByCode(String code)
        {
            var query = from User in _context.User
                        where User.Code == code
                        select User;
            User user = query.FirstOrDefault();
            return user;
        }

        public void UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
