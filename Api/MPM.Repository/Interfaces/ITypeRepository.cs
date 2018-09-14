using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface ITypeRepository
    {
        List<Databases.Models.Type> GetAllTypes();
        Databases.Models.Type GetTypeByID(int id);
        void AddType(Databases.Models.Type session);
        void UpdateType(Databases.Models.Type session);
        void DeleteType(int id);
    }
}
