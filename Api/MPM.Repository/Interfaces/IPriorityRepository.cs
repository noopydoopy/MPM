using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface IPriorityRepository
    {
        List<Priority> GetAllPrioritys();
        Priority GetPriorityByID(int id);
        void AddPriority(Priority session);
        void UpdatePriority(Priority session);
        void DeletePriority(int id);

    }
}
