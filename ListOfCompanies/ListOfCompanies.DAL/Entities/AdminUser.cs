using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.DAL.Entities
{
    [Table("AdminUsers")]
    public class AdminUser:User
    {
        public bool IsActive { get; set; }

        public ICollection<Company> Companies { get; set; }

        public AdminUser()
        {
            Companies = new List<Company>();
        }
    }
}
