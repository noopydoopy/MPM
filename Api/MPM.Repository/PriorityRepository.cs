using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly mpmContext _context;

        public PriorityRepository(mpmContext context)
        {
            _context = context;
        }

        public void AddPriority(Priority session)
        {
            throw new NotImplementedException();
        }

        public void DeletePriority(int id)
        {
            throw new NotImplementedException();
        }

        public List<Priority> GetAllPrioritys()
        {
            return _context.Priority.ToList();
        }

        public Priority GetPriorityByID(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdatePriority(Priority session)
        {
            throw new NotImplementedException();
        }
    }
}
