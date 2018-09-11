using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.BLL.DTO;
using ListOfCompanies.BLL.Infrastructure;

namespace ListOfCompanies.BLL.Interfaces
{
   public interface ICompanyService:IDisposable
    {
        IEnumerable<DTOCompanyViewModel> GetCompanies(DTOPagingFilteringViewModel model, ref int page, int pagesize, out int totalPages);// ////////////////////////////////

        bool DeleteCompany(Guid IdCompany);

        DTOCompanyViewModel GetCompany(Guid IdCompany);

        bool Edit_CreateCompanyConfirm(DTOCompanyViewModel model, out string parametr);
    }
}
