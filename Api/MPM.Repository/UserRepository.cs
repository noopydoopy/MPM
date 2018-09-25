using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MPM.Databases.Models;
using MPM.Model;
using MPM.Repository.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MPM.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly mpmContext _context;
        private readonly TokenModel _tokenModel;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserRepository(mpmContext context, IOptions<TokenModel> tokenModel, IRefreshTokenRepository refreshTokenContext)
        {
            _context = context;
            _tokenModel = tokenModel.Value;
            _refreshTokenRepository = refreshTokenContext;
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
            var passEnrypt = EncryptString(user.Password);
            user.Password = passEnrypt;
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

        public UserModel Authenticate(String email, String password)
        {
            var passEnrypt = EncryptString(password);
            var user = _context.User.SingleOrDefault(x => x.Email == email && x.Password == passEnrypt);

            if (user == null)
                return null;

            UserModel userModel = GenTokenAndRefreshToken(user);

            return userModel;
        }

        public UserModel RefreshToken(String refreshToken)
        {
            RefreshToken refreshTokenObj = _refreshTokenRepository.GetRefreshTokenByRefreshToken(refreshToken);

            if(refreshTokenObj == null)
            {
                return null;
            }
            else
            {
                if(DateTime.Compare(DateTime.Now, refreshTokenObj.RefreshTokenExpire) > 0)
                {
                    _refreshTokenRepository.DeleteRefreshToken(refreshTokenObj.UserId);
                    return null;
                }
                else
                {
                    var userDetail = DecryptString(refreshTokenObj.UserDetail);
                    JObject jObject = JObject.Parse(userDetail);

                    User user = new User();
                    user.UserId = (int)jObject["UserId"];
                    user.UserName = (String)jObject["UserName"];
                    user.Name = (String)jObject["Name"];
                    user.Email = (String)jObject["Email"];
                    user.IsAdmin = (Boolean)jObject["IsAdmin"];

                    UserModel userModel = GenTokenAndRefreshToken(user);
                    return userModel;
                }
            }
        }
        public List<UserProjectManageModel> GetUserNotinProject(int projectId)
        {
            List<User> allUser = (from user in _context.User
                           select user).ToList();

            List<int> userInProject = (from userProject in _context.UserProject
                               where userProject.ProjectId == projectId
                                select userProject.UserId).ToList();

            List<User> userList = allUser.Where(user => !userInProject.Any(uip=>uip==user.UserId)).ToList();
            List<UserProjectManageModel> userProjectManageModel = TranferUserToUserProjectManage(userList);
            return userProjectManageModel;
        }
        public List<UserProjectManageModel> GetUserInProject(int projectId)
        {
            List<User> userInProject = (from user in _context.User
                                        from userProject in _context.UserProject
                                        where userProject.ProjectId == projectId && user.UserId == userProject.UserId
                                        select user).ToList();
            List<UserProjectManageModel> userProjectManageModel = TranferUserToUserProjectManage(userInProject);

            return userProjectManageModel;
        }

        private List<UserProjectManageModel> TranferUserToUserProjectManage(List<User> users)
        {
            List<UserProjectManageModel> userProjectManageModel = new List<UserProjectManageModel>();
            foreach (var user in users)
            {
                userProjectManageModel.Add(new UserProjectManageModel(user));
            }
            return userProjectManageModel;
        }

        public string EncryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_tokenModel.Key));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        public string DecryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_tokenModel.Key));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        public UserModel GenTokenAndRefreshToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenModel.Key);
            var tokenExpireDate = DateTime.Now.AddDays(_tokenModel.TokenExpire);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = tokenExpireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //generate refresh token
            var RefreshTokenExpireDate = DateTime.Now.AddDays(_tokenModel.RefreshTokenExpire);
            var RefreshTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString())
                }),
                Expires = RefreshTokenExpireDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToken = tokenHandler.CreateToken(RefreshTokenDescriptor);

            UserModel userModel = new UserModel();
            userModel.AccessToken = tokenHandler.WriteToken(token);
            userModel.AccessTokenExpire = tokenExpireDate;
            userModel.RefreshToken = tokenHandler.WriteToken(refreshToken);
            userModel.RefreshTokenExpire = RefreshTokenExpireDate;
            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;
            userModel.Name = user.Name;
            userModel.Email = user.Email;
            userModel.IsAdmin = user.IsAdmin;

            var userId = EncryptString(user.UserId.ToString()).Trim();
            RefreshToken refreshTokenDb = new RefreshToken();
            refreshTokenDb.UserId = userId;
            refreshTokenDb.Rtoken = userModel.RefreshToken;
            refreshTokenDb.RefreshTokenExpire = RefreshTokenExpireDate;
            refreshTokenDb.UserDetail = EncryptString(JsonConvert.SerializeObject(userModel));
            refreshTokenDb.CreateDate = DateTime.Now;

            //get old refresh token
            RefreshToken oldRefreshToken = _refreshTokenRepository.GetRefreshTokenByUserId(userId);
            if (oldRefreshToken != null)
            {
                _refreshTokenRepository.DeleteRefreshToken(userId);
            }

            _refreshTokenRepository.AddRefreshToken(refreshTokenDb);
            return userModel;
        }
    }

}
