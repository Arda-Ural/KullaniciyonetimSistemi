using System;
using System.Collections.Generic;
using System.Linq;

namespace KullaniciYonetimi
{
    class Program
    {
        // Kullanıcı Modeli
        public class User
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Role { get; set; } // Admin veya User
        }

        static List<User> Veritabani = new List<User>();
        static User AktifKullanici = null;

        static void Main(string[] args)
        {
            // Varsayılan Yönetici Hesabı
            Veritabani.Add(new User { Id = 1, Username = "admin", Password = "123", Role = "Admin" });

            while (true)
            {
                if (AktifKullanici == null)
                {
                    MenuGirisYapilmadi();
                }
                else if (AktifKullanici.Role == "Admin")
                {
                    MenuAdmin();
                }
                else
                {
                    MenuStandartKullanici();
                }
            }
        }

        // --- MENÜLER ---

        static void MenuGirisYapilmadi()
        {
            Console.WriteLine("\n=== ANA MENU ===");
            Console.WriteLine("1- Giriş Yap");
            Console.WriteLine("2- Kayıt Ol");
            Console.Write("Seçiminiz: ");
            string secim = Console.ReadLine();

            if (secim == "1") GirisYap();
            else if (secim == "2") KayitOl();
        }

        static void MenuAdmin()
        {
            Console.WriteLine($"\n=== [ADMİN PANELİ] Hoş geldin, {AktifKullanici.Username} ===");
            Console.WriteLine("1- Kullanıcıları Listele");
            Console.WriteLine("2- Yetki Ver (Admin Yap)");
            Console.WriteLine("3- Kullanıcı Sil");
            Console.WriteLine("4- Oturumu Kapat");
            Console.Write("Seçiminiz: ");
            string secim = Console.ReadLine();

            if (secim == "1") KullaniciListele();
            else if (secim == "2") YetkiVer();
            else if (secim == "3") KullaniciSil();
            else if (secim == "4") AktifKullanici = null;
        }

        static void MenuStandartKullanici()
        {
            Console.WriteLine($"\n=== [KULLANICI PANELİ] Merhaba, {AktifKullanici.Username} ===");
            Console.WriteLine("1- Profil Bilgilerim");
            Console.WriteLine("2- Oturumu Kapat");
            Console.Write("Seçiminiz: ");
            string secim = Console.ReadLine();

            if (secim == "1") Console.WriteLine($"Kullanıcı Adı: {AktifKullanici.Username} | Yetki: {AktifKullanici.Role}");
            else if (secim == "2") AktifKullanici = null;
        }

        // --- İŞLEMLER ---

        static void GirisYap()
        {
            Console.Write("Kullanıcı Adı: "); string u = Console.ReadLine();
            Console.Write("Şifre: "); string p = Console.ReadLine();

            var user = Veritabani.FirstOrDefault(x => x.Username == u && x.Password == p);
            if (user != null)
            {
                AktifKullanici = user;
                Console.WriteLine(">>> Giriş Başarılı!");
            }
            else Console.WriteLine("!!! Hata: Kullanıcı adı veya şifre yanlış.");
        }

        static void KayitOl()
        {
            Console.Write("Yeni Kullanıcı Adı: "); string u = Console.ReadLine();
            Console.Write("Şifre belirleyin: "); string p = Console.ReadLine();

            int yeniId = Veritabani.Count > 0 ? Veritabani.Max(x => x.Id) + 1 : 1;
            Veritabani.Add(new User { Id = yeniId, Username = u, Password = p, Role = "User" });
            Console.WriteLine(">>> Kayıt başarıyla tamamlandı.");
        }

        static void KullaniciListele()
        {
            Console.WriteLine("\n--- SİSTEMDEKİ KULLANICILAR ---");
            foreach (var u in Veritabani)
            {
                Console.WriteLine($"ID: {u.Id} | Ad: {u.Username} | Rol: {u.Role}");
            }
        }

        static void YetkiVer()
        {
            Console.Write("Yetki verilecek (Admin yapılacak) ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var user = Veritabani.FirstOrDefault(x => x.Id == id);
                if (user != null)
                {
                    user.Role = "Admin";
                    Console.WriteLine($">>> {user.Username} artık bir Admin!");
                }
                else Console.WriteLine("!!! Kullanıcı bulunamadı.");
            }
        }

        static void KullaniciSil()
        {
            Console.Write("Silinecek Kullanıcı ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (id == AktifKullanici.Id)
                {
                    Console.WriteLine("!!! Kendi hesabınızı silemezsiniz.");
                    return;
                }
                Veritabani.RemoveAll(x => x.Id == id);
                Console.WriteLine(">>> Kullanıcı silindi.");
            }
        }
    }
}
