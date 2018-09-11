using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ListOfCompanies.DAL.Interfaces;
using ListOfCompanies.DAL.Repositories;

namespace ListOfCompanies.BLL.Infrastructure
{
    public class ServiceModule : Module
    {
        public ServiceModule(string connection, ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameter("connectionString", connection);
        }
    }
}
