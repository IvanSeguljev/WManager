using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WManager.Models
{
    public class StavkaOtpremnice
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Otpremnica")]
        public int OtpremnicaId { get; set; }
        
        public Otpremnica Otpremnica { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Artikal")]
        public string Barkod { get; set; }
        [Required]
        public int Kolicina { get; set; }

        public Artikal Artikal { get; set; }
    }
}