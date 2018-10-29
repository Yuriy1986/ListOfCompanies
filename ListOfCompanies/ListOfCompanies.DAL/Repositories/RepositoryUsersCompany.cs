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

        public bool DeleteAdminUsersInCompany(Guid ID, Guid IdCompany)
        {
            Company companyCurrent = db.Companies.Include(x=>x.AdminUsers).FirstOrDefault(y => y.ID == IdCompany);
            AdminUser adminUserCurrent = db.AdminUsers.FirstOrDefault(x => x.ID == ID);
            if (companyCurrent!=null && adminUserCurrent != null)
            {
                if (companyCurrent.AdminUsers.Remove(adminUserCurrent))
                {
                    if (companyCurrent.AdminUsers.Count == 0)
                        companyCurrent.IncludesAdminUser = false;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool EditAdminUser(AdminUser adminuser, out string parametr)
        {
            AdminUser adminuserCurrent = db.AdminUsers.FirstOrDefault(x => x.ID == adminuser.ID);
            if (adminuserCurrent != null)
            {
                if (adminuserCurrent.DateOfBirth == adminuser.DateOfBirth && adminuserCurrent.IsActive == adminuser.IsActive
                    && adminuserCurrent.Login == adminuser.Login && adminuserCurrent.Position == adminuser.Position)
                {
                    parametr = "Данные не изменены";
                    return false;
                }

                AdminUser checkLogin = db.AdminUsers.FirstOrDefault(x => x.Login == adminuser.Login);
                if (checkLogin != null && checkLogin != adminuserCurrent)
                {
                    parametr = "Пользователь с таким логином уже зарегисрирован";
                    return false;
                }

                adminuserCurrent.DateOfBirth = adminuser.DateOfBirth;
                adminuserCurrent.IsActive = adminuser.IsActive;
                adminuserCurrent.Login = adminuser.Login;
                adminuserCurrent.Position = adminuser.Position;

                db.Entry(adminuserCurrent).State = EntityState.Modified;
                db.SaveChanges();
                parametr = "";
                return true;
            }
            parametr = "Пользователь не найден. Закройте это окно и обновите страницу";
            return false;
        }

    }
}
