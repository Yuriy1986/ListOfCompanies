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

        // Get all adminUsers becides current company.
        IEnumerable<AdminUser> GetAdminUsers(Guid IdCompany);

        bool DeleteEndUser(Guid ID);

        bool CreateEndUser(EndUser enduser, Guid IdCompany, out string parametr);

        bool EditEndUser(EndUser enduser, out string parametr);

        bool DeleteAdminUsersInCompany(Guid ID, Guid IdCompany);

        bool EditAdminUser(AdminUser adminuser, out string parametr);

        IEnumerable<AdminUser> CreateAdminUsersInCompany(Guid[] idUsers, Guid IdCompany);

        // _GetAllAdminUsersPartial.
        IEnumerable<AdminUser> GetAllAdminUsersDetails();

        bool CreateAdminUser(AdminUser adminuser, out string parametr);

        bool DeleteAdminUser(Guid ID);
    }
}
