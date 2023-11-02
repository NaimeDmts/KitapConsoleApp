using EvKitapci.Contexts;
using EvKitapci.Entities.Concrete;
using EvKitapci.Service.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.UIs
{
    public class YazarUI
    {
        private readonly AppDbContext _context;
        private YazarService yazarService;
        public YazarUI()
        {
                _context = new AppDbContext();
            yazarService = new YazarService(_context);
        }

        public void YazarEkle()
        {
            Console.Write("Yazar Ad: ");
            string yazarAdi = Console.ReadLine();
            Console.Write("Yazar Soyad: ");
            string soyad= Console.ReadLine();

            yazarService.Add(new Yazar
            {
                YazarAd = yazarAdi,
                YazarSoyad = soyad

            });
            Console.WriteLine("Ekleme Başarılı");
        }
        public void YazarGuncelle()
        {
            Console.WriteLine("Guncellenecek yazar Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Yazar yazar = yazarService.GetById(id);
            if(yazar == null)
            {
                Console.WriteLine("Id ye ait yazar yoktur");
                return;
            }
            Console.WriteLine("Yazar ad:");
            yazar.YazarAd= Console.ReadLine();
            Console.WriteLine("Yazar Soyad ");
            yazar.YazarSoyad = Console.ReadLine();
            yazarService.Update(yazar);
        }
        public void YazarSil()
        {
            Console.WriteLine("Guncellenecek yazar Id");
            int id = Convert.ToInt32(Console.ReadLine());
            Yazar yazar = yazarService.GetById(id);
            if (yazar == null)
            {
                Console.WriteLine("Id ye ait yazar yoktur");
                return;
            }
            yazarService.Delete(yazar);
        }
        public void YazarListele()
        {
            var yazarList = yazarService.GetAll();
            foreach( var yazar in yazarList )
            {
                Console.WriteLine("Yazar Id:" + yazar.Id + "Yazar AdSoyad " + yazar.YazarAdSoyad);
            }
        }
        public void YazarKitapListele()
        {
            var yazarList = yazarService.GetAll();
            foreach (var yazar in yazarList)
            {
                Console.WriteLine("\t" + "Yazar Id:" + yazar.Id + "Yazar AdSoyad " + yazar.YazarAdSoyad);
                foreach(var kitap in yazar.Kitaps)
                {
                    Console.WriteLine("Kitap Adı" + kitap.KitapAdi);
                }
            }
        }
    }
}
