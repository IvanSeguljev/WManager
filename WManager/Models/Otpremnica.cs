using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WManager.Models
{
    public class Otpremnica
    {
        
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OtpremnicaId { get; set; }
        [Required]
        public string MenadzerId { get; set; }
        [ForeignKey("MenadzerId")]
        public ApplicationUser Menadzer { get; set; }
        [Required]
        public string Lokacija { get; set; }
        [Required]
        public DateTime Datum { get; set; }
        public List<StavkaOtpremnice> Stavke { get; set; }
    }
}