using Microsoft.EntityFrameworkCore;
using MPM.Databases.Models;
using MPM.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MPM.Repository
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly mpmContext _context;
        public RefreshTokenRepository(mpmContext context)
        {
            _context = context;
        }

        //GET All
        public List<RefreshToken> GetAllRefreshToken()
        {
            return _context.RefreshToken.ToList();
        }

        //GET BY UserId
        public RefreshToken GetRefreshTokenByUserId(String userId)
        {
            var query = from RefreshToken in _context.RefreshToken
                        where RefreshToken.UserId == userId
                        select RefreshToken;
            RefreshToken RToken = query.FirstOrDefault();
            return RToken;
        }

        //GET BY RefreshToken
        public RefreshToken GetRefreshTokenByRefreshToken(String refreshToken)
        {
            var query = from RefreshToken in _context.RefreshToken
                        where RefreshToken.Rtoken == refreshToken
                        select RefreshToken;
            RefreshToken RToken = query.FirstOrDefault();
            return RToken;
        }

        //INSERT
        public void AddRefreshToken(RefreshToken refreshToken)
        {
            try
            {
                _context.RefreshToken.Add(refreshToken);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw new Exception("Can't Insert RefreshToken");
            }
        }

        //DELETE
        public void DeleteRefreshToken(String userId)
        {
            RefreshToken refreshToken = _context.RefreshToken.Find(userId);
            if (refreshToken != null)
            {
                try
                {
                    _context.RefreshToken.Remove(refreshToken);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw new Exception("Can't Delete RefreshToken");
                }
            }
        }

        //UPDATE
        public void UpdateRefreshToken(RefreshToken session)
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
