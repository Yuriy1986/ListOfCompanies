using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using ListOfCompanies.BLL.Interfaces;
using ListOfCompanies.BLL.Services;

namespace ListOfCompanies.WEB.Util
{
    public class AutofacModule : Module
    {
        public AutofacModule(ContainerBuilder builder)
        {
            builder.RegisterType<UserService>().As<IUserService>();

            builder.RegisterType<CompanyService>().As<ICompanyService>();

            builder.RegisterType<UserCompanyService>().As<IUserCompanyService>();
        }
    }
}