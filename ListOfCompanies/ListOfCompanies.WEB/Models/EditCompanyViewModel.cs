using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ListOfCompanies.WEB.Models
{
    public class EditCompanyViewModel
    {
        [Required]
        public Guid IdCompany { get; set; }

        [Required]
        public int Page { get; set; }
    }
}