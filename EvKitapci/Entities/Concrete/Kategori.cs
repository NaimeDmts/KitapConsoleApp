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
    [Table("TblKategori")]
    public class Kategori :BaseEntity
    {
        [Required]
        [Column(TypeName ="nvarchar(50)")]
        public string KategoriAd { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string? KategoriTanitim { get; set; }

        //Navigation Property
        public virtual IList<Kitap> Kitaps { get; set; }
    }
}
