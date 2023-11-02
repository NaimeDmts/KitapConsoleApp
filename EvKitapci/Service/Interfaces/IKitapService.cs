using EvKitapci.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.Service.Interfaces
{
    public interface IKitapService
    {
        IList<Kitap> KitapAdinaGoreAra(string kitapAdi);
        IList<Kitap> KategoriyeGoreSorgula(int kategoriId);
        IList<Kitap> YazarGoreSorgula(string yazarAdi);
        IList<Kitap> OrderByDesc<TKey>(Expression<Func<Kitap, TKey>> expression);
        IList<Kitap> OrderByAsc<TKey>(Expression<Func<Kitap, TKey>> expression);
        IList<Kitap> StokBitenler();
        IList<Kitap> SonOnKitap();
        IList<Kitap> IndrimliKitaplar();

    }
}
