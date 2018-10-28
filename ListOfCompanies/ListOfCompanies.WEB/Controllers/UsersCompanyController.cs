﻿using AutoMapper;
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
        public bool DeleteAdminUsers(Guid ID)
        {
            return true;
            //return UserCompanyService.DeleteEndUser(ID);
        }

    }
}
