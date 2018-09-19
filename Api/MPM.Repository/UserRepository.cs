using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MPM.Databases.Models;
using MPM.Helpers;
using MPM.Model;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MPM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly mpmContext _context;
        private readonly AppSettings _appSettings;

        public UserRepository(mpmContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
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

        public TokenModel Authenticate(string username, string password)
        {
            var user = _context.User.SingleOrDefault(x => x.Email == username && x.Password == password);

            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            TokenModel tokenModel = new TokenModel();
            tokenModel.Token = tokenHandler.WriteToken(token);

            return tokenModel;
        }
    }
}
