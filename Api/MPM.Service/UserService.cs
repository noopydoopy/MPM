using Microsoft.Extensions.Options;
using MPM.Helpers;

namespace MPM.Service
{
    public class UserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
    }

}
