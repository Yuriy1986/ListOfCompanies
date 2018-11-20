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

        // Get all adminUsers becides current company.
        [Authorize]
        [HttpGet]
        public string GetAdminUsers(Guid IdCompany)
        {
            var adminUsers = Mapper.Map<IEnumerable<DTOAdminUserViewModel>, IEnumerable<AdminUserViewModel>>(UserCompanyService.GetAdminUsers(IdCompany));
            return JsonConvert.SerializeObject(adminUsers);
        }

        [Authorize]
        [HttpPost]
        public string EditEndUsers(EndUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Login = model.Login.Trim();
                model.Position = model.Position.Trim();
                var endUserViewModelDto = Mapper.Map<DTOEndUserViewModel>(model);
                string parametr;

                if (UserCompanyService.EditEndUser(endUserViewModelDto, out parametr))
                    return JsonConvert.SerializeObject(model);
                return JsonConvert.SerializeObject(parametr);
            }
            return ErrorsUsers(ModelState);
        }

        [Authorize]
        [HttpPost]
        public string CreateEndUsers([Bind(Exclude = "ID")] EndUserViewModel model, Guid IdCompany)
        {
            if (ModelState.IsValid)
            {
                model.Login = model.Login.Trim();
                model.Position = model.Position.Trim();
                var endUserViewModelDto = Mapper.Map<DTOEndUserViewModel>(model);
                string parametr;

                if (UserCompanyService.CreateEndUser(endUserViewModelDto, IdCompany, out parametr))
                {
                    model.ID = Guid.Parse(parametr);
                    return JsonConvert.SerializeObject(model);
                }
                return JsonConvert.SerializeObject(parametr);
            }
            return ErrorsUsers(ModelState);
        }

        private string ErrorsUsers(ModelStateDictionary modelState)
        {
            StringBuilder errors = new StringBuilder();

            foreach (var item in modelState)
            {
                if (item.Value.Errors != null)
                {
                    foreach (var er in item.Value.Errors)
                        errors.Append(er.ErrorMessage + "<br/>");
                }
            }
            return JsonConvert.SerializeObject(errors.ToString());
        }

        [Authorize]        
        [HttpPost]
        public bool DeleteEndUsers(Guid ID)
        {
            return UserCompanyService.DeleteEndUser(ID);
        }

        [Authorize]
        [HttpPost]
        public bool DeleteAdminUsersInCompany(Guid ID, Guid IdCompany)
        {
            return UserCompanyService.DeleteAdminUsersInCompany(ID, IdCompany);
        }

        [Authorize]
        [HttpPost]
        public string EditAdminUsers(AdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Login = model.Login.Trim();
                model.Position = model.Position.Trim();
                var adminUserViewModelDto = Mapper.Map<DTOAdminUserViewModel>(model);
                string parametr;

                if (UserCompanyService.EditAdminUser(adminUserViewModelDto, out parametr))
                    return JsonConvert.SerializeObject(model);
                return JsonConvert.SerializeObject(parametr);
            }
            return ErrorsUsers(ModelState);
        }


        [Authorize]
        [HttpPost]
        public string CreateAdminUsersInCompany(string id, Guid IdCompany)
        {
            if (id != null)
            {
                Guid[] idUsers = JsonConvert.DeserializeObject<Guid[]>(id);
                var newAdminUsersInCompany = UserCompanyService.CreateAdminUsersInCompany(idUsers, IdCompany);
                if (newAdminUsersInCompany != null)
                {
                    return JsonConvert.SerializeObject(newAdminUsersInCompany);
                }
            }
            return null;
        }
// ////////////////////////////////////////////////////////////////////////////////////////////////
        // _GetAllAdminUsersPartial.
        [Authorize]
        [HttpGet]
        public ActionResult GetAllAdminUsersView()
        {
            return PartialView("_GetAllAdminUsersPartial");
        }

        [Authorize]
        [HttpGet]
        public string GetAllAdminUsersDetails()
        {
            var adminUsersAll = Mapper.Map<IEnumerable<DTOAdminUserViewModel>, IEnumerable<AdminUserViewModel>>(UserCompanyService.GetAllAdminUsersDetails());

            foreach (var item in adminUsersAll)
            {
                if (item.CountriesCompanies.Count > 1)
                {
                    for (int i = 1; i < item.CountriesCompanies.Count; i++)
                    {
                        item.CountriesCompanies[0] +=",\n" + item.CountriesCompanies[i];
                        item.NamesCompanies[0] += ",\n" + item.NamesCompanies[i];
                    }
                    item.CountriesCompanies.RemoveRange(1, item.CountriesCompanies.Count - 1);
                    item.NamesCompanies.RemoveRange(1, item.NamesCompanies.Count - 1);
                }
            }
                return JsonConvert.SerializeObject(adminUsersAll);
        }

        [Authorize]
        [HttpPost]
        public string CreateAdminUsers([Bind(Exclude = "ID")] AdminUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.Login = model.Login.Trim();
                model.Position = model.Position.Trim();
                var adminUserViewModelDto = Mapper.Map<DTOAdminUserViewModel>(model);
                string parametr;

                if (UserCompanyService.CreateAdminUser(adminUserViewModelDto, out parametr))
                {
                    model.ID = Guid.Parse(parametr);
                    return JsonConvert.SerializeObject(model);
                }
                return JsonConvert.SerializeObject(parametr);
            }
            return ErrorsUsers(ModelState);
        }

        [Authorize]
        [HttpPost]
        public bool DeleteAdminUsers(Guid ID)
        {
            return UserCompanyService.DeleteAdminUser(ID);
        }
/////////////////////////////////////////////////////////////////////////////////////////////////////////

        protected override void Dispose(bool disposing)
        {
            UserCompanyService.Dispose();
            base.Dispose(disposing);
        }
    }
}
