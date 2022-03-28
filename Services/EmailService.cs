using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
namespace Osvip.Api.Services
{
    public class EmailService
    {
        /// <summary>
        /// Отправка электронного письма
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("OSVIP MAI", "osvip@internet.ru"));//Адресс
            emailMessage.To.Add(new MailboxAddress("", email));//Получатель
            emailMessage.Subject = subject;//Тема
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)//Контент
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.mail.ru", 465, true);//Смтп сервер
                await client.AuthenticateAsync("osvip@internet.ru", "xJiH93a2LCX6sNyBHbVH");//данные аутентификации
                await client.SendAsync(emailMessage);//отправить письмо
                await client.DisconnectAsync(true);//отключиться от сервера
            }
        }
        
        }
}

