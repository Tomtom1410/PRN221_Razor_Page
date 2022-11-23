using System.Net.Mail;
using System.Net;

namespace WebRazor.Helper
{
    public class MailHelper
    {
        public async static void SendMail(string email, string body, Dictionary<Stream, string>? files)
        {

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "luongnthhe151453@fpt.edu.vn",  // replace with valid value
                    Password = "Hienluong1405"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                var message = new MailMessage();
                message.To.Add(email);
                
                message.Body = body;
                message.IsBodyHtml = true;
                message.From = new MailAddress("luongnthhe151453@fpt.edu.vn");
                if(files != null)
                {
                    message.Subject = "[Invoice]";
                    try
                    {
                        foreach (var file in files)
                        {
                            Console.WriteLine(file.Value);
                            message.Attachments.Add(new Attachment(file.Key, file.Value + ".pdf"));
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    message.Subject = "[Reset Password]";
                }
                await smtp.SendMailAsync(message);
            }
        }
    }
}
