using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListOfCompanies.WEB.Models
{
    public class PagingFilteringViewModel
    {
        public int Page { get; set; } = 1;

        public bool? SortByname { get; set; } = null;

        public bool? SortBycountry { get; set; } = null;

        public bool IncludesAdmins { get; set; } = false;

        public string SearchByName { get; set; } = null;
    }
}