using Microsoft.EntityFrameworkCore;
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
            try
            {
                _context.Priority.Add(session);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void DeletePriority(int id)
        {
            try
            {
                var currentPriority = _context.Priority.Find(id);
                _context.Priority.Remove(currentPriority);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public List<Priority> GetAllPrioritys()
        {
            return _context.Priority.ToList();
        }

        public Priority GetPriorityByID(int id)
        {
            return _context.Priority.Find(id);
        }

        public void UpdatePriority(Priority session)
        {
            try
            {
                _context.Entry(session).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
