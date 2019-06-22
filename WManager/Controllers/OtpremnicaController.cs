using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WManager.Models;

namespace WManager.Controllers
{
    public class OtpremnicaController : Controller
    {
        ApplicationDbContext context;

        public OtpremnicaController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Otpremnica
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult KreiranjeOtpremnice()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KreiranjeOtpremnice(int id, string mesto, List<StavkaOtpremnice> stavke)
        {
            Otpremnica otpremnica = new Otpremnica();
            ApplicationUser menadzer = context.Users.First(x => x.UserName == User.Identity.Name);
            otpremnica.OtpremnicaId = id;
            otpremnica.Menadzer = menadzer;
            otpremnica.Lokacija = mesto;
            otpremnica.Datum = DateTime.Now;
            context.Otpremnice.Add(otpremnica);
            
            foreach(StavkaOtpremnice s in stavke)
            {
                s.OtpremnicaId = id;
              
                context.StavkeOtpremnice.Add(s);
                context.Artikli.First(x => x.Barkod == s.Barkod).Kolicina -= s.Kolicina;
            }
            
            context.SaveChanges();
            return Json("");

        }
        /// <summary>
        /// Za ajax, vrsi proveru da li artikal postoji i vraca status kod i artikal
        /// status kodovi: 0-nijeNadjen 1-nadjen 2-Neka druga greska
        /// </summary>
        /// <param name="Barkod">Barkod artikla koji se trazi</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TraziOtpremnicu(int OtpremnicaId = 0)
        {
            if (OtpremnicaId == 0)
            {
                return Json(new { status = 2 });
            }

            
                Otpremnica o = context.Otpremnice.FirstOrDefault(x => x.OtpremnicaId == OtpremnicaId);
                if (o != null)
                {
                    return Json(new { status = 1, otpremnica = o });
                }
                else
                {
                    return Json(new { status = 0 });
                }
            
           
        }
        [HttpPost]
        public JsonResult VratiSveArtikle(int OtpremnicaId = 0)
        {
            return Json(new { artikli=context.Artikli.Select( x => new { barkod= x.Barkod,naziv = x.Naziv } ).ToList() });

        }
        [HttpPost]
        public JsonResult VratiStanje(string barkod)
        {
            return Json(new { stanje = context.Artikli.Where(x => x.Barkod == barkod).Select(x => x.Kolicina).First() });
        }
    }
}