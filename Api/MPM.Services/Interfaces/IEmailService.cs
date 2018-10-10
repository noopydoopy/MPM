
using MPM.Databases.Models;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace MPM.Services.Interfaces
{
    public interface IEmailService
    {
       Task SendRestPasswordEmail(User user, string subject, string message);
       Task SendConfirmEmail(User user, string subject, string message);
    }
}
