using EvKitapci.Contexts;
using EvKitapci.Entities.Concrete;
using EvKitapci.Service.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EvKitapci.UIs
{
    public class KullaniciUI
    {
        private readonly AppDbContext _context;
        private KullaniciService kullaniciService;
        public KullaniciUI()
        {
            _context = new AppDbContext();
            kullaniciService = new KullaniciService(_context);
        }
        public void KullaniciEkle()
        {
            try
            {
                Console.Write("Kullanicnin Adını Giriniz: ");
                string ad = Console.ReadLine();
                Console.Write("Kullanicinin soyadını giriniz: ");
                string soyad = Console.ReadLine();
                Console.Write("Kullanici Email adresinizi giriniz: ");
                string email = Console.ReadLine();
                Console.Write("Şifre giriniz: ");
                string sifre = Console.ReadLine();
                string hashedPassword = HashSHA256(sifre);
                if (IsValidEmail(email) && (!kullaniciService.Any(x => x.Email == email)))
                {
                    if (SifreKontrol(sifre))
                    {
                        kullaniciService.Add(new Kullanici
                        {
                            Ad = ad,
                            Soyad = soyad,
                            Email = email,
                            Sifre = hashedPassword
                        });
                        Console.WriteLine("Kayıt işlemi başarıyla tamamlanmıştır.");
                    }
                }
                else
                {
                    Console.WriteLine("Yanlış formatta mail adresi veya bu mail adresi sistemde kayıtlıdır.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void KulaniciGiris()
        {
            Console.Write("Giris Yapmak için Mail Adresinizi giriniz: ");
            string email = Console.ReadLine();
            Console.Write("Şifrenizi Giriniz: ");
            string sifre = Console.ReadLine();
            Kullanici kullanici1 = kullaniciService.GetDefault(x=>x.Email==email.Trim());
            string hashedPassword = HashSHA256(sifre);
            if (!(kullaniciService.Any(x => x.Email == email)))
            {
                Console.WriteLine("Geçersiz mail adrsi, tekrar deneyin");
                return;
            }
            bool isPasswordCorrect = VerifySHA256(sifre, kullanici1.Sifre.ToString());
            if (!(isPasswordCorrect))
            {
                Console.WriteLine("Şifre yanlış.");
                return;
            }
            var kullanici = kullaniciService.GetDefault(x => x.Email.Equals(email) && x.Sifre.Equals(hashedPassword));
            KitapUI kitapUI = new KitapUI();
            KategoriUI kategoriUI = new KategoriUI();
            YazarUI yazarUI = new YazarUI();
         

            while (kullanici!=null)
            {
                Console.WriteLine("Yapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Kitap Ekle");
                Console.WriteLine("2. Kitap Sil");
                Console.WriteLine("3. Kitap Güncelle");
                Console.WriteLine("4. Kitap Adına Göre Ara");
                Console.WriteLine("5. Kategoriye Göre Sorgula");
                Console.WriteLine("6. Yazar Göre Sorgula");
                Console.WriteLine("7. Fiyata Göre Sırala (artan)");
                Console.WriteLine("8. Tarihe Göre Sırala (azalan)");
                Console.WriteLine("9. Stok Biten Kitaplar");
                Console.WriteLine("10. Son On Kitap");
                Console.WriteLine("11. İndirimli Kitaplar");
                Console.WriteLine("12. Çıkış");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        kitapUI.KitapEkle();
                        break;

                    case "2":
                        kitapUI.KitapSil();
                        break;

                    case "3":
                        kitapUI.KitapGuncelle();
                        break;

                    case "4":
                        kitapUI.KitapAra();
                        break;

                    case "5":
                        kitapUI.KategoriyeGoreSorgula();
                        break;
                    case "6":
                        kitapUI.YazarAdınaGoreSorgula();
                        break;

                    case "7":
                        kitapUI.FiyataGoreArtanSirala();
                        break;

                    case "8":
                        kitapUI.TariheGoreSirala();
                        break;

                    case "9":
                        kitapUI.StokBildir();
                        break;

                    case "10":
                        kitapUI.EklenenSonOnKitap();
                        break;
                    case "11":
                        kitapUI.IndirimliOlanlar();
                        break;
                    case "12":
                        kullanici = null;
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçenek.");
                        break;
                }
            }

        }

        public void KullaniciGuncelle()
        {
            Console.Write("Güncellemek istediğiniz kullanıcının Id bilgisini giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (!(kullaniciService.Any(x => x.Id == id)))
            {
                Console.WriteLine("Id'yi hatalı girdiniz");
                return;
            }
            Kullanici kullanici = kullaniciService.GetById(id);
            Console.Write("Mevcut Şifre: ");
            string mevcutSifre = Console.ReadLine();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(mevcutSifre);
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(mevcutSifre, hashedPassword);
            if (!isPasswordCorrect)
            {
                Console.WriteLine("Mevcut şifrenizi tekrar giriniz ");
                return;
            }
            Console.Write("Yeni Şifre: ");
            string yeniSifre = Console.ReadLine();
            if(mevcutSifre == yeniSifre)
            {
                Console.WriteLine("Eski sifrenizden farklı olmalıdır");
                return;
            }
            if (!SifreKontrol(yeniSifre))
            {
                Console.WriteLine("Zayıf şifre tekrar deneyiniz");
                return;
            }
            string newhashedPassword = BCrypt.Net.BCrypt.HashPassword(yeniSifre);
            kullanici.Sifre=newhashedPassword;
            kullaniciService.Update(kullanici);


        }
        public void KullaniciSil()
        {
            Console.Write("Silmek istediğiniz kullanıcının Id bilgisini giriniz: ");
            int id = Convert.ToInt32(Console.ReadLine());
            if (!(kullaniciService.Any(x => x.Id == id)))
            {
                Console.WriteLine("Id'yi hatalı girdiniz");
                return;
            }
            Kullanici kullanici = kullaniciService.GetById(id);
            kullaniciService.Delete(kullanici);
        }
        static bool IsValidEmail(string emailAddress)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(emailAddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool SifreKontrol(string sifre)
        {
            if (sifre.Length >= 6)
            {
                if (sifre.Any(char.IsDigit))
                {
                    if ((!sifre.All(char.IsLetterOrDigit)) && sifre.Any(char.IsLetter))
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("Şifrede en az bir özel karakter ve harf olmak zorunda");
                    }
                }
                else
                {
                    throw new Exception("Şifrede En az bir sayı olmak zorunda");
                }
            }
            else
            {
                throw new Exception("Şifre en az 6 karakterli olmak zorunda");
            }
        }
        static string HashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder builder = new StringBuilder();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        static bool VerifySHA256(string input, string hash)
        {
            string hashedInput = HashSHA256(input);
            return hashedInput == hash;
        }
    }
}

