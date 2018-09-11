using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ListOfCompanies.BLL.DTO;
using ListOfCompanies.BLL.Infrastructure;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace ListOfCompanies.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        // AccountController.

        Task<OperationDetails> Register(DTORegisterViewModel dtoRegisterViewModel);

        Task<ClaimsIdentity> Login(DTOLoginViewModel dtoLoginViewModel);

        Task<IdentityResult> ConfirmEmailAsync(string userId, string code);

        Task SendEmailAsync(string userId, string subject, string body);

        Task<OperationDetails> ForgotPassword(DTOForgotPasswordViewModel dtoForgotPasswordViewModel);

        Task<OperationDetails> ResetPassword(DTOResetPasswordViewModel dtoResetPasswordViewModel);

        Task<OperationDetails> ResetPasswordAsync(string userId, string code, string _password);

        // ManageController.

        Task<OperationDetails> ChangePassword(DTOChangePasswordViewModel changePasswordViewModelDto);

        Task<ClaimsIdentity> GetClaim(string userId);
    }
}
