using EvKitapci.Entities.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Entities.Concrete
{
    [Table("TblYazar")]
    public class Yazar :BaseEntity
    {
        private string _yazarAd;
        private string _yazarSoyad;

        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string YazarAd { get => _yazarAd; set => _yazarAd = value; }
        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string YazarSoyad { get => _yazarSoyad; set => _yazarSoyad = value; }
        [NotMapped]
        public string YazarAdSoyad
        {
            get => _yazarAd+" "+_yazarSoyad;
        }
        //Navigation Property
        public virtual IList<Kitap> Kitaps { get; set; }


    }
}
