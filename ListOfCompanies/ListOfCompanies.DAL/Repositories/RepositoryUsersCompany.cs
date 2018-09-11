using ListOfCompanies.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ListOfCompanies.DAL.EF;
using ListOfCompanies.DAL.Entities;

namespace ListOfCompanies.DAL.Repositories
{
    public class RepositoryUsersCompany : IRepositoryUsersCompany
    {
        ListOfCompaniesContext db;

        public RepositoryUsersCompany(ListOfCompaniesContext context)
        {
            this.db = context;
        }

        public IEnumerable<EndUser> GetEndUsersCompany(Guid IdCompany)  
        {
            return db.EndUsers.Where(x => x.CompanyID == IdCompany);
        }

        public IEnumerable<AdminUser> GetAdminUsersCompany(Guid IdCompany)  
        {
            return db.AdminUsers.Where(x => x.Companies.Contains(db.Companies.FirstOrDefault(y => y.ID == IdCompany)));
        }

    }
}
