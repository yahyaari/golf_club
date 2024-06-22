using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EntityLayer;
using DataAccesLayer;
using BusinesLayer;
using System.Data.OleDb;
using System.Net.Mail;
using System.Net;
using iTextSharp;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using ZedGraph;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace GolfDerneği
{
    public partial class AdminPaneliFormu : Form
    {
        private int selectedRowIndex;

        public AdminPaneliFormu()
        {
            InitializeComponent();
            Timer timer = new Timer();

            // Timer oluşturun
            timer = new Timer();
            timer.Interval = 1000; // 1 saniyede bir çalışacak
            timer.Tick += timer1_Tick;

            // Timer'ı başlatın
            timer.Start();

            // Form yüklendiğinde otomatik boyutlandırma ayarları
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.WindowState = FormWindowState.Maximized;


            

        }






        private void timer1_Tick(object sender, EventArgs e)
        {
            // Her zaman etkin olan bu olay, timer'ın tetiklenmesi durumunda çalışacaktır
            labelTarihSaat.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }



        void Listele()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear(); // Sütunları temizle

            dataGridView1.DataSource = BLUye.uyeListele();
            textBox_Ad.Text = "";
            textBox_Soyad.Text = "";
            textBox_TC.Text = "";
            textBox_Sehir.Text = "";
            textBox_Yas.Text = "";
            textBox_Kangurubu.Text = "";
            
            textBox_ePosta.Text = "";
            
            textBox_Yetki.Text = "";
            

            AidatBelirleme();
           
        }


        

       

        void AidatListele()
        {
            dataGridView_Aidat.DataSource = null;
            dataGridView_Aidat.Rows.Clear();
            dataGridView_Aidat.Columns.Clear(); // Sütunları temizle

            dataGridView_Aidat.DataSource = BLUye.aidatListele();

            textBox_Ocak.Text = "";
            textBox_Subat.Text = "";
            textBox_Mart.Text = "";
            textBoxNisan.Text = "";
            textBoxMayıs.Text = "";
            textBox_Haziran.Text = "";
            textBox_Temmuz.Text = "";
            textBox_Agustos.Text = "";
            textBox_Eylul.Text = "";
            textBox_Ekim.Text = "";
            textBox_Kasim.Text = "";
            textBox_Aralik.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
            AidatListele();

            // ZedGraphControl'ün konfigürasyonunu yap
            ConfigureZedGraph_Sehirler(zedGraph_Sehirler);

            // Veritabanından şehirlere göre üye sayılarını getir
            DataTable dt = DALUye.GetUyeSayilariBySehir();



            // Grafiği çiz
            DrawBarChart_Sehirler(zedGraph_Sehirler, dt);

            ZedGraphYıllıkGeliriGoster();
            


            FillComboBoxWithDates();

            string kullaniciAdi = GirisFormu.gkullaniciAdi.ToString();

            List<EntityUye> girisYapanAdmin = DALUye.uyeGetir(kullaniciAdi);
            AdminPaneliFormu form = new AdminPaneliFormu();
            this.Text = "Yönetici Paneli - Yönetici Adı : " + girisYapanAdmin[0].Ad.ToString() + " " + girisYapanAdmin[0].SoyAd.ToString();



        }


        void AidatBelirleme()
        {
            DateTime tarih = DateTime.Now;
            int currentAy = tarih.Month;
            

            string aidat = BLUye.GetAidatForMonth(currentAy);


            textBox_Aidat.Text = aidat;
        }
        private void FillComboBoxWithDates()
        {

            List<int> tarihler = DALUye.GetTarihList();
            comboBox_Tarihler.DataSource = tarihler;
            

        }


        private void Btn_Listele_Click(object sender, EventArgs e)
        {
            Listele();
            GelirHesapla();
        }


        private void button1_Click(object sender, EventArgs e)
        {

            kullaniciAdiSifreBelirle();

        }

        void kullaniciAdiSifreBelirle()
        {
            EntityUserData userData = new EntityUserData();

            userData.KullaniciAdi = textBox_kullaniciAdi.Text;
            userData.Sifre = textBox_sifre.Text;
            userData.Yetki = textBox_yetkii.Text;

            BLUye.kullaniciAdiSifreBelirle(userData);

            MessageBox.Show("Kullanıcı Adı Sifre Belirlendi");
        }

        private void Btn_Ekle_Click(object sender, EventArgs e) // Üye Ekleme İşlemi
        {


            
            if (textBox_Ad.Text == "" || textBox_Soyad.Text == "" || textBox_TC.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Alanları Giriniz!", "Hata");
            }
            else
            {

                EntityUye uye = new EntityUye();
                uye.Ad = textBox_Ad.Text;
                uye.SoyAd = textBox_Soyad.Text;
                uye.TC = textBox_TC.Text;
                uye.Sehir = textBox_Sehir.Text;
                uye.Yas = textBox_Yas.Text;
                uye.KanGrubu = textBox_Kangurubu.Text;
                
                uye.Yetki = textBox_Yetki.Text;
                uye.E_Posta = textBox_ePosta.Text;

                uye.Aidat = textBox_Aidat.Text;

                uye.BorcDurumu = "Borçlu";
                uye.Borc = Convert.ToInt32(textBox_Aidat.Text);

                if (DALUye.TCVarMi(uye.TC) == false)
                {
                    try
                    {
                        // İlk SQL ifadesi
                        BLUye.uyeEkle(uye);
                       



                        MessageBox.Show("Kayıt Tamam");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex.Message);
                    }
                    finally
                    {
                        
                    }

                    kullaniciAdiSifreBelirle();


                   

                }
                else
                {
                    MessageBox.Show("Bu TC kimlik numarası sisteme kayıtlı başka bir TC kimlik numarası deneyin");
                }

                Listele();
            }


        }

        private void Btn_Sil_Click(object sender, EventArgs e)
        {
            if (textBox_TC.Text == "" )
            {
                MessageBox.Show("Lütfen Silinecek Kişiyi Seçiniz!", "Hata");
            }
            else
            {
                EntityUye uye = new EntityUye();
                uye.TC = textBox_TC.Text;
                BLUye.uyeSil(uye);

                

                BLUye.kullaniciAdiSifreSil(uye);

                MessageBox.Show("Silme İşlemi Tamam");
            }
            Listele();




        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            AidatBelirleme();

            textBox_Ad.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox_Soyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox_TC.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox_Sehir.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox_Yas.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox_Kangurubu.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            
            textBox_Yetki.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox_ePosta.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            

            textBox_kullaniciAdi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox_sifre.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox_yetkii.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            try
            {
                // Seçilen hücrenin olduğu satır index'ini al
                selectedRowIndex = e.RowIndex;

                // İlgili hücrenin değerini alabilirsiniz
                string selectedCellValue = dataGridView1.Rows[selectedRowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (Exception ex)
            {

                
            }

            





        }

        private void Btn_Guncelle_Click(object sender, EventArgs e)
        {
            if (textBox_Ad.Text == "" || textBox_Soyad.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Alanları Giriniz!", "Hata");
            }
            else
            {

                EntityUye uye = new EntityUye();

                uye.Ad = textBox_Ad.Text;
                uye.SoyAd = textBox_Soyad.Text;
                uye.TC = textBox_TC.Text;
                uye.Sehir = textBox_Sehir.Text;
                uye.Yas = textBox_Yas.Text;
                uye.KanGrubu = textBox_Kangurubu.Text;
                uye.Aidat = textBox_Aidat.Text;
                uye.Yetki = textBox_Yetki.Text;
                uye.E_Posta = textBox_ePosta.Text;

                try
                {
                    if (selectedRowIndex > 0)
                    {





                        // DataGridView'de ilgili hücrelerden değerleri al
                        string borcDurumu = dataGridView1.Rows[selectedRowIndex].Cells["BorcDurumu"].Value.ToString();
                        int borc = (int)dataGridView1.Rows[selectedRowIndex].Cells["Borc"].Value;
                        string durumStr = dataGridView1.Rows[selectedRowIndex].Cells["Durum"].Value.ToString();
                        string aidatOdendiStr = dataGridView1.Rows[selectedRowIndex].Cells["AidatOdendi"].Value.ToString();

                        bool durum = Convert.ToBoolean(durumStr);
                        bool aidatOdendi = Convert.ToBoolean(aidatOdendiStr);

                        uye.Durum = durum;
                        uye.AidatOdendi = aidatOdendi;
                        uye.BorcDurumu = borcDurumu;
                        uye.Borc = borc;
                        // Burada alınan değerleri kullanarak işlemleri gerçekleştir
                        // Örneğin: Aidat ödendi işlemi veya başka bir işlem

                        BLUye.uyeGuncelle(uye);
                    }
                    else
                    {
                        MessageBox.Show("Lütfen bir satır seçin!");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Hata" + ex.Message);
                }


                






                EntityUserData userData = new EntityUserData();


                userData.KullaniciAdi = textBox_kullaniciAdi.Text; // Güncellenmesini istediğiniz kullanıcı adını belirleyin
                userData.Sifre = textBox_kullaniciAdi.Text; // Yeni şifreyi belirleyin
                userData.Yetki = textBox_yetkii.Text; // Yeni yetkiyi belirleyin

                BLUye.kullaniciAdiSifreGuncelle(userData);

                MessageBox.Show("Üye Güncellendi!");
            }
            Listele();
        }

        private void Btn_KanGrubuListele_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Btn_KanGrubuListele_Click_1(object sender, EventArgs e)
        {
            SortedDictionary<string, List<EntityUye>> kanGrupluUyeler = DALUye.UyeleriKanGrubunaGoreListele();

            // DataGridView temizleme
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear(); // Sütunları temizle

            // DataGridView'a sütunları ekle
            dataGridView2.Columns.Add("KanGrubu", "Kan Grubu");
            dataGridView2.Columns.Add("Ad", "Ad");
            dataGridView2.Columns.Add("SoyAd", "Soyad");
            dataGridView2.Columns.Add("TC", "TC");
            dataGridView2.Columns.Add("Şehir", "Şehir");
            dataGridView2.Columns.Add("Yaş", "Yaş");
            dataGridView2.Columns.Add("Aidat", "Aidat");

            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = "Durum";
            checkboxColumn.Name = "Durum";
            dataGridView2.Columns.Add(checkboxColumn);

            //dataGridView2.Columns.Add("Durum", "Durum");
            dataGridView2.Columns.Add("Yetki", "Yetki");

            DataGridViewCheckBoxColumn checkboxColumn1 = new DataGridViewCheckBoxColumn();
            checkboxColumn1.HeaderText = "AidatOdendi";
            checkboxColumn1.Name = "AidatOdendi";
            dataGridView2.Columns.Add(checkboxColumn1);

            //dataGridView2.Columns.Add("AidatOdendi", "AidatOdendi");
            dataGridView2.Columns.Add("E-Posta", "E-Posta");
            // Diğer sütunları ekleyin...

            // Veriyi DataGridView'a ekle
            foreach (var kanGrubu in kanGrupluUyeler.Keys)
            {
                foreach (var uye in kanGrupluUyeler[kanGrubu])
                {
                    int rowIndex = dataGridView2.Rows.Add();
                    dataGridView2.Rows[rowIndex].Cells["KanGrubu"].Value = uye.KanGrubu;
                    dataGridView2.Rows[rowIndex].Cells["Ad"].Value = uye.Ad;
                    dataGridView2.Rows[rowIndex].Cells["SoyAd"].Value = uye.SoyAd;
                    dataGridView2.Rows[rowIndex].Cells["TC"].Value = uye.TC;
                    dataGridView2.Rows[rowIndex].Cells["Şehir"].Value = uye.Sehir;
                    dataGridView2.Rows[rowIndex].Cells["Yaş"].Value = uye.Yas;
                    dataGridView2.Rows[rowIndex].Cells["Aidat"].Value = uye.Aidat;
                    dataGridView2.Rows[rowIndex].Cells["Durum"].Value = uye.Durum;
                    dataGridView2.Rows[rowIndex].Cells["Yetki"].Value = uye.Yetki;
                    dataGridView2.Rows[rowIndex].Cells["AidatOdendi"].Value = uye.AidatOdendi;
                    dataGridView2.Rows[rowIndex].Cells["E-Posta"].Value = uye.E_Posta;
                    // Diğer hücreleri de ekleyin...
                }
            }
        }

        private void Btn_SehirListele_Click(object sender, EventArgs e)
        {
            // UyeleriSehireGoreListele methodunu çağırarak şehirlere göre üyeleri al
            SortedDictionary<string, List<EntityUye>> sehirliUyeler = DALUye.UyeleriSehireGoreListele();

            // DataGridView temizleme
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear(); // Sütunları temizle

            // DataGridView'a sütunları ekle



            dataGridView2.Columns.Add("Sehir", "Şehir");
            dataGridView2.Columns.Add("Ad", "Ad");
            dataGridView2.Columns.Add("SoyAd", "Soyad");
            dataGridView2.Columns.Add("TC", "TC");
            dataGridView2.Columns.Add("Yaş", "Yaş");
            dataGridView2.Columns.Add("Kan Gurubu", "Kan Gurubu");
            dataGridView2.Columns.Add("Aidat", "Aidat");

            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = "Durum";
            checkboxColumn.Name = "Durum";
            dataGridView2.Columns.Add(checkboxColumn);


            //dataGridView2.Columns.Add("Durum", "Durum");
            dataGridView2.Columns.Add("Yetki", "Yetki");

            DataGridViewCheckBoxColumn checkboxColumn1 = new DataGridViewCheckBoxColumn();
            checkboxColumn1.HeaderText = "AidatOdendi";
            checkboxColumn1.Name = "AidatOdendi";
            dataGridView2.Columns.Add(checkboxColumn1);

            //dataGridView2.Columns.Add("AidatOdendi", "AidatOdendi");
            dataGridView2.Columns.Add("E-Posta", "E-Posta");
            // Diğer sütunları ekleyin...

            // Veriyi DataGridView'a ekle
            foreach (var sehir in sehirliUyeler.Keys)
            {
                foreach (var uye in sehirliUyeler[sehir])
                {
                    int rowIndex = dataGridView2.Rows.Add();
                    dataGridView2.Rows[rowIndex].Cells["Sehir"].Value = sehir;
                    dataGridView2.Rows[rowIndex].Cells["Ad"].Value = uye.Ad;
                    dataGridView2.Rows[rowIndex].Cells["SoyAd"].Value = uye.SoyAd;
                    dataGridView2.Rows[rowIndex].Cells["TC"].Value = uye.TC;
                    dataGridView2.Rows[rowIndex].Cells["Yaş"].Value = uye.Yas;
                    dataGridView2.Rows[rowIndex].Cells["Kan Gurubu"].Value = uye.KanGrubu;
                    dataGridView2.Rows[rowIndex].Cells["Aidat"].Value = uye.Aidat;
                    dataGridView2.Rows[rowIndex].Cells["Durum"].Value = uye.Durum;
                    dataGridView2.Rows[rowIndex].Cells["Yetki"].Value = uye.Yetki;
                    dataGridView2.Rows[rowIndex].Cells["AidatOdendi"].Value = uye.AidatOdendi;
                    dataGridView2.Rows[rowIndex].Cells["E-Posta"].Value = uye.E_Posta;

                    // Diğer hücreleri de ekleyin...
                }
            }
        }

        private void Btn_DurumListele_Click(object sender, EventArgs e)
        {
            // UyeleriDurumaGoreListele methodunu çağırarak durumlarına göre üyeleri al
            SortedDictionary<bool, List<EntityUye>> durumluUyeler = DALUye.UyeleriDurumaGoreListele();

            // DataGridView temizleme
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear(); // Sütunları temizle

            // DataGridView'a sütunları ekle
            // DataGridView'a sütunları ekle
            DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.HeaderText = "Durum";
            checkboxColumn.Name = "Durum";
            dataGridView2.Columns.Add(checkboxColumn);



            //dataGridView2.Columns.Add("Durum", "Durum");
            dataGridView2.Columns.Add("Ad", "Ad");
            dataGridView2.Columns.Add("SoyAd", "Soyad");
            dataGridView2.Columns.Add("TC", "TC");
            dataGridView2.Columns.Add("Şehir", "Şehir");
            dataGridView2.Columns.Add("Yaş", "Yaş");
            dataGridView2.Columns.Add("Kan Gurubu", "Kan Gurubu");
            dataGridView2.Columns.Add("Aidat", "Aidat");
            dataGridView2.Columns.Add("Yetki", "Yetki");

            DataGridViewCheckBoxColumn checkboxColumn1 = new DataGridViewCheckBoxColumn();
            checkboxColumn1.HeaderText = "AidatOdendi";
            checkboxColumn1.Name = "AidatOdendi";
            dataGridView2.Columns.Add(checkboxColumn1);

            //dataGridView2.Columns.Add("AidatOdendi", "AidatOdendi");
            dataGridView2.Columns.Add("E-Posta", "E-Posta");
            // Diğer sütunları ekleyin...

            // Veriyi DataGridView'a ekle
            foreach (var durum in durumluUyeler.Keys)
            {
                foreach (var uye in durumluUyeler[durum])
                {
                    int rowIndex = dataGridView2.Rows.Add();
                    dataGridView2.Rows[rowIndex].Cells["Durum"].Value = durum;
                    dataGridView2.Rows[rowIndex].Cells["Ad"].Value = uye.Ad;
                    dataGridView2.Rows[rowIndex].Cells["SoyAd"].Value = uye.SoyAd;
                    dataGridView2.Rows[rowIndex].Cells["TC"].Value = uye.TC;
                    dataGridView2.Rows[rowIndex].Cells["Şehir"].Value = uye.Sehir;
                    dataGridView2.Rows[rowIndex].Cells["Yaş"].Value = uye.Yas;
                    dataGridView2.Rows[rowIndex].Cells["Kan Gurubu"].Value = uye.KanGrubu;
                    dataGridView2.Rows[rowIndex].Cells["Aidat"].Value = uye.Aidat;      
                    dataGridView2.Rows[rowIndex].Cells["Yetki"].Value = uye.Yetki;
                    dataGridView2.Rows[rowIndex].Cells["AidatOdendi"].Value = uye.AidatOdendi;
                    dataGridView2.Rows[rowIndex].Cells["E-Posta"].Value = uye.E_Posta;

                    // Diğer hücreleri de ekleyin...
                }
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        

        

       

        private void Btn_MesajGonderYonlendirme_Click(object sender, EventArgs e)
        {

           

            // "mesajgönder" adlı TabPage'i bul
            TabPage tabPageMesajGonder = null;

            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Name == "tabPage_mesajGonder")
                {
                    tabPageMesajGonder = tabPage;
                    break;
                }
            }

            // Eğer bulunduysa, seçili tab olarak ayarla
            if (tabPageMesajGonder != null)
            {
                tabControl1.SelectedTab = tabPageMesajGonder;

                textBox_AlıcıEposta.Text = textBox_ePosta.Text;
            }
            else
            {
                MessageBox.Show("mesajgönder TabPage bulunamadı!");
            }
        }




        void MailGonder()
        {

            string fromMail = "ariiyahya3@gmail.com"; // Gönderen e-postası // Dernek e-postası
            string fromPassword = "spjh yvev duyo fjew";

            MailMessage eposta = new MailMessage();

            eposta.From = new MailAddress(fromMail);
            eposta.To.Add(textBox_AlıcıEposta.Text.ToString());
            eposta.Subject = textBox_konu.Text.ToString();
            eposta.Body = textBox_mesaj.Text.ToString();


            SmtpClient smtp = new SmtpClient();

            smtp.Credentials = new NetworkCredential(fromMail,fromPassword);
            smtp.Host = "smtp.gmail.com"; // microsoft host adresi : smtp.live.com

            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Send(eposta);
            MessageBox.Show("Mail gonderildi");


        }

        private void button2_Click(object sender, EventArgs e)
        {
            MailGonder();
        }

        private void Btn_TopluEpostaGonder_Click(object sender, EventArgs e)
        {
            List<string> epostalar = DALUye.GetEpostalar();

            if (epostalar.Count == 0)
            {
                MessageBox.Show("Hiç e-posta bulunamadı.");
                return;
            }

            string fromMail = "ariiyahya3@gmail.com";
            string fromPassword = "spjh yvev duyo fjew";

            foreach (string epostaAdresi in epostalar)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient smtp = new SmtpClient();

                    mail.From = new MailAddress(fromMail);
                    mail.To.Add(epostaAdresi);

                    if (textBox_konu.Text == "")
                    {
                        mail.Subject = "Konu";
                    }
                    else
                    {
                        mail.Subject = textBox_konu.Text;
                    }

                    

                    if (textBox_mesaj.Text == "")
                    {
                        mail.Body = "Bu bir otomatik mesajdır";
                    }
                    else
                    {
                        mail.Body = textBox_mesaj.Text;
                    }
                    

                    smtp.Credentials = new NetworkCredential(fromMail, fromPassword);
                    smtp.Host = "smtp.gmail.com"; // veya başka bir SMTP sunucu
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                    MessageBox.Show($"{epostaAdresi} adresine mail gönderildi");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{epostaAdresi} adresine mail gönderilirken hata oluştu: {ex.Message}");
                }
            }
        }

        private void button_aidatHatırlatma_Click(object sender, EventArgs e)
        {
            List<string> epostalar = DALUye.GetOdenmemisAidatEpostaları();

            string fromMail = "ariiyahya3@gmail.com"; // Kendi e-posta adresinizi buraya yazın
            string fromPassword = "spjh yvev duyo fjew"; // Kendi e-posta şifrenizi buraya yazın

            foreach (string eposta in epostalar)
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                try
                {
                    mail.From = new MailAddress(fromMail);
                    mail.To.Add(eposta);
                    mail.Subject = "Aidat Hatırlatma";
                    mail.Body = "Merhaba, aidat ödemeniz gerekmektedir.";

                    smtp.Credentials = new NetworkCredential(fromMail, fromPassword);
                    smtp.Host = "smtp.gmail.com"; // Eğer başka bir e-posta sağlayıcısı kullanıyorsanız, onun SMTP bilgilerini ekleyin
                    smtp.Port = 587;
                    smtp.EnableSsl = true;

                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    // Hata durumunda gerekli işlemleri yapabilirsiniz.
                    MessageBox.Show($"E-posta gönderiminde hata oluştu: {ex.Message}");
                }
                finally
                {
                    // SmtpClient ve MailMessage nesnelerini temizleme
                    mail.Dispose();
                    smtp.Dispose();
                }
            }

            MessageBox.Show("E-postalar gönderildi.");
        }

        private void Btn_AidatListele_Click(object sender, EventArgs e)
        {
            AidatListele();
        }

        private void dataGridView_Aidat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox_Ocak.Text = dataGridView_Aidat.CurrentRow.Cells[0].Value.ToString();
            textBox_Subat.Text = dataGridView_Aidat.CurrentRow.Cells[1].Value.ToString();
            textBox_Mart.Text = dataGridView_Aidat.CurrentRow.Cells[2].Value.ToString();
            textBoxNisan.Text = dataGridView_Aidat.CurrentRow.Cells[3].Value.ToString();
            textBoxMayıs.Text = dataGridView_Aidat.CurrentRow.Cells[4].Value.ToString();
            textBox_Haziran.Text = dataGridView_Aidat.CurrentRow.Cells[5].Value.ToString();
            textBox_Temmuz.Text = dataGridView_Aidat.CurrentRow.Cells[6].Value.ToString();
            textBox_Agustos.Text = dataGridView_Aidat.CurrentRow.Cells[7].Value.ToString();
            textBox_Eylul.Text = dataGridView_Aidat.CurrentRow.Cells[8].Value.ToString();
            textBox_Ekim.Text = dataGridView_Aidat.CurrentRow.Cells[9].Value.ToString();
            textBox_Kasim.Text = dataGridView_Aidat.CurrentRow.Cells[10].Value.ToString();
            textBox_Aralik.Text = dataGridView_Aidat.CurrentRow.Cells[11].Value.ToString();
        }

        private void Btn_AidatGuncelle_Click(object sender, EventArgs e)
        {
            
            if (textBox_Aralik.Text == "")
            {
                MessageBox.Show("Lütfen Tüm Alanları Giriniz!", "Hata");
            }
            else
            {
                EntityAidatlar aidatlar = new EntityAidatlar();

                aidatlar.Ocak = textBox_Ocak.Text;
                aidatlar.Subat = textBox_Subat.Text;
                aidatlar.Mart = textBox_Mart.Text;
                aidatlar.Nisan = textBoxNisan.Text;
                aidatlar.Mayis = textBoxMayıs.Text ;
                aidatlar.Haziran = textBox_Haziran.Text;
                aidatlar.Temmuz = textBox_Temmuz.Text;
                aidatlar.Agustos = textBox_Agustos.Text;
                aidatlar.Eylul = textBox_Eylul.Text;
                aidatlar.Ekim = textBox_Ekim.Text;
                aidatlar.Kasim = textBox_Kasim.Text;
                aidatlar.Aralik = textBox_Aralik.Text;


                DateTime tarih = DateTime.Now;
                int yil = tarih.Year;

               


                DALUye.UpdateOrInsertCurrentYear(aidatlar,yil);



                BLUye.aidatGuncelle(aidatlar);


                AidatBelirleme();

                MessageBox.Show("aidatlar Güncellendi!");

                bool aidatGuncellemeDevamEdiyor = false;

                if (!aidatGuncellemeDevamEdiyor)
                {
                    aidatGuncellemeDevamEdiyor = true;

                    List<EntityUye> uyeler = BLUye.uyeListele();

                    foreach (EntityUye uye in uyeler)
                    {
                        uye.Aidat = textBox_Aidat.Text;

                        // Güncelleme işlemi
                        BLUye.uyeGuncelle(uye);
                    }

                    MessageBox.Show("Tüm üyelerin aidatları güncellendi!");

                    Listele();

                    aidatGuncellemeDevamEdiyor = false;
                }
                else
                {
                    MessageBox.Show("İşlem devam ediyor. Lütfen bekleyin.");
                }

            }
            AidatListele();

            
        }

        private void Btn_BorcPDF_Click(object sender, EventArgs e)
        {
            if (dataGridView2.Rows.Count > 0)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF (*.pdf) | *.pdf";
                save.FileName = "Result.pdf";
                bool errorMessage = false;
                if (save.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(save.FileName))
                    {
                        try
                        {
                            File.Delete(save.FileName);

                        }
                        catch (Exception ex)
                        {

                            errorMessage = true;
                            MessageBox.Show("Data yazılamadı" + ex.Message);
                        }
                    }
                    if (!errorMessage)
                    {
                        try
                        {
                            PdfPTable pTable = new PdfPTable(dataGridView2.Columns.Count);
                            pTable.DefaultCell.Padding = 2;
                            pTable.WidthPercentage = 100;
                            pTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn col in dataGridView2.Columns)
                            {
                                PdfPCell pCell = new PdfPCell(new Phrase(col.HeaderText));
                                pTable.AddCell(pCell);
                            }
                            foreach (DataGridViewRow viewRow in dataGridView2.Rows)
                            {
                                foreach (DataGridViewCell dcell in viewRow.Cells)
                                {
                                    pTable.AddCell(dcell.Value.ToString());

                                }
                            }

                            using (FileStream fileStream = new FileStream(save.FileName,FileMode.Create))
                            {
                                Document document = new Document(PageSize.A4,8f,16f,16f,8f);
                                PdfWriter.GetInstance(document, fileStream);
                                document.Open();
                                document.Add(pTable);
                                document.Close();
                                fileStream.Close();


                            }
                            MessageBox.Show("Datalar aktarıldı","info");

                        }
                        catch (Exception ex)
                        {

                            MessageBox.Show("datalar aktarılırken hata oluştu " + ex.Message);
                        }
                    }
                }


            }
            else
            {
                MessageBox.Show("Kayıt Bulunamdı", "Info");
            }
        }

        private void Btn_BorcluListele_Click(object sender, EventArgs e)
        {
            // BorcuOlanlariListele fonksiyonunu çağırarak liste al
            List<EntityUye> borcuOlanlarListesi = DALUye.BorcuOlanlariListele();

            // DataGridView'i temizle
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear(); // Sütunları temizle

            // Sütunları ekle
            dataGridView2.Columns.Add("Ad", "Ad");
            dataGridView2.Columns.Add("SoyAd", "Soyad");
            dataGridView2.Columns.Add("TC", "TC");
            dataGridView2.Columns.Add("Sehir", "Şehir");
            dataGridView2.Columns.Add("Yas", "Yaş");
            dataGridView2.Columns.Add("KanGrubu", "Kan Grubu");
            dataGridView2.Columns.Add("Aidat", "Aidat");
            dataGridView2.Columns.Add("Yetki", "Yetki");
            dataGridView2.Columns.Add("E_Posta", "E-Posta");
            


            
            // Her bir üye için DataGridView'e yeni bir satır ekle
            foreach (EntityUye uye in borcuOlanlarListesi)
            {
                // DataGridView'e yeni satırı ekle
                dataGridView2.Rows.Add(
                    uye.Ad,
                    uye.SoyAd,
                    uye.TC,
                    uye.Sehir,
                    uye.Yas,
                    uye.KanGrubu,
                    uye.Aidat,
                    uye.Yetki,
                    uye.E_Posta
                    
                );
            }

        }

        private void Btn_BorclularaEpostaGonder_Click(object sender, EventArgs e)
        {
            button_aidatHatırlatma_Click(sender,e);
        }

        private void textBox_TC_TextChanged(object sender, EventArgs e)
        {
            textBox_kullaniciAdi.Text = textBox_TC.Text;
            textBox_sifre.Text = textBox_TC.Text;
        }


        private void textBox_Yetki_TextChanged(object sender, EventArgs e)
        {
            textBox_yetkii.Text = textBox_Yetki.Text;
        }

        private void textBox_yetkii_TextChanged(object sender, EventArgs e)
        {

        }

        private void Btn_GirisPaneleDon_Click(object sender, EventArgs e)
        {
            GirisFormu form = new GirisFormu();
            form.Show();
            this.Close();
        }


        private void ConfigureZedGraph_Sehirler(ZedGraphControl zgc)
        {
            GraphPane myPane = zgc.GraphPane;
            myPane.Title.Text = "Üye Dağılımı (Şehirlere Göre)";
            myPane.XAxis.Title.Text = "Şehir";
            myPane.YAxis.Title.Text = "Üye Sayısı";

            // Diğer konfigürasyon ayarlarını yapabilirsiniz.
        }

        private void DrawBarChart_Sehirler(ZedGraphControl zgc, DataTable dt)
        {
            GraphPane myPane = zgc.GraphPane;

            // Bar tipinde bir grafik
            BarItem myBar = myPane.AddBar("Üye Sayısı", null, dt.AsEnumerable().Select(row => Convert.ToDouble(row["UyeSayisi"])).ToArray(), System.Drawing.Color.Blue);

            // X ekseni değerlerini şehir isimleri ile ayarla
            myPane.XAxis.Scale.TextLabels = dt.AsEnumerable().Select(row => row["Şehir"].ToString()).ToArray();
            myPane.XAxis.Type = AxisType.Text;

            // Her bir barın üzerine üye sayısını yazdıran etiketler
            TextObj uyeSayisiEtiket;
            for (int i = 0; i < myBar.Points.Count; i++)
            {
                double uyeSayisi = Convert.ToDouble(dt.Rows[i]["UyeSayisi"]);
                uyeSayisiEtiket = new TextObj($"{uyeSayisi} Kişi", myBar.Points[i].X, uyeSayisi);
                uyeSayisiEtiket.Location.AlignH = AlignH.Center;
                uyeSayisiEtiket.Location.AlignV = AlignV.Bottom;
                uyeSayisiEtiket.FontSpec.Border.IsVisible = false;
                uyeSayisiEtiket.FontSpec.Fill.IsVisible = false;
                uyeSayisiEtiket.FontSpec.IsBold = true;
                myPane.GraphObjList.Add(uyeSayisiEtiket);
            }

            // Grafiği güncelle
            zgc.AxisChange();
            zgc.Invalidate();

            zedGraph_Sehirler.IsEnableZoom = false;
            zedGraph_Sehirler.IsEnableWheelZoom = false;
            zedGraph_Sehirler.IsEnableVZoom = false;
            zedGraph_Sehirler.IsEnableHZoom = false;
        }

        private void ZedGraphYıllıkGeliriGoster()
        {
            // Veritabanından aidat gelirlerini al
            List<EntityAidatGelirleri> aidatlar = DALUye.aidatGelirleriniGetir();

            // Yıllık toplam gelirleri çıkart
            Dictionary<int, double> yillikGelirler = new Dictionary<int, double>();
            foreach (EntityAidatGelirleri aidat in aidatlar)
            {
                int yil = aidat.Tarih;
                double toplamGelir = (double)aidat.Toplam;

                if (yillikGelirler.ContainsKey(yil))
                {
                    yillikGelirler[yil] += toplamGelir;
                }
                else
                {
                    yillikGelirler[yil] = toplamGelir;
                }
            }

            // ZedGraph kontrolünü ayarla
            GraphPane graphPane = zedGraph_Yıllık.GraphPane;
            graphPane.Title.Text = "Yıllık Aidat Geliri Grafiği";
            graphPane.XAxis.Title.Text = "Yıl";
            graphPane.YAxis.Title.Text = "Toplam Gelir";

            // Yıllık X ekseni için tarih etiketlerini ayarla
            string[] yillar = yillikGelirler.Keys.Select(y => y.ToString()).ToArray();
            graphPane.XAxis.Scale.TextLabels = yillar;
            graphPane.XAxis.Type = AxisType.Text;

            // Yıllık toplam gelirleri çiz
            BarItem yillikGelir = graphPane.AddBar("Yıllık Toplam Gelir", null, yillikGelirler.Values.ToArray(), System.Drawing.Color.Green);

            // Yıllık toplam gelir etiketleri
            TextObj yilToplamEtiket;
            for (int i = 0; i < yillikGelirler.Count; i++)
            {
                yilToplamEtiket = new TextObj(yillikGelirler.ElementAt(i).Value.ToString(), i + 1, yillikGelirler.ElementAt(i).Value);
                yilToplamEtiket.Location.AlignH = AlignH.Center;
                yilToplamEtiket.Location.AlignV = AlignV.Bottom;
                yilToplamEtiket.FontSpec.Border.IsVisible = false;
                yilToplamEtiket.FontSpec.Fill.IsVisible = false;
                yilToplamEtiket.FontSpec.IsBold = true;

                yilToplamEtiket.Text += " ₺";


                graphPane.GraphObjList.Add(yilToplamEtiket);
            }

            // Grafik güncellensin
            zedGraph_Yıllık.AxisChange();
            zedGraph_Yıllık.Invalidate();

            zedGraph_Yıllık.IsEnableZoom = false;
            zedGraph_Yıllık.IsEnableWheelZoom = false;
            zedGraph_Yıllık.IsEnableVZoom = false;
            zedGraph_Yıllık.IsEnableHZoom = false;
        }



        public void AidatGelirleriniGrafikleGetir(int yil)
        {
            TemizleZedGraphAidat();
            // Veritabanından aidat gelirlerini al
            List<EntityAidatGelirleri> aidatlar = DALUye.aidatGelirleriniGetir();

            // Gelirleri aylara ve tarihlere göre gruplandır
            Dictionary<string, double> gelirGruplari = new Dictionary<string, double>();

            foreach (EntityAidatGelirleri aidat in aidatlar)
            {
                if (aidat.Tarih == yil)
                {
                    foreach (var property in typeof(EntityAidatGelirleri).GetProperties())
                    {
                        if (property.Name != "Tarih" && property.Name != "Toplam")
                        {
                            string ayAdi = property.Name;
                            int ayinGeliri = (int)property.GetValue(aidat);

                            // Ay adını ve gelirini ekleyelim veya güncelleyelim
                            string key = $"{yil}-{ayAdi}";
                            if (gelirGruplari.ContainsKey(key))
                            {
                                gelirGruplari[key] += ayinGeliri;
                            }
                            else
                            {
                                gelirGruplari[key] = ayinGeliri;
                            }
                        }
                    }
                }
            }

            // ZedGraph kontrolünü ayarla
            GraphPane graphPane = zedGraph_Aidat.GraphPane;
            graphPane.Title.Text = $"{yil} Yılı Aylık Aidat Geliri Grafiği";
            graphPane.XAxis.Title.Text = "Tarih";
            graphPane.YAxis.Title.Text = "Toplam Gelir";

            // Aylık X ekseni için etiketleri ayarla
            string[] tarihAylar = gelirGruplari.Keys.ToArray();
            graphPane.XAxis.Scale.TextLabels = tarihAylar;
            graphPane.XAxis.Type = AxisType.Text;

            // Her bir ayın gelirini çiz
            BarItem aylarGelir = graphPane.AddBar("Aylık Toplam Gelir", null, gelirGruplari.Values.ToArray(), System.Drawing.Color.Green);

            // Ay toplam gelir etiketleri
            TextObj ayToplamEtiket;
            for (int i = 0; i < aylarGelir.Points.Count; i++)
            {
                // Key'den yıl ve ay adını ayır
                string[] yilAy = tarihAylar[i].Split('-');
               

                // Etiket oluştur
                ayToplamEtiket = new TextObj(gelirGruplari.ElementAt(i).Value.ToString(), i + 1, aylarGelir.Points[i].Y);
                ayToplamEtiket.Location.AlignH = AlignH.Center;
                ayToplamEtiket.Location.AlignV = AlignV.Bottom;
                ayToplamEtiket.FontSpec.Border.IsVisible = false;
                ayToplamEtiket.FontSpec.Fill.IsVisible = false;
                ayToplamEtiket.FontSpec.IsBold = true;

                // Gelirin yanına "₺" ekleyin
                ayToplamEtiket.Text += " ₺";

                // Tarihi etikete ekle
                //ayToplamEtiket.Text += $" ({ayAdi} {nyil})";

                // Etiketi grafiğe ekle
                graphPane.GraphObjList.Add(ayToplamEtiket);
            }

            // Grafik güncellensin
            zedGraph_Aidat.AxisChange();
            zedGraph_Aidat.Invalidate();

            zedGraph_Aidat.IsEnableZoom = false;
            zedGraph_Aidat.IsEnableWheelZoom = false;
            zedGraph_Aidat.IsEnableVZoom = false;
            zedGraph_Aidat.IsEnableHZoom = false;
        }

        private void TemizleZedGraphAidat()
        {
            // ZedGraph kontrolünün GraphPane özelliğini al
            GraphPane graphPane = zedGraph_Aidat.GraphPane;

            // Grafik eğrilerini temizle
            graphPane.CurveList.Clear();

            // Grafik nesnelerini temizle
            graphPane.GraphObjList.Clear();

            // ZedGraph kontrolünü güncelle
            zedGraph_Aidat.AxisChange();
            zedGraph_Aidat.Invalidate();
        }

        void GelirHesapla()
        {
            DALUye.aidatGelirleriniGetir();
            DALUye.AidatGelirleriniToplaVeKaydet();
        }

        private void comboBox_Tarihler_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox'dan seçilen yılı al
            if (comboBox_Tarihler.SelectedItem != null)
            {
                int secilenYil = Convert.ToInt32(comboBox_Tarihler.SelectedItem);

                // Burada secilenYil'i kullanabilirsiniz
                // Örneğin, bir değişkenin içine atayabilirsiniz:
                int yil = secilenYil;

                AidatGelirleriniGrafikleGetir(yil);
            }
        }

        private void Btn_YapilanOdemeler_Click(object sender, EventArgs e)
        {
            // DataGridView'i temizle
            dataGridView2.DataSource = null;
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear(); // Sütunları temizle


            DataTable dataTable = DALUye.GetYapilanOdemeler();
            dataGridView2.DataSource = dataTable;
        }

        //Temizle butonu.
        private void button3_Click(object sender, EventArgs e)
        {
            textBox_AlıcıEposta.Text = "";
            textBox_konu.Text = "";
            textBox_mesaj.Text = "";
        }

        private void textBox_Kasim_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Btn_GirisPaneleDon_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Btn_GirisPaneleDon_Click(sender, e);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Btn_GirisPaneleDon_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Btn_GirisPaneleDon_Click(sender, e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tc = textBox1.Text;

            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear(); // Sütunları temizle

            dataGridView1.DataSource = DALUye.uyeGetir(tc);

            if (textBox1.Text == "")
            {
                dataGridView1.DataSource = BLUye.uyeListele();
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = ""; // TextBox içindeki metni sil
            textBox1.ForeColor = SystemColors.WindowText; // Varsayılan metin rengini ayarla
        }
    }
}
