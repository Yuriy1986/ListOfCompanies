using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.DAL.Identity;
using ListOfCompanies.DAL.Entities;

namespace ListOfCompanies.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepositoryCompanies Companies { get; }

        IRepositoryUsersCompany UsersCompany { get; }

        ApplicationUserManager UserManager { get; }
    }
}
