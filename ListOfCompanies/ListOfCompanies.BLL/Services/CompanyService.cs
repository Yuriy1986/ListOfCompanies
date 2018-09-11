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
    public class CompanyService : ICompanyService
    {
        IUnitOfWork Database { get; set; }

        public CompanyService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<DTOCompanyViewModel> GetCompanies(DTOPagingFilteringViewModel model, ref int page, int pageSize, out int totalPages)
        {
            return Mapper.Map<IEnumerable<Company>, IEnumerable<DTOCompanyViewModel>>(Database.Companies.GetCompanies(model.SortByname,
                model.SortBycountry,model.IncludesAdmins,model.SearchByName, ref page, pageSize, out totalPages));
        }

        public bool DeleteCompany(Guid IdCompany)
        {
            return Database.Companies.DeleteCompany(IdCompany);
        }

        public DTOCompanyViewModel GetCompany(Guid IdCompany)
        {
            return Mapper.Map<DTOCompanyViewModel>(Database.Companies.GetCompany(IdCompany));
        }

        public bool Edit_CreateCompanyConfirm(DTOCompanyViewModel model, out string parametr)
        {
            var company = Mapper.Map<Company>(model);
            return Database.Companies.Edit_CreateCompanyConfirm(company, out parametr);
        }


        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
