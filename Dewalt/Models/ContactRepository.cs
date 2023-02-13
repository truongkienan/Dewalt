using System.Data;
using System.Net;
using System.Net.Mail;

namespace Dewalt.Models
{
    public class ContactRepository:BaseRepository
    {        

        public ContactRepository(IDbConnection connection) : base(connection)
        {
        }

        public void Contact(Message obj)
        {            
            using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("truongkienan2@gmail.com", "semjgdufylmdgocq"),
                EnableSsl = true
            })
            {
                MailMessage message = new MailMessage(new MailAddress("truongkienan2@gmail.com",
                                        "Dewalt"), new MailAddress(obj.EmailTo))
                {
                    Subject = obj.Subject,
                    Body = obj.Body
                };
                client.Send(message);
            }
        }
    }
}
