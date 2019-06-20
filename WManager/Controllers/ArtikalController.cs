using System;
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

        /// <summary>
        /// Vraca listu artikala
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<Artikal> Artikli = context.Artikli.ToList();
            return View(Artikli);
        }

        /// <summary>
        /// Vraca stranicu za unos artikla
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult UnesiNoviArtikal()
        {
            return View();
        }

        /// <summary>
        /// Vrsi unos novog artikla u bazu
        /// </summary>
        /// <param name="artikal">novi artikal</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UnesiNoviArtikal(Artikal artikal)
        {
            if(ModelState.IsValid)
            {
                context.Artikli.Add(artikal);
                context.SaveChanges();
                TempData["AlertSuccess"] = "uspesno kreiran novi artikal!";
                return RedirectToAction("Index");

            }
            TempData["AlertError"] = "Doslo je do greske prilikom unosa artikla";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Vraca stranicu za izmenu artikla
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult IzmeniArtikal()
        {
            return View();
        }

        /// <summary>
        /// Vrsi izmenu artikla
        /// </summary>
        /// <param name="artikal">Artikal popunjen novim podacima</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult IzmeniArtikal(Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                Artikal stari = context.Artikli.FirstOrDefault(x => x.Barkod == artikal.Barkod);
                if (stari != null)
                { 
                    stari.Kolicina = artikal.Kolicina;
                    stari.Naziv = artikal.Naziv;
                    stari.ZemljaPorekla = artikal.ZemljaPorekla;
                    stari.Proizvodjac = artikal.Proizvodjac;

                    context.SaveChanges();
                    TempData["AlertSuccess"] = "Uspesno izmenjen artikal!";
                    return RedirectToAction("Index");
                }
            }
            TempData["AlertError"] = "Doslo je do greske prilikom izmene artikla!";
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Vraca stranicu za brisanje artikla
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult BrisanjeArtikla()
        {
            return View();
        }

        /// <summary>
        /// Vrsi brisanje artikla sa prosledjenim bar kodom
        /// </summary>
        /// <param name="Barkod">Barkod artikla za brisanje</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BrisanjeArtikla(string Barkod)
        {
            Artikal zaBrisanje = context.Artikli.FirstOrDefault(x => x.Barkod == Barkod);
            if(zaBrisanje != null)
            {
                context.Artikli.Remove(zaBrisanje);
                context.SaveChanges();
                TempData["AlertSuccess"] = "uspesno obrisan artikal!";
                return RedirectToAction("index");
            }
            TempData["AlertError"] = "Doslo je do greske prilikom brisanja artikla!";
            return RedirectToAction("index");
        }

        /// <summary>
        /// Vraca stranicu za skidanje artikla sa stanja
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult SkidanjeSaStanja()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SkidanjeSaStanja(string Barkod, int Kolicina)
        {
            Artikal artikal = context.Artikli.FirstOrDefault(x => x.Barkod == Barkod);
            if(artikal != null)
            {
                artikal.Kolicina -= Kolicina;
                context.SaveChanges();
                TempData["AlertSuccess"] = "uspesno skinuto sa stanja!";
                return RedirectToAction("index");
            }
            TempData["AlertError"] = "Doslo je do greske prilikom skidanja artikla sa stanja!";
            return RedirectToAction("index");
        }
        /// <summary>
        /// Vraca stranicu za unos parametara pretrage
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PretragaArtikala()
        {
            return View();
        }

        /// <summary>
        /// Vrsi pretragu artikala po prosledjenim parametrima
        /// </summary>
        /// <param name="Barkod"></param>
        /// <param name="Naziv"></param>
        /// <param name="ZemljaPorekla"></param>
        /// <param name="Proizvodjac"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult TraziArtikal(string Barkod = "", string Naziv = "", string ZemljaPorekla = "", string Proizvodjac = "")
        {
            IQueryable<Artikal> rezultat = context.Artikli;
            if(Barkod != "")
            {
                rezultat = rezultat.Where(x => x.Barkod == Barkod);
            }
            if(Naziv != "")
            {
                rezultat = rezultat.Where(x => x.Naziv == Naziv);
            }
            if(ZemljaPorekla != "")
            {
                rezultat = rezultat.Where(x => x.ZemljaPorekla == ZemljaPorekla);
            }
            if(Proizvodjac != "")
            {
                rezultat = rezultat.Where(x => x.Proizvodjac == Proizvodjac);
            }
            return View(rezultat.ToList());
        }
        /// <summary>
        /// Za ajax, vrsi proveru da li artikal postoji i vraca status kod i artikal
        /// status kodovi: 0-nijeNadjen 1-nadjen 2-Neka druga greska
        /// </summary>
        /// <param name="Barkod">Barkod artikla koji se trazi</param>
        /// <returns></returns>
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