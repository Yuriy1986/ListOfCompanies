using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListOfCompanies.DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace ListOfCompanies.DAL.EF
{
    public class ListOfCompaniesContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<User> UsersBase { get; set; }

        public DbSet<AdminUser> AdminUsers { get; set; }

        public DbSet<EndUser> EndUsers { get; set; }

        static ListOfCompaniesContext()
        {
            Database.SetInitializer<ListOfCompaniesContext>(new UserDbInitializer());
        }

        public ListOfCompaniesContext(string conectionString) : base(conectionString)
        {

        }
    }

    public class UserDbInitializer : DropCreateDatabaseIfModelChanges<ListOfCompaniesContext>
    {
        Random rand = new Random();

        List<AdminUser> adminUserList = new List<AdminUser>(1500);

        Guid id;

        int endUserCount;

        int adminUserCount;

        enum Countries
        {
            Ukraine,
            USA,
            Poland,
            Russia,
            Germany
        }

        enum Position
        {
            director,
            manager,
            programmer,
            booker
        }

        DateTime RandomDate()
        {
            int year = rand.Next(1940, 2001);
            int month = rand.Next(1, 13);
            int day = rand.Next(1, 32);
            DateTime res;

            while (true)
            {
                if (DateTime.TryParse(year + "-" + month + "-" + day, out res))
                    return res;
                day--;
            }
        }

        protected override void Seed(ListOfCompaniesContext context)
        {
            for (char i = 'a'; i <= '~'; i++)
            {
                context.Companies.Add(new Company
                {
                    ID = Guid.NewGuid(),
                    Name = "Company " + i,
                    Country = ((Countries)rand.Next(5)).ToString(),
                    IncludesAdminUser = true
                });
            }

            context.SaveChanges();

            foreach (var item in context.Companies)
            {
                endUserCount = rand.Next(5, 51);

                for (int i = 0; i < endUserCount; i++)
                {
                    id = Guid.NewGuid();
                    context.EndUsers.Add(new EndUser
                    {
                        IsManager = rand.Next(2) == 1 ? true : false,
                        CompanyID = item.ID,
                        ID = id,
                        Login = "loginE" + id + "@gmail.com",
                        Position = ((Position)rand.Next(4)).ToString(),
                        DateOfBirth = RandomDate()
                    });
                }
            }

            for (int i = 0; i < adminUserList.Capacity; i++)
            {
                id = Guid.NewGuid();
                adminUserList.Add((new AdminUser
                {
                    ID = id,
                    Login = "loginA" + id + "@gmail.com",
                    Position = ((Position)rand.Next(4)).ToString(),
                    DateOfBirth = RandomDate(),
                    IsActive = rand.Next(2) == 1 ? true : false
                }));
            }


            int temp;

            foreach (var item in context.Companies)
            {
                adminUserCount = rand.Next(5, 51);

                for (int i = 0; i < adminUserCount;)
                {
                    temp = rand.Next(adminUserList.Capacity);
                    if (adminUserList[temp].Companies.Contains(item))
                        continue;
                    adminUserList[temp].Companies.Add(item);
                    i++;
                }
            }

            adminUserList.RemoveAll(x => x.Companies.Count == 0);

            context.AdminUsers.AddRange(adminUserList);
        }
    }
}

