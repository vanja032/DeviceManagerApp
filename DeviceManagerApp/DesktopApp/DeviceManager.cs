using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Strahinja
{
    public partial class DeviceManager : Form
    {
        string slika = "";
        string podatak_za_izmenu = "";
        public DeviceManager()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(textBox1.Text=="" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    throw new Exception();
                }
                StreamWriter sw = new StreamWriter("laptopovi.txt", true);
                if (slika == "")
                    slika = "nemaslike.png";
                if (checkBox1.Checked)
                {
                    sw.WriteLine("Redni broj: " + textBox1.Text + "; Marka: " + textBox2.Text + "; Naziv: " + textBox3.Text + "; Ekran: " + textBox4.Text + "; Kamera: Da|" + slika);
                }
                else
                {
                    sw.WriteLine("Redni broj: " + textBox1.Text + "; Marka: " + textBox2.Text + "; Naziv: " + textBox3.Text + "; Ekran: " + textBox4.Text + "; Kamera: Ne|" + slika);
                }
                sw.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                checkBox1.Checked = false;
                listBox1.Items.Clear();
                StreamReader sr = new StreamReader("laptopovi.txt");
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    line = line.Substring(0, line.IndexOf('|'));
                    listBox1.Items.Add(line);
                }
                sr.Close();
                slika = "";
            }
            catch
            {
                MessageBox.Show("Morate uneti sve podatke!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    slika = slika = openFileDialog1.FileName;
                    pictureBox1.Image = new Bitmap(slika);
                    MessageBox.Show(slika);
                }
                catch
                {
                    MessageBox.Show("Format slike nije ispravan!");
                    slika = "nemaslike.png";
                    pictureBox1.Image = new Bitmap(slika);
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex==-1)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                checkBox1.Checked = false;
                return;
            }

            string lb_data = Convert.ToString(listBox1.Items[listBox1.SelectedIndex]);
            lb_data = lb_data.Substring(lb_data.IndexOf(':') + 2).Substring(0, lb_data.IndexOf(';'));
            podatak_za_izmenu = Convert.ToString(listBox1.Items[listBox1.SelectedIndex]);

            string data = Convert.ToString(listBox1.Items[listBox1.SelectedIndex]);
            data = data.Substring(data.IndexOf(':') + 2);
            textBox1.Text = data.Substring(0, data.IndexOf(';'));
            data = data.Substring(data.IndexOf(':') + 2);
            textBox2.Text = data.Substring(0, data.IndexOf(';'));
            data = data.Substring(data.IndexOf(':') + 2);
            textBox3.Text = data.Substring(0, data.IndexOf(';'));
            data = data.Substring(data.IndexOf(':') + 2);
            textBox4.Text = data.Substring(0, data.IndexOf(';'));
            data = data.Substring(data.IndexOf(':') + 2);

            if(data == "Da")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }

            StreamReader sr = new StreamReader("laptopovi.txt");
            while(!sr.EndOfStream)
            {
                string file_data = sr.ReadLine();
                string picture = file_data;
                file_data = file_data.Substring(file_data.IndexOf(':') + 2).Substring(0, file_data.IndexOf(';'));
                if (file_data == lb_data)
                {
                    try
                    {
                        pictureBox1.Image = new Bitmap(picture.Substring(picture.IndexOf('|') + 1));
                        slika = picture.Substring(picture.IndexOf('|') + 1);
                    }
                    catch
                    {
                        MessageBox.Show("Format slike nije ispravan!");
                        pictureBox1.Image = new Bitmap("nemaslike.png");
                    }
                }
            }
            sr.Close();



        }

        private void DeviceManager_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("laptopovi.txt");
            while(!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                line = line.Substring(0, line.IndexOf('|'));
                listBox1.Items.Add(line);
            }
            sr.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();
            StreamReader sr = new StreamReader("laptopovi.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                lista.Add(line);
            }
            for(int i=0;i<lista.Count;i++)
            {
                if(lista[i].Substring(0,lista[i].IndexOf('|')) == podatak_za_izmenu)
                {
                    if (checkBox1.Checked)
                    {
                        if (slika == "")
                            slika = "nemaslike.png";
                        lista[i]="Redni broj: " + textBox1.Text + "; Marka: " + textBox2.Text + "; Naziv: " + textBox3.Text + "; Ekran: " + textBox4.Text + "; Kamera: Da|" + slika;
                    }
                    else
                    {
                        if (slika == "")
                            slika = "nemaslike.png";
                        lista[i]="Redni broj: " + textBox1.Text + "; Marka: " + textBox2.Text + "; Naziv: " + textBox3.Text + "; Ekran: " + textBox4.Text + "; Kamera: Ne|" + slika;
                    }
                }
            }
            sr.Close();
            FileStream fs = new FileStream("laptopovi.txt", FileMode.Truncate);
            fs.Close();
            StreamWriter sw = new StreamWriter("laptopovi.txt", true);
            for(int i=0;i<lista.Count;i++)
            {
                sw.WriteLine(lista[i]);
            }
            sw.Close();
            listBox1.Items.Clear();
            StreamReader sr1 = new StreamReader("laptopovi.txt");
            while (!sr1.EndOfStream)
            {
                string line = sr1.ReadLine();
                line = line.Substring(0, line.IndexOf('|'));
                listBox1.Items.Add(line);
            }
            sr1.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            slika = "";
            checkBox1.Checked = false;
            listBox1.SelectedIndex = -1;
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> lista = new List<string>();
            StreamReader sr = new StreamReader("laptopovi.txt");
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                lista.Add(line);
            }
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].Substring(0, lista[i].IndexOf('|')) == podatak_za_izmenu)
                {
                    lista.RemoveAt(i);
                }
            }
            sr.Close();
            FileStream fs = new FileStream("laptopovi.txt", FileMode.Truncate);
            fs.Close();
            StreamWriter sw = new StreamWriter("laptopovi.txt", true);
            for (int i = 0; i < lista.Count; i++)
            {
                sw.WriteLine(lista[i]);
            }
            sw.Close();
            listBox1.Items.Clear();
            StreamReader sr1 = new StreamReader("laptopovi.txt");
            while (!sr1.EndOfStream)
            {
                string line = sr1.ReadLine();
                line = line.Substring(0, line.IndexOf('|'));
                listBox1.Items.Add(line);
            }
            sr1.Close();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            slika = "";
            checkBox1.Checked = false;
            listBox1.SelectedIndex = -1;
            pictureBox1.Image = null;
        }
    }
}
