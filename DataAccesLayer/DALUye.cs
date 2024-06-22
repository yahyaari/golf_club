using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer;
using System.Data.OleDb;
using System.Data;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Globalization;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;



namespace DataAccesLayer
{
    public class DALUye
    {

      

        public static int uyeEkle(EntityUye p)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Uye_Bilgileri (İsim,SoyIsim,TC,Şehir,Yaş,KanGrubu,Aidat,Durum,Yetki,EPosta,BorcDurumu,Borc) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12);", Connection.connection);

            cmd.Parameters.AddWithValue("@p1", p.Ad);
            cmd.Parameters.AddWithValue("@p2", p.SoyAd);
            cmd.Parameters.AddWithValue("@p3", p.TC);
            cmd.Parameters.AddWithValue("@p4", p.Sehir);
            cmd.Parameters.AddWithValue("@p5", p.Yas);
            cmd.Parameters.AddWithValue("@p6", p.KanGrubu);
            cmd.Parameters.AddWithValue("@p7", p.Aidat);
            cmd.Parameters.AddWithValue("@p8", p.Durum);
            cmd.Parameters.AddWithValue("@p9", p.Yetki);
            cmd.Parameters.AddWithValue("@p10", p.E_Posta);
            cmd.Parameters.AddWithValue("@p11", p.BorcDurumu);
            cmd.Parameters.AddWithValue("@p12", p.Borc);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            return cmd.ExecuteNonQuery();




        }

        public static int kullaniciAdiSifreBelirle(EntityUserData p)
        {
            OleDbCommand cmd = new OleDbCommand("INSERT INTO Giris_Bilgileri (kulAd, kulSifre, yetki) VALUES (@kullaniciAdi, @sifre, @yetki);", Connection.connection);

            cmd.Parameters.AddWithValue("@p1", p.KullaniciAdi);
            cmd.Parameters.AddWithValue("@p2", p.Sifre);
            cmd.Parameters.AddWithValue("@p3", p.Yetki);


            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            return cmd.ExecuteNonQuery();
        }

        public static bool TCVarMi(string tc)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT COUNT(*) FROM Uye_Bilgileri WHERE TC = @tc", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@tc", tc);

            int count = (int)cmd.ExecuteScalar();

            return count > 0;
        }


        public static int uyeSil(EntityUye p)
        {
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Uye_Bilgileri WHERE TC = @p1", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@p1", p.TC);


            return cmd.ExecuteNonQuery();
        }

        public static int kullaniciAdiSifreSil(EntityUye p)
        {
            OleDbCommand cmd = new OleDbCommand("DELETE FROM Giris_Bilgileri WHERE kulAd = @p1", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@p1", p.TC);


            return cmd.ExecuteNonQuery();
        }

        public static int uyeGuncelle(EntityUye p)
        {
            OleDbCommand cmd = new OleDbCommand("UPDATE Uye_Bilgileri SET İsim = @p1, SoyIsim = @p2, TC=@p3, Şehir = @p4, Yaş = @p5, KanGrubu = @p6, Aidat = @p7, Durum = @p8, Yetki = @p9, AidatOdendi = @p10, EPosta = @p11, BorcDurumu = @p12, Borc = @p13 WHERE TC=@p3", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@p1", p.Ad);
            cmd.Parameters.AddWithValue("@p2", p.SoyAd);
            cmd.Parameters.AddWithValue("@p3", p.TC);
            cmd.Parameters.AddWithValue("@p4", p.Sehir);
            cmd.Parameters.AddWithValue("@p5", p.Yas);
            cmd.Parameters.AddWithValue("@p6", p.KanGrubu);
            cmd.Parameters.AddWithValue("@p7", p.Aidat);
            cmd.Parameters.AddWithValue("@p8", p.Durum);
            cmd.Parameters.AddWithValue("@p9", p.Yetki);
            cmd.Parameters.AddWithValue("@p10", p.AidatOdendi);
            cmd.Parameters.AddWithValue("@p11", p.E_Posta);
            cmd.Parameters.AddWithValue("@p12", p.BorcDurumu);
            cmd.Parameters.AddWithValue("@p13", p.Borc);






            return cmd.ExecuteNonQuery();
        }

        public static int kullaniciAdiSifreGuncelle(EntityUserData p)
        {
            OleDbCommand cmd = new OleDbCommand("UPDATE Giris_Bilgileri SET kulAd = @p1, kulSifre = @p2, yetki=@p3 WHERE kulAd = @p1", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@p1", p.KullaniciAdi);
            cmd.Parameters.AddWithValue("@p2", p.Sifre);
            cmd.Parameters.AddWithValue("@p3", p.Yetki);


            return cmd.ExecuteNonQuery();
        }

        public static List<EntityUye> uyeListele()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityUye> uyeler = new List<EntityUye>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                ent.Ad = dr[0].ToString();
                ent.SoyAd = dr[1].ToString();
                ent.TC = dr[2].ToString();
                ent.Sehir = dr[3].ToString();
                ent.Yas = dr[4].ToString();
                ent.KanGrubu = dr[5].ToString();
                ent.Aidat = dr[6].ToString();
                ent.Durum = (bool)dr[7];
                ent.Yetki = dr[8].ToString();
                ent.AidatOdendi = (bool)dr[9];
                ent.E_Posta = dr[10].ToString();
                ent.BorcDurumu = dr[11].ToString();
                ent.Borc = (int)dr[12];

                uyeler.Add(ent);
            }


            return uyeler;
        }


        public static SortedDictionary<string, List<EntityUye>> UyeleriKanGrubunaGoreListele()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            // Kan gruplarına göre üyeleri gruplamak için bir sözlük kullanalım
            SortedDictionary<string, List<EntityUye>> kanGrupluUyeler = new SortedDictionary<string, List<EntityUye>>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                ent.Ad = dr[0].ToString();
                ent.SoyAd = dr[1].ToString();
                ent.TC = dr[2].ToString();
                ent.Sehir = dr[3].ToString();
                ent.Yas = dr[4].ToString();
                ent.KanGrubu = dr[5].ToString();
                ent.Aidat = dr[6].ToString();
                ent.Durum = (bool)dr[7];
                ent.Yetki = dr[8].ToString();
                ent.AidatOdendi = (bool)dr[9];
                ent.E_Posta = dr[10].ToString();

                // Üyenin kan grubunu alalım
                string kanGrubu = ent.KanGrubu;

                // Eğer kanGrupluUyeler içinde bu kan grubu daha önce eklenmemişse, ekleyelim
                if (!kanGrupluUyeler.ContainsKey(kanGrubu))
                {
                    kanGrupluUyeler[kanGrubu] = new List<EntityUye>();
                }

                // Bu kan grubundaki üyeler listesine ent'i ekleyelim
                kanGrupluUyeler[kanGrubu].Add(ent);
            }

            return kanGrupluUyeler;
        }

        public static SortedDictionary<string, List<EntityUye>> UyeleriSehireGoreListele()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            // Şehirlere göre üyeleri gruplamak için bir sıralı sözlük kullanalım
            SortedDictionary<string, List<EntityUye>> sehirliUyeler = new SortedDictionary<string, List<EntityUye>>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                ent.Ad = dr[0].ToString();
                ent.SoyAd = dr[1].ToString();
                ent.TC = dr[2].ToString();
                ent.Sehir = dr[3].ToString();
                ent.Yas = dr[4].ToString();
                ent.KanGrubu = dr[5].ToString();
                ent.Aidat = dr[6].ToString();
                ent.Durum = (bool)dr[7];
                ent.Yetki = dr[8].ToString();
                ent.AidatOdendi = (bool)dr[9];
                ent.E_Posta = dr[10].ToString();

                // Üyenin şehrini alalım
                string sehir = ent.Sehir;

                // Eğer sehirliUyeler içinde bu şehir daha önce eklenmemişse, ekleyelim
                if (!sehirliUyeler.ContainsKey(sehir))
                {
                    sehirliUyeler[sehir] = new List<EntityUye>();
                }

                // Bu şehirdeki üyeler listesine ent'i ekleyelim
                sehirliUyeler[sehir].Add(ent);
            }

            return sehirliUyeler;
        }

        public static SortedDictionary<bool, List<EntityUye>> UyeleriDurumaGoreListele()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            // Durumlara göre üyeleri gruplamak için bir sıralı sözlük kullanalım
            SortedDictionary<bool, List<EntityUye>> durumluUyeler = new SortedDictionary<bool, List<EntityUye>>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                ent.Ad = dr["İsim"].ToString(); // Sütun adlarını kullanmanız önerilir
                ent.SoyAd = dr["SoyIsim"].ToString();
                ent.TC = dr["TC"].ToString();
                ent.Sehir = dr["Şehir"].ToString();
                ent.Yas = dr["YaŞ"].ToString();
                ent.KanGrubu = dr["KanGrubu"].ToString();
                ent.Aidat = dr["Aidat"].ToString();
                ent.Durum = Convert.ToBoolean(dr["Durum"]);
                ent.Yetki = dr["Yetki"].ToString();
                ent.AidatOdendi = (bool)dr["AidatOdendi"];
                ent.E_Posta = dr["EPosta"].ToString();

                // Üyenin durumunu alalım
                bool durum = ent.Durum;

                // Eğer durumluUyeler içinde bu durum daha önce eklenmemişse, ekleyelim
                if (!durumluUyeler.ContainsKey(durum))
                {
                    durumluUyeler[durum] = new List<EntityUye>();
                }

                // Bu durumdaki üyeler listesine ent'i ekleyelim
                durumluUyeler[durum].Add(ent);
            }

            return durumluUyeler;
        }

        


        public static List<string> GetEpostalar()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT EPosta FROM Uye_Bilgileri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<string> epostalar = new List<string>();

            while (dr.Read())
            {
                string eposta = dr["EPosta"].ToString();
                epostalar.Add(eposta);
            }

            return epostalar;
        }

        public static List<string> GetOdenmemisAidatEpostaları()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT EPosta FROM Uye_Bilgileri WHERE AidatOdendi = False", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<string> epostalar = new List<string>();

            while (dr.Read())
            {
                string eposta = dr["EPosta"].ToString();
                epostalar.Add(eposta);
            }

            return epostalar;
        }

        public static List<EntityAidatlar> GetAidatlar()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Aidatlar", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityAidatlar> aidatlar = new List<EntityAidatlar>();

            while (dr.Read())
            {
                EntityAidatlar aylar = new EntityAidatlar();

                aylar.Ocak = dr[0].ToString();
                aylar.Subat = dr[1].ToString();
                aylar.Mart = dr[2].ToString();
                aylar.Nisan = dr[3].ToString();
                aylar.Mayis = dr[4].ToString();
                aylar.Haziran = dr[5].ToString();
                aylar.Temmuz = dr[6].ToString();
                aylar.Agustos = dr[7].ToString();
                aylar.Eylul = dr[8].ToString();
                aylar.Ekim = dr[9].ToString();
                aylar.Kasim = dr[10].ToString();
                aylar.Aralik = dr[11].ToString();


                aidatlar.Add(aylar);
            }


            return aidatlar;
        }

        public static int aidatGuncelle(EntityAidatlar p)
        {
            OleDbCommand cmd = new OleDbCommand("UPDATE Aidatlar SET Ocak = @p1, Subat = @p2, Mart=@p3, Nisan = @p4, Mayis = @p5, Haziran = @p6, Temmuz = @p7, Agustos = @p8, Eylul = @p9, Ekim = @p10, Kasim = @p11, Aralik = @p12", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            cmd.Parameters.AddWithValue("@p1", p.Ocak);
            cmd.Parameters.AddWithValue("@p2", p.Subat);
            cmd.Parameters.AddWithValue("@p3", p.Mart);
            cmd.Parameters.AddWithValue("@p4", p.Nisan);
            cmd.Parameters.AddWithValue("@p5", p.Mayis);
            cmd.Parameters.AddWithValue("@p6", p.Haziran);
            cmd.Parameters.AddWithValue("@p7", p.Temmuz);
            cmd.Parameters.AddWithValue("@p8", p.Agustos);
            cmd.Parameters.AddWithValue("@p9", p.Eylul);
            cmd.Parameters.AddWithValue("@p10", p.Ekim);
            cmd.Parameters.AddWithValue("@p11", p.Kasim);
            cmd.Parameters.AddWithValue("@p12", p.Aralik);







            return cmd.ExecuteNonQuery();
        }

        public static List<EntityUye> BorcuOlanlariListele()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri WHERE BorcDurumu = 'Borçlu'", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityUye> borcuOlanlar = new List<EntityUye>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                // Diğer sütun değerlerini okuma işlemleri buraya eklenebilir
                ent.Ad = dr["İsim"].ToString();
                ent.SoyAd = dr["SoyIsim"].ToString();
                ent.TC = dr["TC"].ToString();
                ent.Sehir = dr["Şehir"].ToString();
                ent.Yas = dr["YaŞ"].ToString();
                ent.KanGrubu = dr["KanGrubu"].ToString();
                ent.Aidat = dr["Aidat"].ToString();
                ent.Durum = Convert.ToBoolean(dr["Durum"]);
                ent.Yetki = dr["Yetki"].ToString();
                ent.AidatOdendi = (bool)dr["AidatOdendi"];
                ent.E_Posta = dr["EPosta"].ToString();
                // ... Diğer sütunlar

                borcuOlanlar.Add(ent);
            }

            return borcuOlanlar;
        }

        public static List<EntityUye> uyeGetir(string tc)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Uye_Bilgileri WHERE TC = @tc", Connection.connection);
            cmd.Parameters.AddWithValue("@tc", tc);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityUye> uyeler = new List<EntityUye>();

            while (dr.Read())
            {
                EntityUye ent = new EntityUye();

                ent.Ad = dr[0].ToString();
                ent.SoyAd = dr[1].ToString();
                ent.TC = dr[2].ToString();
                ent.Sehir = dr[3].ToString();
                ent.Yas = dr[4].ToString();
                ent.KanGrubu = dr[5].ToString();
                ent.Aidat = dr[6].ToString();
                ent.Durum = (bool)dr[7];
                ent.Yetki = dr[8].ToString();
                ent.AidatOdendi = (bool)dr[9];
                ent.E_Posta = dr[10].ToString();
                ent.BorcDurumu = dr[11].ToString();
                ent.Borc = (int)dr[12];

                uyeler.Add(ent);
            }

            return uyeler;
        }


        public static List<EntityUserData> kullaniciTanıma(EntityUserData p)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Giris_Bilgileri WHERE kulAd = @kullaniciAdi", Connection.connection);
            cmd.Parameters.AddWithValue("@kullaniciAdi", p.KullaniciAdi);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityUserData> uyeler = new List<EntityUserData>();

            while (dr.Read())
            {
                EntityUserData ent = new EntityUserData();

                ent.KullaniciAdi = dr["kulAd"].ToString();
                ent.Sifre = dr["kulSifre"].ToString();
                ent.Yetki = dr["yetki"].ToString();

                uyeler.Add(ent);
            }

            return uyeler;
        }

        public static string yetkiCekKulAd(string kullaniciAdi)
        {
            OleDbCommand cmd = new OleDbCommand("SELECT yetki FROM Giris_Bilgileri WHERE kulAd = @p1", Connection.connection);
            cmd.Parameters.AddWithValue("@p1", kullaniciAdi);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            object yetkiObj = cmd.ExecuteScalar();

            if (yetkiObj != null)
            {
                return yetkiObj.ToString();
            }
            else
            {
                return null; // Kullanıcı adına göre bir eşleşme bulunamadıysa null dönebilirsiniz.
            }
        }

        public static DataTable GetUyeSayilariBySehir()
        {
            DataTable dt = new DataTable();

            try
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT Şehir, COUNT(*) as UyeSayisi FROM Uye_Bilgileri GROUP BY Şehir", Connection.connection))
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimini burada gerçekleştirin (örneğin, MessageBox ile hata mesajı gösterme)
                Console.WriteLine("Hata: " + ex.Message);
            }

            return dt;
        }



        public static List<EntityAidatGelirleri> aidatGelirleriniGetir()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT * FROM Aidat_Gelirleri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<EntityAidatGelirleri> aidatlar = new List<EntityAidatGelirleri>();

            while (dr.Read())
            {
                EntityAidatGelirleri ent = new EntityAidatGelirleri();

                ent.Tarih = (int)dr[0];
                ent.Ocak = (int)dr[1];
                ent.Şubat = (int)dr[2];
                ent.Mart = (int)dr[3];
                ent.Nisan = (int)dr[4];
                ent.Mayıs = (int)dr[5];
                ent.Haziran = (int)dr[6];
                ent.Temmuz = (int)dr[7];
                ent.Ağustos = (int)dr[8];
                ent.Eylül = (int)dr[9];
                ent.Ekim = (int)dr[10];
                ent.Kasım = (int)dr[11];
                ent.Aralık = (int)dr[12];
                ent.Toplam = (int)dr[13];


                aidatlar.Add(ent);
            }


            return aidatlar;
        }

        public static void AidatGelirleriniToplaVeKaydet()
        {
            List<EntityAidatGelirleri> aidatlar = aidatGelirleriniGetir(); // Mevcut aidat verilerini getir

            foreach (var aidat in aidatlar)
            {
                // Her bir aidat nesnesi için ay toplamını hesapla
                double ayToplam = aidat.Ocak + aidat.Şubat + aidat.Mart + aidat.Nisan + aidat.Mayıs + aidat.Haziran +
                            aidat.Temmuz + aidat.Ağustos + aidat.Eylül + aidat.Ekim + aidat.Kasım + aidat.Aralık;

                // Ay toplamını ilgili toplam sütununa güncelle
                string updateQuery = $"UPDATE Aidat_Gelirleri SET Toplam = {ayToplam} WHERE Tarih = {aidat.Tarih}";

                using (OleDbCommand cmd = new OleDbCommand(updateQuery, Connection.connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }


        }

       

        public static EntityAidatGelirleri getAidatGelirleri(int selectedYear)
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT * FROM Aidat_Gelirleri WHERE Tarih = {selectedYear}", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                EntityAidatGelirleri ent = new EntityAidatGelirleri();

                ent.Tarih = (int)dr[0];
                ent.Ocak = (int)dr[1];
                ent.Şubat = (int)dr[2];
                ent.Mart = (int)dr[3];
                ent.Nisan = (int)dr[4];
                ent.Mayıs = (int)dr[5];
                ent.Haziran = (int)dr[6];
                ent.Temmuz = (int)dr[7];
                ent.Ağustos = (int)dr[8];
                ent.Eylül = (int)dr[9];
                ent.Ekim = (int)dr[10];
                ent.Kasım = (int)dr[11];
                ent.Aralık = (int)dr[12];
                ent.Toplam = (int)dr[13];

                return ent;
            }

            return null; // veya uygun bir hata durumu
        }


        public static List<int> GetTarihList()
        {
            OleDbCommand cmd = new OleDbCommand("SELECT Tarih FROM Aidat_Gelirleri", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            List<int> tarihListesi = new List<int>();

            while (dr.Read())
            {
                int tarih = (int)dr["Tarih"];
                tarihListesi.Add(tarih);
            }

            // Tarihleri sırala
            tarihListesi.Sort();

            return tarihListesi;
        }

        public static void UpdateAidatRecord(EntityAidatGelirleri existingRecord)
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT * FROM Aidat_Gelirleri WHERE Tarih={existingRecord.Tarih}", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            // Eğer yılın kaydı mevcutsa güncelle
            if (dr.Read())
            {
                dr.Close(); // Önceki okumayı kapat

                // Güncelleme sorgusu
                cmd.CommandText = $"UPDATE Aidat_Gelirleri SET " +
                                  $"Ocak=@Ocak, Şubat=@Subat, Mart=@Mart, Nisan=@Nisan, Mayıs=@Mayis, Haziran=@Haziran, " +
                                  $"Temmuz=@Temmuz, Ağustos=@Agustos, Eylül=@Eylul, Ekim=@Ekim, Kasım=@Kasim, Aralık=@Aralik, Toplam=@Toplam " +
                                  $"WHERE Tarih={existingRecord.Tarih}";

                // Parametre değerlerini ekleyin
                cmd.Parameters.AddWithValue("@Ocak", existingRecord.Ocak);
                cmd.Parameters.AddWithValue("@Subat", existingRecord.Şubat);
                cmd.Parameters.AddWithValue("@Mart", existingRecord.Mart);
                cmd.Parameters.AddWithValue("@Nisan", existingRecord.Nisan);
                cmd.Parameters.AddWithValue("@Mayis", existingRecord.Mayıs);
                cmd.Parameters.AddWithValue("@Haziran", existingRecord.Haziran);
                cmd.Parameters.AddWithValue("@Temmuz", existingRecord.Temmuz);
                cmd.Parameters.AddWithValue("@Agustos", existingRecord.Ağustos);
                cmd.Parameters.AddWithValue("@Eylul", existingRecord.Eylül);
                cmd.Parameters.AddWithValue("@Ekim", existingRecord.Ekim);
                cmd.Parameters.AddWithValue("@Kasim", existingRecord.Kasım);
                cmd.Parameters.AddWithValue("@Aralik", existingRecord.Aralık);
                cmd.Parameters.AddWithValue("@Toplam", existingRecord.Toplam);

                // Güncelleme sorgusunu çalıştır
                cmd.ExecuteNonQuery();
            }
            else // Eğer yılın kaydı yoksa yeni kayıt ekle
            {
                dr.Close(); // Önceki okumayı kapat

                // Ekleme sorgusu
                cmd.CommandText = $"INSERT INTO Aidat_Gelirleri (Tarih, Ocak, Şubat, Mart, Nisan, Mayıs, Haziran, " +
                                  $"Temmuz, Ağustos, Eylül, Ekim, Kasım, Aralık, Toplam) " +
                                  $"VALUES (@Tarih, @Ocak, @Subat, @Mart, @Nisan, @Mayis, @Haziran, " +
                                  $"@Temmuz, @Agustos, @Eylul, @Ekim, @Kasim, @Aralik, @Toplam)";

                // Parametre değerlerini ekleyin
                cmd.Parameters.AddWithValue("@Tarih", existingRecord.Tarih);
                cmd.Parameters.AddWithValue("@Ocak", existingRecord.Ocak);
                cmd.Parameters.AddWithValue("@Subat", existingRecord.Şubat);
                cmd.Parameters.AddWithValue("@Mart", existingRecord.Mart);
                cmd.Parameters.AddWithValue("@Nisan", existingRecord.Nisan);
                cmd.Parameters.AddWithValue("@Mayis", existingRecord.Mayıs);
                cmd.Parameters.AddWithValue("@Haziran", existingRecord.Haziran);
                cmd.Parameters.AddWithValue("@Temmuz", existingRecord.Temmuz);
                cmd.Parameters.AddWithValue("@Agustos", existingRecord.Ağustos);
                cmd.Parameters.AddWithValue("@Eylul", existingRecord.Eylül);
                cmd.Parameters.AddWithValue("@Ekim", existingRecord.Ekim);
                cmd.Parameters.AddWithValue("@Kasim", existingRecord.Kasım);
                cmd.Parameters.AddWithValue("@Aralik", existingRecord.Aralık);
                cmd.Parameters.AddWithValue("@Toplam", existingRecord.Toplam);

                // Ekleme sorgusunu çalıştır
                cmd.ExecuteNonQuery();
            }
        }

       

        public static void UpdateOrInsertCurrentYear(EntityAidatlar p, int currentYear)
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT * FROM Alınacak_Aidatlar WHERE Tarih={currentYear}", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            OleDbDataReader dr = cmd.ExecuteReader();

            // Eğer yılın kaydı mevcutsa güncelle
            if (dr.Read())
            {
                dr.Close(); // Önceki okumayı kapat

                // Güncelleme sorgusu
                cmd.CommandText = $"UPDATE Alınacak_Aidatlar SET Ocak=@Ocak, Şubat=@Subat, Mart=@Mart, Nisan=@Nisan, Mayıs=@Mayis, Haziran=@Haziran, " +
                                  $"Temmuz=@Temmuz, Ağustos=@Agustos, Eylül=@Eylul, Ekim=@Ekim, Kasım=@Kasim, Aralık=@Aralik " +
                                  $"WHERE Tarih={currentYear}";

                cmd.Parameters.AddWithValue("@Ocak", p.Ocak);
                cmd.Parameters.AddWithValue("@Subat", p.Subat);
                cmd.Parameters.AddWithValue("@Mart", p.Mart);
                cmd.Parameters.AddWithValue("@Nisan", p.Nisan);
                cmd.Parameters.AddWithValue("@Mayis", p.Mayis);
                cmd.Parameters.AddWithValue("@Haziran", p.Haziran);
                cmd.Parameters.AddWithValue("@Temmuz",  p.Temmuz);
                cmd.Parameters.AddWithValue("@Agustos", p.Agustos);
                cmd.Parameters.AddWithValue("@Eylul", p.Eylul);
                cmd.Parameters.AddWithValue("@Ekim", p.Ekim);
                cmd.Parameters.AddWithValue("@Kasim", p.Kasim);
                cmd.Parameters.AddWithValue("@Aralik", p.Aralik);

                // Güncelleme sorgusunu çalıştır
                cmd.ExecuteNonQuery();
            }
            else // Eğer yılın kaydı yoksa yeni kayıt ekle
            {
                dr.Close(); // Önceki okumayı kapat

                // Ekleme sorgusu
                cmd.CommandText = $"INSERT INTO Alınacak_Aidatlar (Tarih, Ocak, Şubat, Mart, Nisan, Mayıs, Haziran, " +
                                  $"Temmuz, Ağustos, Eylül, Ekim, Kasım, Aralık) " +
                                  $"VALUES (@Tarih, @Ocak, @Subat, @Mart, @Nisan, @Mayis, @Haziran, " +
                                  $"@Temmuz, @Agustos, @Eylul, @Ekim, @Kasim, @Aralik)";

                cmd.Parameters.AddWithValue("Tarih", currentYear);
                cmd.Parameters.AddWithValue("@Ocak", p.Ocak);
                cmd.Parameters.AddWithValue("@Subat", p.Subat);
                cmd.Parameters.AddWithValue("@Mart", p.Mart);
                cmd.Parameters.AddWithValue("@Nisan", p.Nisan);
                cmd.Parameters.AddWithValue("@Mayis", p.Mayis);
                cmd.Parameters.AddWithValue("@Haziran", p.Haziran);
                cmd.Parameters.AddWithValue("@Temmuz", p.Temmuz);
                cmd.Parameters.AddWithValue("@Agustos", p.Agustos);
                cmd.Parameters.AddWithValue("@Eylul", p.Eylul);
                cmd.Parameters.AddWithValue("@Ekim", p.Ekim);
                cmd.Parameters.AddWithValue("@Kasim", p.Kasim);
                cmd.Parameters.AddWithValue("@Aralik", p.Aralik);

                // Ekleme sorgusunu çalıştır
                cmd.ExecuteNonQuery();
            }
        }


       


        public static int GetAidatBorc(int yil, string ay)
        {
            OleDbCommand cmd = new OleDbCommand($"SELECT {ay} FROM Alınacak_Aidatlar WHERE Tarih=@tarih", Connection.connection);

            if (cmd.Connection.State != ConnectionState.Open)
            {
                cmd.Connection.Open();
            }

            // Tarih'i yıl ve ayı birleştirerek oluştur
            string tarih = $"{yil}";

            cmd.Parameters.AddWithValue("@tarih", tarih);

            OleDbDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                return (int)dr[0];
            }

            return 0; // Eğer belirtilen yıl ve ay değerine karşılık gelen kayıt bulunamazsa 0 dönebilirsiniz.
        }

        

        public static DataTable GetYapilanOdemeler()
        {
            DataTable dt = new DataTable();

            try
            {
                using (OleDbCommand cmd = new OleDbCommand("SELECT * FROM Yapılan_Odemeler", Connection.connection))
                {
                    if (cmd.Connection.State != ConnectionState.Open)
                    {
                        cmd.Connection.Open();
                    }

                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata yönetimini burada gerçekleştirin (örneğin, MessageBox ile hata mesajı gösterme)
                Console.WriteLine("Hata: " + ex.Message);
            }

            return dt;
        }


        
    }
}
