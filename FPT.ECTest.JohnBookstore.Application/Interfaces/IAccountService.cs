using FPT.ECTest.JohnBookstore.Application.DTOs.Account;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);
    }
}
