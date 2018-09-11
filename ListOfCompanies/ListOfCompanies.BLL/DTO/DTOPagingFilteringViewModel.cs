using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListOfCompanies.BLL.DTO
{
    public class DTOPagingFilteringViewModel
    {
        public bool? SortByname { get; set; }

        public bool? SortBycountry { get; set; }

        public bool IncludesAdmins { get; set; }

        public string SearchByName { get; set; }
    }
}
