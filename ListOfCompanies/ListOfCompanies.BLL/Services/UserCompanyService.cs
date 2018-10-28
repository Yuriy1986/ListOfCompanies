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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
