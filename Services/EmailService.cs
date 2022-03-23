using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace Osvip.Api.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("OSVIP MAI", "osvip@internet.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);
                await client.AuthenticateAsync("osvip@internet.ru", "xJiH93a2LCX6sNyBHbVH");
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        
        }
}

