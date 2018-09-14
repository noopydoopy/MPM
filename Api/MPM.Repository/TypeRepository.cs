using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class TypeRepository : ITypeRepository
    {
        private readonly mpmContext _context;

        public TypeRepository(mpmContext context)
        {
            _context = context;
        }
        void ITypeRepository.AddType(Databases.Models.Type session)
        {
            try
            {
                _context.Type.Add(session);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        void ITypeRepository.DeleteType(int id)
        {
            try
            {
                Databases.Models.Type removeType = GetTypeByid(id);
                _context.Type.Remove(removeType);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<Databases.Models.Type> ITypeRepository.GetAllTypes()
        {
            List<Databases.Models.Type> type = _context.Type.ToList();
            return type;
        }

        Databases.Models.Type ITypeRepository.GetTypeByID(int id)
        {
            return GetTypeByid(id);
        }

        void ITypeRepository.UpdateType(Databases.Models.Type session)
        {
            try
            {
                var updateType = GetTypeByid(session.TypeId);
                if (updateType != null)
                {
                    updateType.Name = session.Name;
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception(string.Format("No Type id {0} data in database", session.TypeId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Databases.Models.Type GetTypeByid(int id)
        {
            var type = _context.Type.Where(t => t.TypeId == id); ;
            if (type.Count() > 0)
                return type.First();
            else
                return null;
        }
    }
}
