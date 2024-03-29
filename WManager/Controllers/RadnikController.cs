﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WManager.Models;

namespace WManager.Controllers
{
    [Authorize(Roles = "Menadzer")]
    public class RadnikController : Controller
    {
        ApplicationDbContext context;
        public RadnikController()
        {
            context = new ApplicationDbContext();
        }
        /// <summary>
        /// Prikazuje listu radnika
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            
            return View(context.Users.ToList());
        }
        /// <summary>
        /// Prikazuje stranicu za izmenu radnika
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IzmenaRadnika()
        {
            return View();
        }
        /// <summary>
        /// Vrsi izmenu podataka o radniku u bazi.
        /// </summary>
        /// <param name="radnik">izmenjeni radnik</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IzmenaRadnika(ApplicationUser radnik)
        {
           
                ApplicationUser stari = context.Users.FirstOrDefault(x => x.Email == radnik.Email);
                if (stari != null)
                {
                    stari.FirstName = radnik.FirstName;
                    stari.LastName = radnik.LastName;
                    stari.PhoneNumber = radnik.PhoneNumber;
                    stari.UserName = radnik.UserName;
                    stari.Email = radnik.UserName; //Username i email su prakticno isti. Na stranici za izmenu se username koristi za izmenu, a email za proveru postojanja
                    context.SaveChanges();
                    TempData["AlertSuccess"] = "uspesno izmenjen radnik!";
                    return RedirectToAction("Index");
                }
            
            TempData["AlertError"] = "Doslo je do greske prilikom izmene radnika";
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Vraca stranicu za brisanje radnika
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BrisanjeRadnika()
        {
            return View();
        }
        /// <summary>
        /// Brise radnika iz baze
        /// </summary>
        /// <param name="Email">Imejl adresa radnika za brisanje</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BrisanjeRadnika(string Email)
        {
            ApplicationUser zaBrisanje = context.Users.FirstOrDefault(x => x.Email == Email);
            if(zaBrisanje != null)
            {
                context.Users.Remove(zaBrisanje);
                context.SaveChanges();
                TempData["AlertSuccess"] = "uspesno obrisan radnik!";
                return RedirectToAction("Index");
            }

            TempData["AlertError"] = "Doslo je do greske prilikom brisanja radnika";
            return RedirectToAction("Index");
        }
        /// <summary>
        /// vraca stranicu za trazenje radnika
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TraziRadnika()
        {
            return View();
        }
        /// <summary>
        /// Prikazuje rezultate pretrage
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <param name="PhoneNumber"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PretragaRadnika(string Email = "", string FirstName = "", string LastName = "", string PhoneNumber = "")
        {
            IQueryable<ApplicationUser> kveri = context.Users;
            if (Email != "")
            {
                kveri = kveri.Where(x => x.Email == Email);
            }
            if(FirstName != "")
            {
                kveri = kveri.Where(x => x.FirstName == FirstName);
            }
            if(LastName != "")
            {
                kveri = kveri.Where(x => x.LastName == LastName);
            }
            if(PhoneNumber != "")
            {
                kveri = kveri.Where(x => x.PhoneNumber == PhoneNumber);
            }
            List<ApplicationUser> radnici = kveri.ToList();
            if(radnici.Count == 1)
            {
                return View("DetaljiRadnika", radnici[0]);
            }
            return View(radnici);
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