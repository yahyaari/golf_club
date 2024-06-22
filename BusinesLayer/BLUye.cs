using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using DataAccesLayer;
using System.Xml.Linq;


namespace BusinesLayer
{
    public class BLUye
    {

        public static int kullaniciAdiSifreBelirle(EntityUserData p)
        {

            return DALUye.kullaniciAdiSifreBelirle(p);
           
        }

        public static int uyeEkle(EntityUye p)
        {
            if (p.Ad !=null && p.SoyAd != null && p.Ad.Length>2 && p.Ad.Length <50 && p.TC !=null && p.KanGrubu != null && p.Yas !=null && p.Sehir !=null)
                return DALUye.uyeEkle(p);
            
            return -1;
        }

        public static int uyeSil(EntityUye p)
        {
            if (p.TC != null)
                return DALUye.uyeSil(p);

            return uyeSil(p);
        }

        public static int kullaniciAdiSifreSil(EntityUye p)
        {
            if (p.TC != null)
                return DALUye.kullaniciAdiSifreSil(p);

            return uyeSil(p);
        }

        public static int uyeGuncelle(EntityUye p)
        {
            if (p.Ad != null && p.SoyAd != null && p.Ad.Length > 2 && p.Ad.Length < 50 && p.TC != null && p.KanGrubu != null && p.Yas != null && p.Sehir != null)
                return DALUye.uyeGuncelle(p);

            return -1;
        }

        public static int kullaniciAdiSifreGuncelle(EntityUserData p)
        {
            if (p.KullaniciAdi != null && p.Sifre != null && p.Yetki != null)
                return DALUye.kullaniciAdiSifreGuncelle(p);

            return -1;
        }

        public static List<EntityUye> uyeListele()
        {
            return DALUye.uyeListele();
        }

        


        public static List<EntityAidatlar> aidatListele()
        {
            return DALUye.GetAidatlar();
        }

        public static int aidatGuncelle(EntityAidatlar p)
        {
            if (p.Aralik != null)
                return DALUye.aidatGuncelle(p);

            return -1;
        }


        public static string GetAidatForMonth(int ay)
        {
            string aidat = "";

            List<EntityAidatlar> aidatlar = DALUye.GetAidatlar();

            if (ay >= 1 && ay <= 12)
            {
                switch (ay)
                {
                    case 1:
                        aidat = aidatlar[0].Ocak;
                        break;
                    case 2:
                        aidat = aidatlar[0].Subat;
                        break;
                    case 3:
                        aidat = aidatlar[0].Mart;
                        break;
                    case 4:
                        aidat = aidatlar[0].Nisan;
                        break;
                    case 5:
                        aidat = aidatlar[0].Mayis;
                        break;
                    case 6:
                        aidat = aidatlar[0].Haziran;
                        break;
                    case 7:
                        aidat = aidatlar[0].Temmuz;
                        break;
                    case 8:
                        aidat = aidatlar[0].Agustos;
                        break;
                    case 9:
                        aidat = aidatlar[0].Eylul;
                        break;
                    case 10:
                        aidat = aidatlar[0].Ekim;
                        break;
                    case 11:
                        aidat = aidatlar[0].Kasim;
                        break;
                    case 12:
                        aidat = aidatlar[0].Aralik;
                        break;
                    default:
                        break;
                }
            }

            return aidat;
        }

        public static bool OdemeYapildiMi(int yil, string ay)
        {
            // Burada daha önceki ödemelerin durumunu kontrol edebilirsiniz
            // Örneğin, bir koleksiyon içinde tutabilir veya başka bir tabloya kaydedebilirsiniz.

            // Örnek olarak bir koleksiyon kullanma:
            // Bu koleksiyonun tanımı class'ın dışında yapılmalıdır.
            List<OdemeDurumu> odemeDurumListesi = new List<OdemeDurumu>();

            // Kontrol et
            foreach (var odemeDurumu in odemeDurumListesi)
            {
                if (odemeDurumu.Yil == yil && odemeDurumu.Ay == ay && odemeDurumu.OdendiMi)
                {
                    return true;
                }
            }

            return false;
        }

        public static void MarkAsPaid(int yil, string ay)
        {
            // Ödeme durumunu işaretlemek için koleksiyonu veya başka bir tabloyu güncelleyebilirsiniz.

            // Örnek olarak bir koleksiyon kullanma:
            // Bu koleksiyonun tanımı class'ın dışında yapılmalıdır.
            List<OdemeDurumu> odemeDurumListesi = new List<OdemeDurumu>();

            // Güncelleme işlemi
            foreach (var odemeDurumu in odemeDurumListesi)
            {
                if (odemeDurumu.Yil == yil && odemeDurumu.Ay == ay)
                {
                    odemeDurumu.OdendiMi = true;
                    break;
                }
            }
        }

    }
}
