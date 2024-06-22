namespace GolfDerneği
{
    partial class GirisMessageBoxForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GirisMessageBoxForm));
            this.label1 = new System.Windows.Forms.Label();
            this.Btn_YoneticiGiris = new System.Windows.Forms.Button();
            this.Btn_ÜyeGirisi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(23, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(502, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Admin veya Üye olarak giriş yapabilirsiniz.";
            // 
            // Btn_YoneticiGiris
            // 
            this.Btn_YoneticiGiris.BackColor = System.Drawing.Color.LightGray;
            this.Btn_YoneticiGiris.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_YoneticiGiris.Location = new System.Drawing.Point(58, 81);
            this.Btn_YoneticiGiris.Name = "Btn_YoneticiGiris";
            this.Btn_YoneticiGiris.Size = new System.Drawing.Size(147, 42);
            this.Btn_YoneticiGiris.TabIndex = 1;
            this.Btn_YoneticiGiris.Text = "Yönetici Girişi";
            this.Btn_YoneticiGiris.UseVisualStyleBackColor = false;
            this.Btn_YoneticiGiris.Click += new System.EventHandler(this.Btn_YoneticiGiris_Click);
            // 
            // Btn_ÜyeGirisi
            // 
            this.Btn_ÜyeGirisi.BackColor = System.Drawing.Color.LightGray;
            this.Btn_ÜyeGirisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Btn_ÜyeGirisi.Location = new System.Drawing.Point(252, 81);
            this.Btn_ÜyeGirisi.Name = "Btn_ÜyeGirisi";
            this.Btn_ÜyeGirisi.Size = new System.Drawing.Size(147, 42);
            this.Btn_ÜyeGirisi.TabIndex = 2;
            this.Btn_ÜyeGirisi.Text = "Üye Girişi";
            this.Btn_ÜyeGirisi.UseVisualStyleBackColor = false;
            this.Btn_ÜyeGirisi.Click += new System.EventHandler(this.Btn_ÜyeGirisi_Click);
            // 
            // GirisMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(457, 149);
            this.Controls.Add(this.Btn_ÜyeGirisi);
            this.Controls.Add(this.Btn_YoneticiGiris);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GirisMessageBoxForm";
            this.Text = "- Giriş Mesaj -";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_YoneticiGiris;
        private System.Windows.Forms.Button Btn_ÜyeGirisi;
    }
}