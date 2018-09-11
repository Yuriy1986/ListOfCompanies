﻿using ListOfCompanies.BLL.DTO;
using ListOfCompanies.DAL.Entities;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using ListOfCompanies.BLL.Interfaces;
using ListOfCompanies.BLL.Infrastructure;
using ListOfCompanies.DAL.Interfaces;
using System;
using System.Linq;
using System.Security.Principal;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;

namespace ListOfCompanies.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
            var provider = new DpapiDataProtectionProvider("ASP.NET Identity");
            Database.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            //Database.UserManager.EmailService = new EmailService();
        }

        public async Task<OperationDetails> Register(DTORegisterViewModel dtoRegisterViewModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(dtoRegisterViewModel.Email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = dtoRegisterViewModel.Email, Email = dtoRegisterViewModel.Email};

                var password = new UserPasswordValidator();
                var validatePassword = password.ValidateAsync(dtoRegisterViewModel.Password).Result;
                if (validatePassword.Errors.Count() > 0)
                    return new OperationDetails(false, validatePassword.Errors.FirstOrDefault(), "");

                var result = await Database.UserManager.CreateAsync(user, dtoRegisterViewModel.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");

                var code = await Database.UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                return new OperationDetails(true, user.Id, code);
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином (Email) уже существует", "Email");
            }
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string code)
        {
            return await Database.UserManager.ConfirmEmailAsync(userId, code);
        }

        public async Task SendEmailAsync(string userId, string subject, string body)
        {
            Database.UserManager.EmailService = new EmailService();

            await Database.UserManager.SendEmailAsync(userId, subject, body);
        }

        public async Task<ClaimsIdentity> Login(DTOLoginViewModel dtoLoginViewModel)
        {
            ClaimsIdentity claim = null;
            var user = await Database.UserManager.FindAsync(dtoLoginViewModel.Email, dtoLoginViewModel.Password);
            if (user != null && user.EmailConfirmed)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public async Task<OperationDetails> ForgotPassword(DTOForgotPasswordViewModel dtoForgotPasswordViewModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(dtoForgotPasswordViewModel.Email);
            if (user == null || !(await Database.UserManager.IsEmailConfirmedAsync(user.Id)))
                return new OperationDetails(false, "", "");
            else
            {
                var code = await Database.UserManager.GeneratePasswordResetTokenAsync(user.Id);
                return new OperationDetails(true, user.Id, code);
            }
        }

        public async Task<OperationDetails> ResetPassword(DTOResetPasswordViewModel dtoResetPasswordViewModel)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(dtoResetPasswordViewModel.Email);
            if (user == null)
                return new OperationDetails(false, "", "");
            else
                return new OperationDetails(true, user.Id, "");
        }

        public async Task<OperationDetails> ResetPasswordAsync(string userId, string code, string _password)
        {
            var password = new UserPasswordValidator();
            var validatePassword = password.ValidateAsync(_password).Result;
            if (validatePassword.Errors.Count() > 0)
                return new OperationDetails(false, validatePassword.Errors.FirstOrDefault(), "");

            var result = await Database.UserManager.ResetPasswordAsync(userId, code, _password);
            if (result.Errors.Count() > 0)
                return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
            else
                return new OperationDetails(true, "", "");
        }

        public async Task<OperationDetails> ChangePassword(DTOChangePasswordViewModel changePasswordViewModelDto)
        {
            var password = new UserPasswordValidator();
            var validatePassword = password.ValidateAsync(changePasswordViewModelDto.NewPassword).Result;
            if (validatePassword.Errors.Count() > 0)
                return new OperationDetails(false, validatePassword.Errors.FirstOrDefault(), "");

            var result = await Database.UserManager.ChangePasswordAsync(changePasswordViewModelDto.UserId, changePasswordViewModelDto.OldPassword,
                changePasswordViewModelDto.NewPassword);

            if (result.Succeeded)
            {
                return new OperationDetails(true, "", "");
            }
            return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
        }

        public async Task<ClaimsIdentity> GetClaim(string userId)
        {
            ClaimsIdentity claim = null;
            var user = await Database.UserManager.FindByIdAsync(userId);
            if (user != null && user.EmailConfirmed)
            {
                claim = await Database.UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
