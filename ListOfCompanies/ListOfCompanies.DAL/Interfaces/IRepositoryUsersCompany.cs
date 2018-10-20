using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.DAL.Entities;

namespace ListOfCompanies.DAL.Interfaces
{
   public interface IRepositoryUsersCompany
    {
        IEnumerable<EndUser> GetEndUsersCompany(Guid IdCompany);

        IEnumerable<AdminUser> GetAdminUsersCompany(Guid IdCompany);

        bool DeleteEndUser(Guid ID);
    }
}
