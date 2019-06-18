using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WManager.Models
{
    public class Artikal
    {
        [Key]
        [Required]
        [StringLength(13)]
        public string Barkod { get; set; }
        [Required]
        [MaxLength(250)]
        public string Naziv { get; set; }
        [Required]
        
        public int Kolicina { get; set; }
        [Required]
        [MaxLength(250)]
        public string ZemljaPorekla { get; set; }
        [Required]
        [MaxLength(250)]
        public string Proizvodjac { get; set; }
    }
}