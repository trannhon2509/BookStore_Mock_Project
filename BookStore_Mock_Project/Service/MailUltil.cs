using System.Net;
using System.Net.Mail;

namespace BookStore_Mock_Project.Service
{
    public class MailUltil
    {
        /// <summary>
        /// Gửi email sử dụng máy chủ SMTP Google (smtp.gmail.com)
        /// </summary>
        public static async Task<bool> SendMailGoogleSmtp(string _from, string _to, string _subject,
                                                            string _body, string _gmailsend, string _gmailpassword)
        {

            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            // Tạo SmtpClient kết nối đến smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {
                client.Port = 587;
                client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                client.EnableSsl = true;

                try
                {
                    // Send the email asynchronously
                    await client.SendMailAsync(message);
                    return true; // Return true if email sent successfully
                }
                catch (Exception ex)
                {
                    // Log or handle the exception accordingly
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    return false; // Return false if there was an error sending the email
                }
            }
        }
    }
}
