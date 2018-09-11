using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.DAL.Entities
{
    [Table("EndUsers")]
    public class EndUser:User
    {
        public bool IsManager { get; set; }

        public Company Company { get; set; }

        public Guid CompanyID { get; set; }
    }
}
