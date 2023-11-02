using EvKitapci.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.DTOs
{
    public class KitapDTO
    {
        public IList<Kitap> Kitaps { get; set; }
        public string KategoriAdi { get; set; }
    }
}
