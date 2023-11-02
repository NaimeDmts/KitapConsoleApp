using EvKitapci.Contexts;
using EvKitapci.Entities.Concrete;
using EvKitapci.Entities.Enums;
using EvKitapci.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Concrete
{
    public class KitapService : BaseService<Kitap>, IKitapService
    {
        private readonly AppDbContext _context;
        public KitapService(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public IList<Kitap> IndrimliKitaplar()
        {
          return _context.Kitaps.Where(x=>x.Status!=Status.Passive && x.KitapIndirimi>0).OrderBy(x=>x.KitapIndirimi).ToList();
        }

        public IList<Kitap> KategoriyeGoreSorgula(int kategoriId)
        {
            return _context.Kitaps.Where(x=>x.Status!=Status.Passive && x.KategoriId==kategoriId).OrderBy(x=>x.KitapAdi).ToList();
        }

        public IList<Kitap> KitapAdinaGoreAra(string kitapAdi)
        {
            return _context.Kitaps.Where(x => x.KitapAdi.Contains(kitapAdi)&& x.Status != Status.Passive).OrderByDescending(x=>x.Id).ToList();
        }

        public IList<Kitap> OrderByAsc<TKey>(Expression<Func<Kitap,TKey>> expression)
        {
            return _context.Kitaps.Where(x => x.Status != Status.Passive).OrderBy(expression).ToList();
        }

        public IList<Kitap> OrderByDesc<TKey>(Expression<Func<Kitap, TKey>> expression)
        {
            return _context.Kitaps.Where(x=>x.Status!=Status.Passive).OrderByDescending(expression).ToList();
        }

        public IList<Kitap> SonOnKitap()
        {
            return _context.Kitaps.Where(x => x.Status != Status.Passive).OrderByDescending(x => x.Id).Take(10).ToList();
        }

        public IList<Kitap> StokBitenler()
        {
            return _context.Kitaps.Where(x=>x.Status!=Status.Passive && x.KitapStok<10).ToList();
        }

        public IList<Kitap> YazarGoreSorgula(string yazarAdi)
        {
            return _context.Kitaps.Where(x=>x.Status !=Status.Passive &&x.Yazar.YazarAd.Contains(yazarAdi)).OrderBy(x=>x.KitapYayınTarihi).ToList();
        }
    }
}
