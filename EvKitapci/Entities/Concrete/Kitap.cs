using EvKitapci.Entities.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Entities.Concrete
{
    public class Kitap : BaseEntity
    {
        private int kitapStok;
        private bool kitapStokDurum;

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string KitapAdi{ get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public double KitapFiyati{ get; set; }
        [Required]
        public int KitapStok
        {
            get { return kitapStok; }
            set
            {
                if (kitapStok >= 0)
                    kitapStok = value;
                else
                    throw new Exception("Stok 0 altında olamaz.");
            }
        }
        [Column(TypeName = "decimal(8,2)")]
        public double? KitapIndirimi { get; set; }
        public bool KitapStokDurum
        {
            get { return kitapStokDurum; }
            set
            {
                if (kitapStok > 0)
                    kitapStokDurum = true;
                else
                    kitapStokDurum = false;
            }
        }
        [Column(TypeName = "date")]
        public DateTime? KitapYayınTarihi { get; set; }

        public int YazarId { get; set; }
        public int KategoriId { get; set; }

        //Navigation Property
        public virtual Yazar Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}

