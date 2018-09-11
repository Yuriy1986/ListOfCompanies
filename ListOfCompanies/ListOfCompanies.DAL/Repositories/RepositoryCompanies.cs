using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ListOfCompanies.DAL.EF;
using ListOfCompanies.DAL.Entities;
using ListOfCompanies.DAL.Interfaces;

namespace ListOfCompanies.DAL.Repositories
{
    public class RepositoryCompanies : IRepositoryCompanies
    {
        ListOfCompaniesContext db;

        public RepositoryCompanies(ListOfCompaniesContext context)
        {
            this.db = context;
        }

        public bool DeleteCompany(Guid IdCompany)
        {
            Company companyCurrent = db.Companies.FirstOrDefault(x => x.ID == IdCompany);
            if (companyCurrent != null)
            {
                db.Companies.Remove(companyCurrent);
                db.Entry(companyCurrent).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public IEnumerable<Company> GetCompanies(bool? SortByname, bool? SortBycountry, bool IncludesAdmins, string SearchByName, ref int page, int pageSize, out int totalPages)
        {
            IQueryable<Company> query = db.Companies;

            if (IncludesAdmins || SearchByName != null)
            {
                if (IncludesAdmins)
                    query = query.Where(x => x.IncludesAdminUser);
                if (SearchByName != null)
                    query = query.Where(x => x.Name.Contains(SearchByName));
            }

            if (SortByname != null && SortBycountry != null)
            {
                if (SortByname == true && SortBycountry==true)
                    query = query.OrderBy(x => x.Name).ThenBy(x=>x.Country);
                else if (SortByname == false && SortBycountry == false)
                    query = query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Country);
                else if (SortByname == true && SortBycountry == false)
                    query = query.OrderBy(x => x.Name).ThenByDescending(x => x.Country);
                else if (SortByname == false && SortBycountry == true)
                    query = query.OrderByDescending(x => x.Name).ThenBy(x => x.Country);
            }
            else
            {
                if (SortByname != null)
                {
                    if (SortByname == true)
                        query = query.OrderBy(x => x.Name);
                    else
                        query = query.OrderByDescending(x => x.Name);
                }
                else if (SortBycountry != null)
                {
                    if (SortBycountry == true)
                        query = query.OrderBy(x => x.Country);
                    else
                        query = query.OrderByDescending(x => x.Country);
                }
            }
            
            var selection = query.ToList();

            totalPages = (int)Math.Ceiling((decimal)query.Count() / pageSize);

            if (page < 1)
                page = 1;

            if (page > totalPages)
                page = totalPages;

            return selection.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public Company GetCompany(Guid IdCompany)
        {
            return db.Companies.Find(IdCompany);
        }

        public bool Edit_CreateCompanyConfirm(Company company, out string parametr)
        {
            if (company.ID.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                // Create.
                company.ID = Guid.NewGuid();
                db.Companies.Add(company);
                db.Entry(company).State = EntityState.Added;
                db.SaveChanges();
                parametr = company.ID.ToString();
                return true;
            }
            else
            {
                // Edit.
                Company companycurrent = db.Companies.FirstOrDefault(x => x.ID == company.ID);
                if (companycurrent != null)
                {
                    if (companycurrent.Name == company.Name && companycurrent.Country == company.Country)
                    {
                        parametr = "компания не изменена";
                        return false;
                    }

                    companycurrent.Name = company.Name;
                    companycurrent.Country = company.Country;
                    db.Entry(companycurrent).State = EntityState.Modified;
                    db.SaveChanges();
                    parametr = companycurrent.ID.ToString();
                    return true;
                }
                parametr = "компания не найдена";
                return false;
            }
        }

    }
}
