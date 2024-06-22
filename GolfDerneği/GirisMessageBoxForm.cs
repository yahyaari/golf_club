using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GolfDerneği
{
    public partial class GirisMessageBoxForm : Form
    {
        public GirisMessageBoxForm()
        {
            InitializeComponent();
        }

        public bool AdminGirisi { get; set; }
        public bool UyeGirisi { get; set; }

        private void Btn_YoneticiGiris_Click(object sender, EventArgs e)
        {
            AdminGirisi = true;
            UyeGirisi = false;
            this.Close();
        }

        private void Btn_ÜyeGirisi_Click(object sender, EventArgs e)
        {
            UyeGirisi = true;
            AdminGirisi = false;
            this.Close();
        }
    }
}
