using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.DAL.Entities;

namespace ListOfCompanies.DAL.Interfaces
{
    public interface IRepositoryCompanies
    {
        IEnumerable<Company> GetCompanies(bool? SortByname, bool? SortBycountry, bool IncludesAdmins, string SearchByName, ref int page, int pagesize, out int totalPages);

        bool DeleteCompany(Guid IdCompany);

        Company GetCompany(Guid IdCompany);

        bool Edit_CreateCompanyConfirm(Company company, out string parametr);
    }
}
