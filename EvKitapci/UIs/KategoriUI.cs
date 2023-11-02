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
    public class KategoriUI
    {
        private readonly AppDbContext context;
        private KategoriService kategoriService;
        public KategoriUI()
        {
                context = new AppDbContext();
            kategoriService = new KategoriService(context);

        }
        public void KategoriEkle()
        {
            Console.Write("Kategori Adı: ");
            string kategoriAd = Console.ReadLine();
            Console.Write("Kategori içeriği");
            string kategoriTanitim = Console.ReadLine();

            if (kategoriAd.Length > 0 && !(kategoriService.Any(x => x.KategoriAd == kategoriAd)))
            {
                kategoriService.Add(new Kategori
                {
                    KategoriAd = kategoriAd,
                    KategoriTanitim = kategoriTanitim
                });
                Console.WriteLine("Başarıyla eklendi");

            }
            else
            {
                Console.WriteLine("Kategori adı mevcuttur");
            }
        }
        public void KategoriGuncelle()
        {
            Console.Write("Güncellemek istediğiniz Kategori ıd yazınız:");
            int id = Convert.ToInt32(Console.ReadLine());
            var kategori = kategoriService.GetById(id);
            if(kategori == null)
            {
                Console.WriteLine("İd hatalı");
                return;
            }
            Console.Write("Kategori yeni içeriği");
           kategori.KategoriTanitim = Console.ReadLine();
            kategoriService.Update(kategori);
        }
        public void KategoriSil()
        {
            Console.Write("Silmek istediğiniz Kategori ıd yazınız:");
            int id = Convert.ToInt32(Console.ReadLine());
            var kategori = kategoriService.GetById(id);
            if(kategori == null)
            {
                Console.WriteLine("İd hatalı");
                return;
            }
            kategoriService.Delete(kategori);
        }
        public void KategoriKitapListe()
        {
            var liste = kategoriService.KategoriVeIcindekiler();
            foreach(var item in liste)
            {
                Console.WriteLine("\t" + item.KategoriAdi);
                {
                    foreach(var item2 in item.Kitaps)
                    {
                        Console.WriteLine(item2.KitapAdi);
                    }
                }
            }
        }
        public void KategoriListele()
        {
            var kategoriListele = kategoriService.GetAll();
            foreach( var item in kategoriListele)
            {
                Console.WriteLine("Kategori Id: " + item.Id + "Kategori Ad: " + item.KategoriAd);
            }
        }
    }
}
