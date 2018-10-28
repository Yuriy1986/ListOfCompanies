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

        public bool DeleteEndUser(Guid ID)
        {
            EndUser enduserCurrent = db.EndUsers.FirstOrDefault(x => x.ID == ID);
            if (enduserCurrent != null)
            {
                db.EndUsers.Remove(enduserCurrent);
                db.Entry(enduserCurrent).State = EntityState.Deleted;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool CreateEndUser(EndUser enduser, Guid IdCompany, out string parametr)
        {
            EndUser enduserCurrent = db.EndUsers.FirstOrDefault(x => x.Login == enduser.Login);
            if (enduserCurrent == null)
            {
                enduser.CompanyID = IdCompany;
                enduser.ID = Guid.NewGuid();
                db.EndUsers.Add(enduser);
                db.Entry(enduser).State = EntityState.Added;
                db.SaveChanges();
                parametr = enduser.ID.ToString();
                return true;
            }
            parametr = "Пользователь с таким логином уже зарегисрирован";
            return false;
        }

        public bool EditEndUser(EndUser enduser, out string parametr)
        {
            EndUser enduserCurrent = db.EndUsers.FirstOrDefault(x => x.ID == enduser.ID);
            if (enduserCurrent != null)
            {
                if(enduserCurrent.DateOfBirth== enduser.DateOfBirth && enduserCurrent.IsManager == enduser.IsManager
                    && enduserCurrent.Login == enduser.Login && enduserCurrent.Position == enduser.Position)
                {
                    parametr = "Данные не изменены";
                    return false;
                }
                
                EndUser checkLogin = db.EndUsers.FirstOrDefault(x => x.Login == enduser.Login);
                if (checkLogin != null && checkLogin!= enduserCurrent)
                {
                    parametr = "Пользователь с таким логином уже зарегисрирован";
                    return false;
                }

                enduserCurrent.DateOfBirth = enduser.DateOfBirth;
                enduserCurrent.IsManager = enduser.IsManager;
                enduserCurrent.Login = enduser.Login;
                enduserCurrent.Position = enduser.Position;
                
                db.Entry(enduserCurrent).State = EntityState.Modified;
                db.SaveChanges();
                parametr = "";
                return true;
            }
            parametr = "Пользователь не найден. Закройте это окно и обновите страницу";
            return false;
        }
    }
}
