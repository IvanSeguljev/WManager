﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WManager.Models;

namespace WManager.Controllers
{
    public class ArtikalController : Controller
    {
        ApplicationDbContext context;
        public ArtikalController()
        {
            context = new ApplicationDbContext();
        }
        
        [HttpGet]
        public ActionResult Index()
        {
            List<Artikal> Artikli = context.Artikli.ToList();
            return View(Artikli);
        }
        [HttpGet]
        public ActionResult UnesiNoviArtikal()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnesiNoviArtikal(Artikal artikal)
        {
            if(ModelState.IsValid)
            {
                context.Artikli.Add(artikal);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult IzmeniArtikal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IzmeniArtikal(Artikal artikal)
        {
            if(ModelState.IsValid)
            {
                Artikal stari = context.Artikli.FirstOrDefault(x => x.Barkod == artikal.Barkod);
                stari.Kolicina = artikal.Kolicina;
                stari.Naziv = artikal.Naziv;
                stari.ZemljaPorekla = artikal.ZemljaPorekla;
                stari.Proizvodjac = artikal.Proizvodjac;

                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        //ststus kodovi: 0-nijeNadjen 1-nadjen 2-Neka druga greska
        [HttpPost] 
        public JsonResult TraziArtikal(string Barkod = "0")
        {
            if(Barkod == null)
            {
                return Json(new { status = 2 });
            }

            if (Barkod.Length == 13)
            {
                Artikal a = context.Artikli.FirstOrDefault(x => x.Barkod == Barkod);
                if (a != null)
                {
                    return Json(new { status = 1, artikal = a });
                }
                else
                {
                    return Json(new { status = 0 });
                }
            }
            else
            {
                return Json(new { status = 2 });
            }
        }
    }
}