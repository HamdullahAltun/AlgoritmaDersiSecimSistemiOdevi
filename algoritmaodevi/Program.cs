using System;
using System.Linq;

namespace AlgoritmaOdevi
{
    class Program
    {
        

        static string[] isim = new string[20];
        static string[] mezuniyet = new string[20];
        static int[] saat = new int[20];
        static string[] oy = new string[20];
        static string kazanan_parti_adi = "";

        static bool oy_saat_kontrol(int saat)
        {
            return saat >= 8 && saat <= 18;
        }

        static string mezuniyet_kontrol()
        {
            while (true)
            {
                string mezuniyet = Console.ReadLine().ToLower();
                switch (mezuniyet)
                {
                    case "doktora":
                    case "yüksek lisans":
                    case "lisans":
                    case "lise":
                    case "ortaokul":
                    case "ilkokul":
                    case "gitmiyor":
                        return mezuniyet;
                    default:
                        Console.WriteLine("Hata: Geçersiz mezuniyet derecesi girişi!");
                        Console.WriteLine("Lütfen geçerli bir mezuniyet derecesi girin (doktora/yüksek lisans/lisans/lise/ortaokul/ilkokul/gitmiyor):");
                        break;
                }
            }
        }


        static double MezuniyetKatsayisi(string mezuniyet)
        {
            switch (mezuniyet.ToLower())
            {
                case "doktora":
                    return 5;
                case "yüksek lisans":
                case "lisans":
                    return 4;
                case "lise":
                    return 3;
                case "ortaokul":
                    return 2;
                case "ilkokul":
                    return 1;
                case "gitmiyor":
                    return 0.5;
                default:
                    Console.WriteLine("Hata: Geçersiz mezuniyet derecesi girişi!");
                    return 1.0;
            }
        }

        static double SaatKatsayisi(int oySaati)
        {
            return 10 - (oySaati - 8);
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                double sayi = i + 1;
                Convert.ToInt32(sayi);
                Console.WriteLine(sayi +". seçmenin adı ve soyadı?");
                isim[i] = Console.ReadLine();
                Console.WriteLine("\n" + sayi + ". seçmenin mezuniyet durumu (doktora/yüksek lisans/lisans/lise/ortaokul/ilkokul/gitmiyor)?");
                mezuniyet[i] = mezuniyet_kontrol();
                Console.WriteLine("\n" + sayi + ". seçmenin oy kullanma saati?");
                saat[i] = Convert.ToInt32(Console.ReadLine());
                if (oy_saat_kontrol(saat[i]))
                {
                    Console.WriteLine("\n" + sayi + ". seçmenin oy kullanacağı parti (akp/chp/iyi/ysp/zafer)?");
                    oy[i] = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\n" + sayi + ". seçmen oy kullanma saati dışında olduğu için oy kullanamaz!\n");
                }
            }

            double[] oy_katsayisi = new double[20];
            for (int i = 0; i < 20; i++)
            {
                double mezuniyetKatsayisi = MezuniyetKatsayisi(mezuniyet[i]);
                double saatKatsayisi = oy_saat_kontrol(saat[i]) ? SaatKatsayisi(saat[i]) : 0;
                oy_katsayisi[i] = mezuniyetKatsayisi * saatKatsayisi;
            }

            double[] parti_puan = new double[5];
            for (int i = 0; i < 20; i++)
            {
                switch (oy[i])
                {
                    case "akp":
                        parti_puan[0] += oy_katsayisi[i];
                        break;
                    case "chp":
                        parti_puan[1] += oy_katsayisi[i];
                        break;
                    case "iyi":
                        parti_puan[2] += oy_katsayisi[i];
                        break;
                    case "ysp":
                        parti_puan[3] += oy_katsayisi[i];
                        break;
                    case "zafer":
                        parti_puan[4] += oy_katsayisi[i];
                        break;
                }
            }

            double maxPartiPuan = parti_puan.Max();
            int kazanan_parti = Array.IndexOf(parti_puan, maxPartiPuan);
            switch (kazanan_parti)
            {
                case 0:
                    kazanan_parti_adi = "Ak Parti";
                    break;
                case 1:
                    kazanan_parti_adi = "CHP";
                    break;
                case 2:
                    kazanan_parti_adi = "İYİ Parti";
                    break;
                case 3:
                    kazanan_parti_adi = "Yeşil Sol Parti";
                    break;
                case 4:
                    kazanan_parti_adi = "Zafer Partisi";
                    break;
                default:
                    kazanan_parti_adi = "Kimse kazanamadı";
                    break;
            }

            Console.WriteLine("**Sonuçlar**");
            Console.WriteLine("Kazanan parti: " + kazanan_parti_adi);
            Console.WriteLine("Kazanan partinin oyu: " + maxPartiPuan);
        }
    }
}