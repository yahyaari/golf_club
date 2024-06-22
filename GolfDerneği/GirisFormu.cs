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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GolfDerneği
{
    public partial class GirisFormu : Form
    {
        public GirisFormu()
        {
            InitializeComponent();
        }

        private void GirisFormu_Load(object sender, EventArgs e)
        {
            label3.Text = " # Üye kayıt işleminiz yönetici tarafından yapıldı ise : Kullanıcı Adınız -> TC , Şifreniz -> TC ";
            timer1.Interval = 300; // Yazının hızını belirleyebilirsiniz
            timer1.Tick += timer1_Tick;
            timer1.Start();
        }

        public static string gkullaniciAdi { get; set; }

        private void Btn_GirisYap_Click(object sender, EventArgs e)
        {
            
            
            
            string kullaniciAdi = textBox_KullaniciAdi.Text;
            string sifre = textBox_Sifre.Text;



            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Kullanıcı adı ve şifre boş bırakılamaz.");
                return;
            }

            try
            {
                EntityUserData user = new EntityUserData();

                user.KullaniciAdi = kullaniciAdi;
                user.Sifre = sifre;


                DALUye.kullaniciTanıma(user);
               

                string yetki = DALUye.yetkiCekKulAd(kullaniciAdi);

               
                if (yetki == "Admin")
                {
                    GirisMessageBoxForm mboxForm = new GirisMessageBoxForm();
                    mboxForm.ShowDialog();

                    if (mboxForm.AdminGirisi == true)
                    {
                        gkullaniciAdi = kullaniciAdi;
                        MessageBox.Show("Giriş Başarılı!");
                        MessageBox.Show("Admin yetkisine sahipsiniz.");
                        AdminPaneliFormu form1 = new AdminPaneliFormu();
                        form1.Show();
                        this.Hide();
                    }
                    else // Üye Girişi
                    {
                        gkullaniciAdi = kullaniciAdi;
                        MessageBox.Show("Giriş Başarılı!");
                        MessageBox.Show("Üye kullanıcı yetkisine sahipsiniz.");
                        UyePaneliFormu form = new UyePaneliFormu();
                        form.Show();
                        this.Hide();
                    }
                    
                }
                else if (yetki == "Üye")
                {
                    gkullaniciAdi = kullaniciAdi;
                    MessageBox.Show("Giriş Başarılı!");
                    MessageBox.Show("Üye kullanıcı yetkisine sahipsiniz.");
                    UyePaneliFormu form = new UyePaneliFormu();
                    form.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Giriş Başarısız!");
                    MessageBox.Show("Yetki bilgisi bulunamadı veya tanımsız.");
                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = label3.Text.Substring(1) + label3.Text[0];
        }
    }
}


