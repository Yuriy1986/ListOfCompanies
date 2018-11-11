using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.BLL.Interfaces;
using ListOfCompanies.BLL.Infrastructure;
using ListOfCompanies.DAL.Interfaces;
using ListOfCompanies.BLL.DTO;
using AutoMapper;
using ListOfCompanies.DAL.Entities;

namespace ListOfCompanies.BLL.Services
{
    public class UserCompanyService : IUserCompanyService
    {
        IUnitOfWork Database { get; set; }

        public UserCompanyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DTOEndUserViewModel> GetEndUsersCompany(Guid IdCompany)
        {
            return Mapper.Map<IEnumerable<EndUser>, IEnumerable<DTOEndUserViewModel>>(Database.UsersCompany.GetEndUsersCompany(IdCompany));
        }

        public IEnumerable<DTOAdminUserViewModel> GetAdminUsersCompany(Guid IdCompany)
        {
            return Mapper.Map<IEnumerable<AdminUser>, IEnumerable<DTOAdminUserViewModel>>(Database.UsersCompany.GetAdminUsersCompany(IdCompany));
        }

        // Get all adminUsers becides current company.
        public IEnumerable<DTOAdminUserViewModel> GetAdminUsers(Guid IdCompany)
        {
            return Mapper.Map<IEnumerable<AdminUser>, IEnumerable<DTOAdminUserViewModel>>(Database.UsersCompany.GetAdminUsers(IdCompany));
        }

        public bool DeleteEndUser(Guid ID)
        {
            return Database.UsersCompany.DeleteEndUser(ID);
        }

        public bool CreateEndUser(DTOEndUserViewModel model, Guid IdCompany, out string parametr)
        {
            var enduser = Mapper.Map<EndUser>(model);
            return Database.UsersCompany.CreateEndUser(enduser, IdCompany, out parametr);
        }

        public bool EditEndUser(DTOEndUserViewModel model, out string parametr)
        {
            var enduser = Mapper.Map<EndUser>(model);
            return Database.UsersCompany.EditEndUser(enduser, out parametr);
        }

        public bool DeleteAdminUsersInCompany(Guid ID, Guid IdCompany)
        {
            return Database.UsersCompany.DeleteAdminUsersInCompany(ID, IdCompany);
        }

        public bool EditAdminUser(DTOAdminUserViewModel model, out string parametr)
        {
            var adminuser = Mapper.Map<AdminUser>(model);
            return Database.UsersCompany.EditAdminUser(adminuser, out parametr);
        }

        public IEnumerable<DTOAdminUserViewModel> CreateAdminUsersInCompany(Guid[] idUsers, Guid IdCompany)
        {
            return Mapper.Map<IEnumerable<AdminUser>, IEnumerable<DTOAdminUserViewModel>>(Database.UsersCompany.CreateAdminUsersInCompany(idUsers, IdCompany));
        }

        // _GetAllAdminUsersPartial.
        public IEnumerable<DTOAdminUserViewModel> GetAllAdminUsersDetails()
        {
            return Mapper.Map<IEnumerable<AdminUser>, IEnumerable<DTOAdminUserViewModel>>(Database.UsersCompany.GetAllAdminUsersDetails());
        }

        public bool CreateAdminUser(DTOAdminUserViewModel model, out string parametr)
        {
            var adminuser = Mapper.Map<AdminUser>(model);
            return Database.UsersCompany.CreateAdminUser(adminuser, out parametr);
        }

        public bool DeleteAdminUser(Guid ID)
        {
            return Database.UsersCompany.DeleteAdminUser(ID);
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
