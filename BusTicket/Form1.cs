using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace busbilet
{
    public partial class Form1 : Form
    {
        List<string> tickets = new List<string>();

        public Form1()
        {
            InitializeComponent();

            string[] cities = { "Bakı", "Gəncə", "Sumqayıt", "Şəki", "Lənkəran", "Naxçıvan", "Quba" };
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(cities);
            comboBox2.Items.AddRange(cities);

            button1.Click -= button1_Click;
            button1.Click += button1_Click;
            button2.Click -= button2_Click;
            button2.Click += button2_Click;
            button3.Click -= button3_Click;
            button3.Click += button3_Click;
            button4.Click -= button4_Click;
            button4.Click += button4_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void RefreshList()
        {
            listBox1.Items.Clear();
            for (int i = 0; i < tickets.Count; i++)
            {
                listBox1.Items.Add((i + 1) + ") " + tickets[i]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            object temp = comboBox1.SelectedItem;
            comboBox1.SelectedItem = comboBox2.SelectedItem;
            comboBox2.SelectedItem = temp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Şəhərləri seçin", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox1.Text == comboBox2.Text)
            {
                MessageBox.Show("Eyni şəhərlərə gediş yoxdur", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime date;
            if (!DateTime.TryParseExact(maskedTextBox1.Text, "dd/MM/yyyy HH:mm",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                MessageBox.Show("Tarixi düzgün daxil edin (gün/ay/il saat:dəqiqə)", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (date < DateTime.Now)
            {
                MessageBox.Show("Keçmiş tarixə bilet almaq olmaz", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("Yer nömrəsini daxil edin", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox2.Text.Trim() == "")
            {
                MessageBox.Show("Ad və soyadı daxil edin", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox3.Text.Trim().Length != 7)
            {
                MessageBox.Show("FİN 7 simvoldan ibarət olmalıdır", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!maskedTextBox2.MaskCompleted)
            {
                MessageBox.Show("Telefon nömrəsini tam daxil edin", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!textBox4.Text.Contains("@") || !textBox4.Text.Contains("."))
            {
                MessageBox.Show("Email düzgün deyil", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string ticket = comboBox1.Text + " " + comboBox2.Text + " (" + maskedTextBox1.Text + ") " + textBox1.Text.Trim();
            if (tickets.Contains(ticket))
            {
                MessageBox.Show("Bu yer artıq satılıb", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tickets.Add(ticket);
            RefreshList();
            MessageBox.Show("Bilet uğurla alındı", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Silmək üçün siyahıdan seçim edin", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            tickets.RemoveAt(listBox1.SelectedIndex);
            RefreshList();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Proqramdan çıxış edilsinmi?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}