
using MPM.Databases.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace MPM.Services.Interfaces
{
    public interface IEmailService
    {
       Task SendEmail(User user, string subject, string message);
    }
}
