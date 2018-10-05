using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MPM.Databases.Models;
using MPM.Services.Interfaces;
using System.IO;
using System.Net;
using System.Net.Mail;
using Task = System.Threading.Tasks.Task;

namespace MPM.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private IHostingEnvironment _env;
        public EmailService(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public async Task SendEmail(User user, string subject, string host)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = _configuration["Email:Email"],
                    Password = _configuration["Email:Password"]
                };

                client.Credentials = credential;
                client.Host = _configuration["Email:Host"];
                client.Port = int.Parse(_configuration["Email:Port"]);
                client.EnableSsl = true;

                var webRoot = _env.WebRootPath;
                var pathToFile = _env.WebRootPath
                            + Path.DirectorySeparatorChar.ToString()
                            + "Templates"
                            + Path.DirectorySeparatorChar.ToString()
                            + "ResetPasswordTemplate.html";

                var builder = new BodyBuilder();
                using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                string messageBody = string.Format(builder.HtmlBody);
                var url = host + "/user/resetPassword?code=" + user.Code;
                messageBody = messageBody.Replace("username", user.Name);
                messageBody = messageBody.Replace("resetUrl", url);

                using (var emailMessage = new MailMessage())
                {
                    emailMessage.To.Add(new MailAddress(user.Email));
                    emailMessage.From = new MailAddress(_configuration["Email:Email"]);
                    emailMessage.Subject = subject;
                    emailMessage.IsBodyHtml = true;
                    emailMessage.Body = messageBody;
                    client.Send(emailMessage);
                }
            }
            await Task.CompletedTask;
        }

        public Task SendEmail(string email, string message)
        {
            throw new System.NotImplementedException();
        }
    }
}