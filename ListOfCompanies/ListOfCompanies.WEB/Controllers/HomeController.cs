using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListOfCompanies.WEB.Models;
using ListOfCompanies.BLL.DTO;
using ListOfCompanies.BLL.Interfaces;

namespace ListOfCompanies.WEB.Controllers
{
    public class HomeController : Controller
    {
        ICompanyService CompanyService;
        public HomeController(ICompanyService service)
        {
            CompanyService = service;
        }

        [Authorize]
        public ActionResult Index(PagingFilteringViewModel model)
        {
            // Companies on page.
           // int pageSize = 2;
            int pageSize = 10;

            int page = model.Page;
            int totalPages;
            if (model.SearchByName != null && model.SearchByName.Trim()=="")
                model.SearchByName = null;

            var pagingFilteringViewModel = Mapper.Map<DTOPagingFilteringViewModel>(model);

            var companyList = Mapper.Map<IEnumerable<DTOCompanyViewModel>, IEnumerable<CompanyViewModel>>
                (CompanyService.GetCompanies(pagingFilteringViewModel, ref page, pageSize, out totalPages));

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;

            return View(companyList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Тестовое задание - список компаний";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Связаться с Админом";

            return View();
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditCompany(PagingFilteringViewModel model, Guid IdCompany)
        {
            if (IdCompany.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                var companyViewModel = Mapper.Map<CompanyViewModel>(CompanyService.GetCompany(IdCompany));

                if (companyViewModel != null)
                {
                    ViewBag.Title = "Редактирование компании";
                    ViewBag.Page = model.Page;
                    ViewBag.SortByname = model.SortByname;
                    ViewBag.SortBycountry = model.SortBycountry;
                    ViewBag.IncludesAdmins = model.IncludesAdmins;
                    if (model.SearchByName != null && model.SearchByName.Trim() == "")
                        model.SearchByName = null;
                    ViewBag.SearchByName = model.SearchByName;
                    return View("CompanyView", companyViewModel);
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CreateCompany(PagingFilteringViewModel model)
        {
            ViewBag.Title = "Создание компании";
            ViewBag.Page = model.Page;
            ViewBag.SortByname = model.SortByname;
            ViewBag.SortBycountry = model.SortBycountry;
            ViewBag.IncludesAdmins = model.IncludesAdmins;
            if (model.SearchByName != null && model.SearchByName.Trim() == "")
                model.SearchByName = null;
            ViewBag.SearchByName = model.SearchByName;
            CompanyViewModel companyViewModel = new CompanyViewModel();
            return View("CompanyView", companyViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CreateCompanyConfirm(PagingFilteringViewModel pfmodel, CompanyViewModel model)
        {
            if (model.ID.ToString() == "00000000-0000-0000-0000-000000000000")
                ViewBag.Title = "Создание компании";
            else
                ViewBag.Title = "Редактирование компании";

            ViewBag.Page = pfmodel.Page;
            ViewBag.SortByname = pfmodel.SortByname;
            ViewBag.SortBycountry = pfmodel.SortBycountry;
            ViewBag.IncludesAdmins = pfmodel.IncludesAdmins;
            ViewBag.SearchByName = pfmodel.SearchByName;

            if (ModelState.IsValid)
            {
                model.Country = model.Country.Trim();
                model.Name = model.Name.Trim();
                string parametr;
                var companyViewModelDto = Mapper.Map<DTOCompanyViewModel>(model);
                if (CompanyService.Edit_CreateCompanyConfirm(companyViewModelDto, out parametr))
                {
                    model.ID = Guid.Parse(parametr);
                    return View("CompanyView", model);
                }
                ModelState.AddModelError("", parametr);
            }
            return View("CompanyView", model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteCompany(DeleteCompanyViewModel model)
        {
            if (ModelState.IsValid)
                ViewBag.answer = "Вы действительно хотите удалить компанию с названием \"" + model.Name + "\"";
            else
                ViewBag.answer = "Произошла ошибка. Закройте это окно и обновите страницу";
            return PartialView("_DeleteCompanyPartial", model);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public bool DeleteCompanyConfirm(Guid IdCompany)
        {
            return CompanyService.DeleteCompany(IdCompany);
        }

        protected override void Dispose(bool disposing)
        {
            CompanyService.Dispose();
            base.Dispose(disposing);
        }
    }
}