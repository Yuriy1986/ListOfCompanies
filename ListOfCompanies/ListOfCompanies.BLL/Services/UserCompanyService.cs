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

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
