using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ListOfCompanies.WEB.Models
{
    public class EndUserViewModel: UserViewModel
    {
        [Display(Name = "Управляющий ")]
        public bool IsManager { get; set; }
    }

    public class AdminUserViewModel: UserViewModel
    {
        [Display(Name = "Активный ")]
        public bool IsActive { get; set; }

        public List<string> NamesCompanies { get; set; }

        public List<string> CountriesCompanies { get; set; }
    }

    public class UserViewModel
    {
        [HiddenInput]
        public Guid ID { get; set; }

        [Display(Name = "Логин")]
        [Required(ErrorMessage ="Поле \"Логин\" должно быть заполнено")]
        [EmailAddress]
        public string Login { get; set; }

        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Поле \"Должность\" должно быть заполнено")]
        public string Position { get; set; }

        [Display(Name = "День рождения")]
        [Required(ErrorMessage = "Поле \"День рождения\" должно быть заполнено")]
        [DataType(DataType.Date)]
      // [Range(typeof(DateTime), "01/01/1940","01/01/2001",ErrorMessage = "Проверьте дату рождения (от 01/01/1940 до 01/01/2001)")]
        [DateRange]
        public DateTime DateOfBirth { get; set; }
    }

    public class DateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            DateTime dateMin = new DateTime(1940, 1, 1);
            DateTime dateMax = new DateTime(DateTime.Now.Year - 18, 12, 31);
            if (dateMin.CompareTo(value) <= 0 && dateMax.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Проверьте дату рождения");
            }
        }
    }
}