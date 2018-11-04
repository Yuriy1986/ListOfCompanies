using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.BLL.DTO
{
    public class DTOEndUserViewModel : DTOUserViewModel
    {
        public bool IsManager { get; set; }
    }

    public class DTOAdminUserViewModel : DTOUserViewModel
    {
        public bool IsActive { get; set; }

        public List<string> NamesCompanies { get; set; }

        public List<string> CountriesCompanies { get; set; }
    }

    public class DTOUserViewModel
    {
        public Guid ID { get; set; }

        public string Login { get; set; }

        public string Position { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
