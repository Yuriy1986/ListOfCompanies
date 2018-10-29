﻿using System;
using AutoMapper;
using ListOfCompanies.BLL.DTO;
using ListOfCompanies.DAL.Entities;
using ListOfCompanies.WEB.Models;

namespace ListOfCompanies.WEB.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(
                cfg =>
                {
                    //BLL.
                    // CompanyService / GetCompanies, GetCompany.
                    cfg.CreateMap<Company, DTOCompanyViewModel>();

                    // CompanyService / Edit_CreateCompanyConfirm
                    cfg.CreateMap<DTOCompanyViewModel,Company>();

                    // UserCompanyService / GetEndUsersCompany.
                    cfg.CreateMap<EndUser, DTOEndUserViewModel>();

                    // UserCompanyService / GetAdminUsersCompany.
                    cfg.CreateMap<AdminUser, DTOAdminUserViewModel>();

                    // UserCompanyService / CreateEndUser, EditEndUsers.
                    cfg.CreateMap<DTOEndUserViewModel, EndUser>();

                    // UserCompanyService / EditAdminUser.
                    cfg.CreateMap<DTOAdminUserViewModel, AdminUser>();

                    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //WEB.
                    // Account / Login.
                    cfg.CreateMap<LoginViewModel, DTOLoginViewModel>();

                    // Account / Register.
                    cfg.CreateMap<RegisterViewModel, DTORegisterViewModel>();

                    // Account / ForgotPassword.
                    cfg.CreateMap<ForgotPasswordViewModel, DTOForgotPasswordViewModel>();

                    // Account / ResetPassword.
                    cfg.CreateMap<ResetPasswordViewModel, DTOResetPasswordViewModel>();

                    // Manage/ChangePassword.
                    cfg.CreateMap<ChangePasswordViewModel, DTOChangePasswordViewModel>();

                    // Home / Index.
                    cfg.CreateMap<DTOCompanyViewModel, CompanyViewModel>();
                    cfg.CreateMap<PagingFilteringViewModel,DTOPagingFilteringViewModel>();

                    // Home / EditCompany.
                    cfg.CreateMap<DTOCompanyViewModel, CompanyViewModel>();

                    // UsersCompany / GetEndUsersCompany, CreateEndUsers, EditEndUsers
                    cfg.CreateMap<DTOEndUserViewModel, EndUserViewModel>();

                    // UsersCompany / GetAdminUsersCompany, EditAdminUsers
                    cfg.CreateMap<DTOAdminUserViewModel, AdminUserViewModel>();

               
                }
                );
        }
    }
}