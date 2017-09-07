using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AnaghaEnterprises.Models;
using System.Web.Security;
using AnaghaEnterprises.Helper;


namespace AnaghaEnterprises.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login()
        {
       
            return View();
        }

       [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                var adminmodelobj = new AdminModel();
                if (model.IsAuthorize())
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToLocal(model.Role);
                }
            }
            ModelState.AddModelError("", "Please Enter valid username or password");
            return View(model);
        }   
    }
}