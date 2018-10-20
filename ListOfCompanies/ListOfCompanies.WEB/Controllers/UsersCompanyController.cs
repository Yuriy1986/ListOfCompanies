using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListOfCompanies.WEB.Models;
using ListOfCompanies.BLL.DTO;
using ListOfCompanies.BLL.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace ListOfCompanies.WEB.Controllers
{
    public class UsersCompanyController : Controller
    {
        IUserCompanyService UserCompanyService;
        public UsersCompanyController(IUserCompanyService service)
        {
            UserCompanyService = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult GetUsersCompany(Guid IdCompany)
        {
            ViewBag.IdCompany = IdCompany;
            return PartialView("_GetUsersCompanyPartial");
        }

        [Authorize]
        [HttpGet]
        public string GetEndUsersCompany(Guid IdCompany)
        {
            var endUsersCompany = Mapper.Map<IEnumerable<DTOEndUserViewModel>, IEnumerable<EndUserViewModel>>(UserCompanyService.GetEndUsersCompany(IdCompany));
            return JsonConvert.SerializeObject(endUsersCompany);
        }

        [Authorize]
        [HttpGet]
        public string GetAdminUsersCompany(Guid IdCompany)
        {
            var adminUsersCompany = Mapper.Map<IEnumerable<DTOAdminUserViewModel>, IEnumerable<AdminUserViewModel>>(UserCompanyService.GetAdminUsersCompany(IdCompany));
            return JsonConvert.SerializeObject(adminUsersCompany);
        }
// .................................................................................................................................
        [Authorize]
        [HttpPost]
        public string EditEndUsers(EndUserViewModel model, Guid IdCompany)
        {
            // действия по редактированию
            return "EditEndUsers";
        }

        [Authorize]
        [HttpPost]        
        public string CreateEndUsers([Bind(Exclude = "ID")] EndUserViewModel model, Guid IdCompany)
        {
            if(ModelState.IsValid)
            {

                return null;
            }

            StringBuilder errors = new StringBuilder();
            foreach (var item in ModelState)
            {
                if(item.Value.Errors!=null)
                {
                    foreach (var er in item.Value.Errors)
                        errors.Append(er.ErrorMessage+"\n");
                }
            }
            return errors.ToString();
        }

        [Authorize]
        [HttpPost]
        public bool DeleteEndUsers(Guid ID)
        {
            return UserCompanyService.DeleteEndUser(ID);
        }

    }
}
