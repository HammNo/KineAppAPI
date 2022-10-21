using System.Net.Mail;

namespace KineApp.BLL.Interfaces
{
    public interface IMailer
    {
        Task Send(string subject, string message, Attachment attachment, params string[] to);
        Task Send(string subject, string message, params string[] to);
    }
}
