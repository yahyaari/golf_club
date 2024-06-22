using BusinesLayer;
using DataAccesLayer;
using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GolfDerneği
{
    public partial class UyePaneliFormu : Form
    {

        public int Yil { get; set; }
        public string Ay { get; set; }
        public bool OdendiMi { get; set; }


        public UyePaneliFormu()
        {
            InitializeComponent();

            
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.WindowState = FormWindowState.Maximized;

            
        }

        


        private void UyePaneliFormu_Load(object sender, EventArgs e)
        {
        
            UyeBilgileriGoruntulume();
            Guncelle();

            FillComboBox();

            OdemeDurumu odemeDurumu = new OdemeDurumu();

            odemeDurumu.OdendiMi = false;
        }

        private void FillComboBox()
        {
            // Şu anki yılı al
            int currentYear = DateTime.Now.Year;

            // ComboBox'ı temizle
            comboBox_Yil.Items.Clear();

            // Şu andan 5 yıl öncesine kadar olan yılları ComboBox'a ekle
            for (int year = currentYear; year >= currentYear - 4; year--)
            {
                comboBox_Yil.Items.Add(year);
            }

            // İlk öğeyi seçili hale getir (örneğin, şu anki yılı)
            comboBox_Yil.SelectedIndex = 0;


            // ComboBox'ı temizle
            comboBox_Ay.Items.Clear();

            // Tüm ayları ComboBox'a ekle
            for (int month = 1; month <= 12; month++)
            {
                comboBox_Ay.Items.Add(System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month));
            }

            // İlk öğeyi seçili hale getir (örneğin, Ocak)
            comboBox_Ay.SelectedIndex = 0;
        }

        void UyeBilgileriGoruntulume()
        {

            string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();

            List<EntityUye> girisYapanUye = DALUye.uyeGetir(kullaniciAdi);
            groupBox1.Text = "Kullanıcı Adı : " + girisYapanUye[0].Ad.ToString() + " " + girisYapanUye[0].SoyAd.ToString();
        }

        private void Btn_AidatOde_Click(object sender, EventArgs e)
        {


            int secilenYil = Convert.ToInt32(comboBox_Yil.SelectedItem);
            string secilenAy = comboBox_Ay.SelectedItem.ToString();

            
                // Aidatı ekleme işlemleri
                AidatEkleme();

                
                // Kullanıcı bilgilerini güncelleme işlemleri
                string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();
                string borcuYoktur = "Borcu Yoktur";

                List<EntityUye> girisYapanUye = DALUye.uyeGetir(kullaniciAdi);
               



                if (girisYapanUye.Count > 0 && girisYapanUye[0].Borc != 0)
                {

                    

                    

                    girisYapanUye[0].AidatOdendi = true;
                    girisYapanUye[0].BorcDurumu = borcuYoktur.ToString();
                    girisYapanUye[0].Borc = 0;
                    girisYapanUye[0].Durum = true;
                    BLUye.uyeGuncelle(girisYapanUye[0]);

                    MessageBox.Show("Aidat ödendi !");
                    
                    
                    Guncelle();
                }
                else
                {
                    MessageBox.Show("Giriş yapan kullanıcı bulunamadı !" + " Borcunuz yoktur. Ödeme işlemi yapılamadı !");
                }

                Yil = secilenYil;
                Ay = secilenAy;
                OdendiMi = true;
          
                

        }


        void BorcGuncelle()
        {

            // Seçilen yıl ve ay değerlerini al
            int secilenYil = Convert.ToInt32(comboBox_Yil.SelectedItem);
            string secilenAy = comboBox_Ay.SelectedItem.ToString();

            string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();

            // Borcu güncelle
            BorcuGuncelle(kullaniciAdi, secilenYil, secilenAy);

            

            Guncelle();
            
            

        }

        public void BorcuGuncelle(string tc, int secilenYil, string secilenAy)
        {
           
            

            List<EntityUye> uyeler = DALUye.uyeGetir(tc);

            
            int yeniBorc = DALUye.GetAidatBorc(secilenYil, secilenAy);


            Console.WriteLine($"Yeni Borç Değeri: {yeniBorc}");

            // Eğer daha önce bu yıl ve ay için ödeme yapıldıysa işlemi yapma
            if (OdendiMi)
            {
                Console.WriteLine("Bu yıl ve ay için daha önce ödeme yapıldı !");
                string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();



                List<EntityUye> girisYapanUye = DALUye.uyeGetir(kullaniciAdi);


                if (girisYapanUye.Count > 0)
                {

                    girisYapanUye[0].Borc = 0;


                    BLUye.uyeGuncelle(girisYapanUye[0]);


                    MessageBox.Show("Borç Güncellendi !");

                }
                else
                {
                    MessageBox.Show("Giriş yapan kullanıcı bulunamadı !");
                }
                return;
            }


            // Her bir üye için borcu güncelle
            foreach (EntityUye uye in uyeler)
            {

                Console.WriteLine($"Eski Borç Değeri: {uye.Borc}");

                uye.Borc = yeniBorc;

                string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();
                


                List<EntityUye> girisYapanUye = DALUye.uyeGetir(kullaniciAdi);


                if (girisYapanUye.Count > 0)
                {

                    girisYapanUye[0].Borc = yeniBorc;


                    BLUye.uyeGuncelle(girisYapanUye[0]);


                    MessageBox.Show("Borç Güncellendi !");

                }
                else
                {
                    MessageBox.Show("Giriş yapan kullanıcı bulunamadı !");
                }

                Console.WriteLine($"Yeni Borç Değeri (Güncellendikten Sonra): {uye.Borc}");
            }

            

        }

        void Guncelle()
        {
            string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();

            dataGridView_UyeBilgileri.DataSource = DALUye.uyeGetir(kullaniciAdi);


        }

        private void button1_Click(object sender, EventArgs e)
        {
            GirisFormu form = new GirisFormu();
            form.Show();
            this.Close();
        }

        void AidatEkleme()
        {
           

            string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();
            List<EntityUye> girisYapanUye = DALUye.uyeGetir(kullaniciAdi);

            // Seçili yılı ve ayı al
            int selectedYear = (int)comboBox_Yil.SelectedItem;
            string selectedMonth = comboBox_Ay.SelectedItem.ToString();

            // Belirtilen yıl ve ayda kayıt var mı kontrol et
            EntityAidatGelirleri existingRecord = DALUye.getAidatGelirleri(selectedYear);

            if (girisYapanUye.Count > 0)
            {
                if (existingRecord != null)
                {


                    // Kayıt varsa değeri al, üzerine 200 ekle ve güncelle
                    int existingValue = (int)typeof(EntityAidatGelirleri).GetProperty(selectedMonth).GetValue(existingRecord);
                    existingRecord.Toplam += girisYapanUye[0].Borc;
                    typeof(EntityAidatGelirleri).GetProperty(selectedMonth).SetValue(existingRecord, existingValue + girisYapanUye[0].Borc);

                    // Güncellenen kaydı veritabanına kaydet
                    DALUye.UpdateAidatRecord(existingRecord);
                }
                else
                {
                    // Kayıt yoksa yeni bir kayıt oluştur
                    EntityAidatGelirleri newRecord = new EntityAidatGelirleri
                    {
                        Tarih = selectedYear,
                        Toplam = girisYapanUye[0].Borc
                    };

                    // Ay değerini belirtilen değere ayarla
                    typeof(EntityAidatGelirleri).GetProperty(selectedMonth).SetValue(newRecord, girisYapanUye[0].Borc);

                    // Yeni kaydı veritabanına ekle
                    DALUye.UpdateAidatRecord(newRecord);
                }

            }
            else
            {
                MessageBox.Show("Giriş yapan kullanıcı bulunamadı!");
            }

            

            // Veritabanındaki değişiklikleri göstermek için gerekli işlemleri yapabilirsiniz.
            // Örneğin, grafiği yeniden çizmek veya başka bir kontrolü güncellemek.
        }

        private void comboBox_Yil_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BorcGuncelle();
            
        }

        private void comboBox_Ay_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            // Seçilen yıl ve ay değerlerini al
            int secilenYil = Convert.ToInt32(comboBox_Yil.SelectedItem);
            string secilenAy = comboBox_Ay.SelectedItem.ToString();
            

           

            if (Yil == secilenYil && Ay == secilenAy)
            {
                OdendiMi = true;
                MessageBox.Show("Borç Yok");
                BorcGuncelle();
            }
            else
            {
                OdendiMi = false;
                BorcGuncelle();
            }


           
        }
    }
}
