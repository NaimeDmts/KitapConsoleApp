using EvKitapci.Contexts;
using EvKitapci.DTOs;
using EvKitapci.Entities.Concrete;
using EvKitapci.Entities.Enums;
using EvKitapci.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Concrete
{
    public class KategoriService : BaseService<Kategori>, IKategoriService
    {
        private readonly AppDbContext _context;
        public KategoriService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IList<KitapDTO> KategoriVeIcindekiler()
        {
            return _context.Kategoris.Where(x => x.Status != Status.Passive).Select(x => new KitapDTO { Kitaps = x.Kitaps, KategoriAdi = x.KategoriAd }).ToList();
        }
    }
}
