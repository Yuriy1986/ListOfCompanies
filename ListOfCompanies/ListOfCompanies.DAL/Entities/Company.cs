using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.DAL.Entities
{
    public class Company
    {
        public Guid ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }
        
        public bool IncludesAdminUser { get; set; }

        public ICollection<EndUser> EndUsers { get; set; }

        public ICollection<AdminUser> AdminUsers { get; set; }

        public Company()
        {
            EndUsers = new List<EndUser>();
            AdminUsers = new List<AdminUser>();
        }
    }
}
