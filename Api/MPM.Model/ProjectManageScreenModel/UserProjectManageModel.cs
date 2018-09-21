using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace MPM.Model
{
    public class UserProjectManageModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public UserProjectManageModel()
        {
            
        }
        public UserProjectManageModel(User user)
        {
            if (user != null)
            {
                UserId = user.UserId;
                Name = user.Name;
                Email = user.Email;
                IsActive = user.IsActive;
                IsAdmin = user.IsAdmin;

            }
        }

    }
}
