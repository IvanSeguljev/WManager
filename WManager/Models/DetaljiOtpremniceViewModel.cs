using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WManager.Models
{
    public class DetaljiOtpremniceViewModel
    {
        public DetaljiOtpremniceViewModel()
        {
            Stavke = new List<StavkaOtpremnice>();
        }
        public Otpremnica Otpremnica { get; set; }
        public ApplicationUser Menadzer { get; set; }
        public List<StavkaOtpremnice> Stavke { get; set; }
       
    }
}