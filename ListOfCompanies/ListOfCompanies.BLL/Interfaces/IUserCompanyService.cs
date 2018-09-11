using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.BLL.DTO;

namespace ListOfCompanies.BLL.Interfaces
{
    public interface IUserCompanyService : IDisposable
    {
        IEnumerable<DTOEndUserViewModel> GetEndUsersCompany(Guid IdCompany);

        IEnumerable<DTOAdminUserViewModel> GetAdminUsersCompany(Guid IdCompany);
    }
}
