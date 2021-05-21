using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad
{
    public partial class Form1 : Form
    {
        string formBaslik = "Bizim Notepad-";
        string dosyaAdi = null;
        string dosyaYolu;
        bool dosyaKayıtlıMi = false;
        public Form1()
        {
            InitializeComponent();
        }
        //Fonksiyonlar
        //Yeni() dosyaAc() dosyaKaydet() Kes() Kopyala() Yapistir() Bicim()
        void dosyaKaydet()
        {
            if (dosyaAdi == null)
            {
                DialogResult dr;
                saveFileDialog1.Filter = "Zengin Metin(*.rtf)| *.rtf";
                saveFileDialog1.FileName = "";
                if (dosyaKayıtlıMi == false)//dosya önceden kaydedilmemiş
                {
                    dr = saveFileDialog1.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        richTextBox1.SaveFile(saveFileDialog1.FileName);// , RichTextBoxStreamType
                        richTextBox1.Modified = false;
                        dosyaAdi = Path.GetFileName(saveFileDialog1.FileName);//dosya adını aldık
                        dosyaYolu = Path.GetDirectoryName(saveFileDialog1.FileName);
                        this.Text = formBaslik + dosyaAdi;
                        dosyaKayıtlıMi = true;
                    }

                }
                else
                {
                    //dosya daha önceden kayıtlıdır
                    richTextBox1.SaveFile(dosyaYolu +"\\"+ dosyaAdi);
                    this.Text = formBaslik + dosyaAdi;
                    richTextBox1.Modified = false;
                }
            }
            
        }
        void dosyaAc()
        {
            DialogResult drr;
            Yeni();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Zengin Metin(*.rtf)| *.rtf";
            drr = openFileDialog1.ShowDialog();
            if (drr == DialogResult.OK )
            {
                try
                {
                    //dosyayı kaydet
                    dosyaAdi = Path.GetFileName(openFileDialog1.FileName);
                    richTextBox1.LoadFile(openFileDialog1.FileName);
                    dosyaYolu = Path.GetDirectoryName(saveFileDialog1.FileName);
                    dosyaKayıtlıMi = true;
                    richTextBox1.Modified = false;
                    this.Text = formBaslik + dosyaAdi;
                }
                catch (Exception e)
                {

                    MessageBox.Show("Yanlış Format... "+e.Message);
                }
                
            }
        }
        void kontrol()
        {
            DialogResult dr;
            if (richTextBox1.Modified)//değişiklik var mı yok mu kontrol eder
            {
                dr = MessageBox.Show("Kaydetmek istiyor musunuz?", "UYARI!!!", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    dosyaKaydet();
                }
            }
        }
        void Yeni()
        {
            kontrol();
            richTextBox1.Text = "";
            richTextBox1.Visible = true;
            this.Text = formBaslik + "Yeni Dosya";
        }
        void Kes()
        {
            richTextBox1.Cut();
        }

        void Kopyala()
        {
            if (richTextBox1.SelectionLength > 0)
                richTextBox1.Copy();
            else
                MessageBox.Show("seçim yapınız!!!");
        }

        void Yapistir()
        {
            richTextBox1.Paste();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = formBaslik;
            richTextBox1.Visible = false;
            
        }

        void Bicimle()
        {
            //FontDialog penceresi
            fontDialog1.ShowColor = true; // fontDialog penceresine renk özelliği eklendi
            fontDialog1.ShowDialog();
            //formatı seçti ve uygulanacak
            richTextBox1.SelectionFont = fontDialog1.Font;
            //font uygulandığında renk uygulanmaz renk ayrı uygulanır
            richTextBox1.SelectionColor = fontDialog1.Color;


        }

        private void dosyaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yeni();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Yeni();
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            kontrol();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (dosyaAdi == null)
            {
                this.Text = formBaslik + "Yeni Dosya" + "*";
            }
            else
            {
                this.Text = formBaslik + dosyaAdi + "*";
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            dosyaAc();
        }

        private void açToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dosyaAc();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            dosyaKaydet();
        }

        private void kaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dosyaKaydet();
        }

        private void kesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kes();
        }

        private void kopyalaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kopyala();
        }

        private void yapıştırToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Yapistir();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            Kes();
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            Kopyala();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            Yapistir();
        }

        private void biçimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bicimle();
        }
    }
}
