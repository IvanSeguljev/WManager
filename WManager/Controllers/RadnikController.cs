using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WManager.Models;

namespace WManager.Controllers
{
    public class RadnikController : Controller
    {
        ApplicationDbContext context;
        public RadnikController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Radnik
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Za ajax, vrsi proveru da li radnik postoji i vraca status kod i radnika
        /// status kodovi: 0-nijeNadjen 1-nadjen 2-Neka druga greska
        /// </summary>
        /// <param name="Email">Email radnika koga trazimo</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TraziRadnika(string Email = "")
        {
            if (Email == "")
            {
                return Json(new { status = 2 });
            }

            ApplicationUser radnik = context.Users.FirstOrDefault(x => x.Email == Email);
            if (radnik != null)
            {
                return Json(new { status = 1, radnik = radnik });
            }
            else
            {
                return Json(new { status = 0 });
            }


        }
    }
}