using MPM.Databases.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPM.Repository.Interfaces
{
    public interface IRefreshTokenRepository
    {
        List<RefreshToken> GetAllRefreshToken();
        RefreshToken GetRefreshTokenByUserId(String userId);
        RefreshToken GetRefreshTokenByRefreshToken(String refreshToken);
        void AddRefreshToken(RefreshToken session);
        void UpdateRefreshToken(RefreshToken session);
        void DeleteRefreshToken(String userId);
    }
}
