using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using ListOfCompanies.WEB.Models;
using ListOfCompanies.BLL.DTO;
using System.Security.Claims;
using ListOfCompanies.BLL.Interfaces;
using ListOfCompanies.BLL.Infrastructure;
using Microsoft.AspNet.Identity;
using AutoMapper;

namespace ListOfCompanies.WEB.Controllers
{
    public class ManageController : Controller
    {
        IUserService UserService;
        public ManageController(IUserService service)
        {
            UserService = service;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            Error
        }

        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : "";

            return View();
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            model.UserId = userId;

            var changePasswordViewModelDto = Mapper.Map<DTOChangePasswordViewModel>(model);
            OperationDetails operationDetails = await UserService.ChangePassword(changePasswordViewModelDto);

            if (operationDetails.Succedeed)
            {
                ClaimsIdentity claim = await UserService.GetClaim(userId);

                if (claim != null)
                {
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
            }

            ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            UserService.Dispose();
            base.Dispose(disposing);
        }
    }
}
