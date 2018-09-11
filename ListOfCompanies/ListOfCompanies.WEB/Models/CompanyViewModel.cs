using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ListOfCompanies.WEB.Models
{
    public class CompanyViewModel
    {
        [HiddenInput]
        public Guid ID { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Поле названия компании должно быть заполнено")]
        public string Name { get; set; }

        [Display(Name = "Страна")]
        [Required(ErrorMessage = "Поле страна компании должно быть заполнено")]
        public string Country { get; set; }

    }
}