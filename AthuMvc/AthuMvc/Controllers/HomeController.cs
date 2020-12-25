using AthuMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AthuMvc.Controllers
{

    public class HomeController : Controller
    {
    [AllowAnonymous]
     
        public ActionResult Index()
        {
            return View();
        }       

    }
}