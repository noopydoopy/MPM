using MPM.Databases.Models;
using MPM.Model;
using System;
using System.Collections.Generic;

namespace MPM.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByCode(String code);
        User GetUserResetPasswordByCode(String code);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
        UserModel Authenticate(string username, string password);
        UserModel RefreshToken(String refreshToken);
        List<UserProjectManageModel> GetUserNotinProject(int projectId);
        List<UserProjectManageModel> GetUserInProject(int projectId);
        User ResetPassword(String email);
    }
}
