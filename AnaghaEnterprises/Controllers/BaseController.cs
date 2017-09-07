using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnaghaEnterprises.Controllers
{
    public class BaseController : Controller
    {
        public ActionResult RedirectToLocal(string role)
        {
            switch (role)
            {
                case "Admin":
                    return RedirectToAction("Index","Admin");
                default:
                    return RedirectToAction("Index", "Home");
            }
        }
    }
}