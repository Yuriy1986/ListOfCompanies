using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ListOfCompanies.DAL.EF;
using ListOfCompanies.DAL.Entities;
using ListOfCompanies.DAL.Interfaces;
using ListOfCompanies.DAL.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ListOfCompanies.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        ListOfCompaniesContext db;

        ApplicationUserManager userManager;

        RepositoryCompanies repositoryCompanies;

        RepositoryUsersCompany repositoryUsersCompany;

        public UnitOfWork(string connectionString)
        {
            db = new ListOfCompaniesContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IRepositoryCompanies Companies
        {
            get
            {
                if (repositoryCompanies == null)
                    repositoryCompanies = new RepositoryCompanies(db);
                return repositoryCompanies;
            }
        }

        public IRepositoryUsersCompany UsersCompany
        {
            get
            {
                if (repositoryUsersCompany == null)
                    repositoryUsersCompany = new RepositoryUsersCompany(db);
                return repositoryUsersCompany;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}
