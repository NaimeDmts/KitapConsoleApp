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
    public class KitapUI
    {
        private readonly AppDbContext _context;
        private KitapService kitapService;
        private KategoriService kategoriService;
        private YazarService yazarService;
        public KitapUI()
        {
            _context = new AppDbContext();
            kitapService = new KitapService(_context);
            kategoriService = new KategoriService(_context);
            yazarService = new YazarService(_context);
        }

        public void KitapEkle()
        {
            var yazarListesi = yazarService.GetAll();
            var kategoriListesi = kategoriService.GetAll();
            if (yazarListesi.Count > 0 && kategoriListesi.Count > 0)
            {
                KategoriUI kategoriUI = new KategoriUI();
                Console.WriteLine("\nTüm kategori Listesi\n");
                kategoriUI.KategoriListele();
                YazarUI yazarUI = new YazarUI();
                Console.WriteLine("\nTüm Yazar Listesi\n");
                yazarUI.YazarListele();
                Console.WriteLine("Kitabın kategorisi kategori listesinde mevcut mu?");
                string kategorivar = Console.ReadLine();
                Console.WriteLine("Kitabın yazarı Yazar listesinde mevcut mu?");
                string yazarvar = Console.ReadLine();
                if (yazarvar.ToLower().Trim() != "evet" || yazarListesi.Count == 0)
                {
                    Console.WriteLine("Kitap kaydına devam edebilmek için  yazar kaydı yapmalısınız.");
                    yazarUI.YazarEkle();

                }
                if (kategorivar.ToLower().Trim() != "evet"|| kategoriListesi.Count == 0)
                {
                    Console.WriteLine("Kitap kaydına devam edebilmek için  kategori kaydı yapmalısınız.");
                    kategoriUI.KategoriEkle();

                }
                yazarUI.YazarListele();
                kategoriUI.KategoriListele();
                Console.Write("Kitap Adı Giriniz: ");
                string ad = Console.ReadLine();
                Console.Write("Kitabın Fiyatını Giriniz: ");
                double fiyat = Convert.ToDouble(Console.ReadLine());
                Console.Write("Kitabın İndirimi varsa giriniz: ");
                double indirim = Convert.ToDouble(Console.ReadLine());
                Console.Write("Kitap stok bilgisiniz giriniz: ");
                int stok = Convert.ToInt32(Console.ReadLine());
                Console.Write("Kitap yayin tarihi  (YYYY-MM-GG formatında) giriniz: ");
                DateTime yayinTarihi = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Yazar Id Giriniz: ");
                int yazarId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Kategori Id Giriniz: ");
                int kategoriId = Convert.ToInt32(Console.ReadLine());
                if (stok > 0)
                {
                    kitapService.Add(new Kitap
                    {
                        KitapAdi = ad,
                        KitapFiyati = fiyat,
                        KitapIndirimi = indirim,
                        KitapStok = stok,
                        KitapStokDurum = true,
                        KitapYayınTarihi = yayinTarihi,
                        YazarId = yazarId,
                        KategoriId = kategoriId

                    });

                    Console.WriteLine("Eklenme işlemi Başarıyla yapılmıştır");

                }
                else
                {
                    kitapService.Add(new Kitap
                    {
                        KitapAdi = ad,
                        KitapFiyati = fiyat,
                        KitapIndirimi = indirim,
                        KitapStok = stok,
                        KitapYayınTarihi = yayinTarihi,
                        YazarId = yazarId,
                        KategoriId = kategoriId

                    });

                    Console.WriteLine("Eklenme işlemi Başarıyla yapılmıştır");

                }

               



            }
          

        }
        public void KategoriyeGoreSorgula()
        {
            KategoriUI kategoriUI = new KategoriUI();
            kategoriUI.KategoriListele();
            Console.Write("Kitaplarını listelemek istediğiniz kategorini Id sini giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            var kitapListesi = kitapService.KategoriyeGoreSorgula(id);
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi} Kategori Adı: {item.Kategori.KategoriAd}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kitap Fiyatı: {item.KitapFiyati}");
            }

        }
        public void YazarAdınaGoreSorgula()
        {
            Console.Write("Kitaplarını listelemek istediğiniz Yazar ad bilgsini giriniz: ");
            string yazarAd = Console.ReadLine();
            var kitapListesi = kitapService.YazarGoreSorgula(yazarAd);
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }
        }
        public void KitapAra()
        {
            Console.Write("Aramak istediğiniz kitap adını giriniz: ");
            string kitapAd = Console.ReadLine();
            var kitapListesi = kitapService.KitapAdinaGoreAra(kitapAd);
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }
        }
        public void FiyataGoreArtanSirala()
        {
            var kitapListesi = kitapService.OrderByAsc(x => x.KitapFiyati);
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi}  Kitap Fiyatı: {item.KitapFiyati}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd} ");
            }
        }
        public void TariheGoreSirala()
        {
            var kitapListesi = kitapService.OrderByDesc(x => x.KitapYayınTarihi);
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi} Kitap YayınTarihi: {item.KitapYayınTarihi} Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }
        }
        public void IndirimliOlanlar()
        {
            var kitapListesi = kitapService.IndrimliKitaplar();
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi} İndirim Oranı: {item.KitapIndirimi}   Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }
        }
        public void EklenenSonOnKitap()
        {
            var kitapListesi = kitapService.SonOnKitap();
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }
        }
        public void KitapGuncelle()
        {
            KitapListesi();
            Console.Write("\nGüncellemek istediğiniz Kitap Id'sini giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if(kitapService.Any(x=>x.Id == id))
            {
                var kitap = kitapService.GetById(id);

                Console.WriteLine("Hangi bilgiyi güncellemek istersiniz?");
                Console.WriteLine("1. Kitap Adı");
                Console.WriteLine("2. Kitap Fiyatı");
                Console.WriteLine("3. Kitap İndirimi");
                Console.WriteLine("4. Kitap Stok Miktarı");
                Console.WriteLine("5. Kitap Yayın Tarihi");
                Console.WriteLine("6. Yazar ID");
                Console.WriteLine("7. Kategori ID");
                Console.Write("Lütfen bir seçenek girin (1-7): ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.Write("Yeni Kitap Adını Giriniz: ");
                        string yeniKitapAdi = Console.ReadLine();
                        kitap.KitapAdi = yeniKitapAdi;
                        kitapService.Update(kitap);
                        break;
                    case "2":
                        Console.Write("Yeni Kitap Fiyatını Giriniz: ");
                        double yeniFiyat = Convert.ToDouble(Console.ReadLine());
                        kitap.KitapFiyati = yeniFiyat;
                        Console.WriteLine("Kitap fiyatı başarıyla güncellendi.");
                        kitapService.Update(kitap);
                        break;

                    case "3":
                        Console.Write("Yeni Kitap İndirimini Giriniz: ");
                        double yeniIndirim = Convert.ToDouble(Console.ReadLine());
                        kitap.KitapIndirimi = yeniIndirim;
                        kitapService.Update(kitap);
                        Console.WriteLine("Kitap indirimi başarıyla güncellendi.");
                        break;

                    case "4":
                        Console.Write("Yeni Kitap Stok Miktarını Giriniz: ");
                        int yeniStok = Convert.ToInt32(Console.ReadLine());
                        kitap.KitapStok = yeniStok;
                        kitapService.Update(kitap);
                        Console.WriteLine("Kitap stok miktarı başarıyla güncellendi.");
                        break;

                    case "5":
                        Console.Write("Yeni Kitap Yayın Tarihini Giriniz: ");
                        DateTime yeniYayinTarihi = Convert.ToDateTime(Console.ReadLine());
                        kitap.KitapYayınTarihi = yeniYayinTarihi;
                        kitapService.Update(kitap);
                        Console.WriteLine("Kitap yayın tarihi başarıyla güncellendi.");
                        break;

                    case "6":
                        YazarUI yazarUI = new YazarUI();
                        yazarUI.YazarListele();
                        Console.Write("Yeni Yazar ID'sini Giriniz: ");
                        int yeniYazarId = Convert.ToInt32(Console.ReadLine());
                        kitap.YazarId = yeniYazarId;
                        kitapService.Update(kitap);
                        Console.WriteLine("Yazar ID başarıyla güncellendi.");
                        break;

                    case "7":
                        KategoriUI kategoriUI = new KategoriUI();
                        kategoriUI.KategoriListele();
                        Console.Write("Yeni Kategori ID'sini Giriniz: ");
                        int yeniKategoriId = Convert.ToInt32(Console.ReadLine());
                        kitap.KategoriId = yeniKategoriId;
                        kitapService.Update(kitap);
                        Console.WriteLine("Kategori ID başarıyla güncellendi.");
                        break;

                    default:
                        Console.WriteLine("Geçersiz seçenek.");
                        break;
                }

            }
            else
            {
                Console.WriteLine("Girdiğiniz Id'ye ait Kitap bulunmamaktadır.");
            }

        }
        public void KitapListesi()
        {
            var kitaplar = kitapService.GetAll();
            foreach (var item in kitaplar)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi} ");
            }
        }
        public void KitapSil()
        {
            KitapListesi();
          
            Console.Write("\nSilmek istediğiniz Kitap Id'sini giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (kitapService.Any(x => x.Id == id))
            {
                var kitap = kitapService.GetById(id);
                kitapService.Delete(kitap);
            }
            else
            {
                Console.WriteLine("Girdiğiniz Id'ye ait Kitap bulunmamaktadır.");
            }

        }

        public void StokBildir()
        {
            var kitapListesi = kitapService.StokBitenler();
            foreach (var item in kitapListesi)
            {
                Console.WriteLine($"Kitap Id: {item.Id} Kitap Adı: {item.KitapAdi}  Yazar Ad-Soyad: {item.Yazar.YazarAdSoyad} Kategori Adı: {item.Kategori.KategoriAd}  Kitap Fiyatı: {item.KitapFiyati}");
            }

        }
        public void KategoriListele()
        {
            var kategoriListesi = kategoriService.GetAll();
            foreach (var item in kategoriListesi)
            {
                Console.WriteLine($"Kategori Id: {item.Id} Kategori Adı: {item.KategoriAd}");
            }
        }
        public void YazarListele()
        {
            var yazarListesi = yazarService.GetAll();
            foreach (var item in yazarListesi)
            {
                Console.WriteLine($"Yazar Id: {item.Id} Yazar Ad-Soyad: {item.YazarAdSoyad}");
            }
        }
    }
}
