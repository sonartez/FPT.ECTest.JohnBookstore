using FPT.ECTest.JohnBookstore.Application.DTOs.Email;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
