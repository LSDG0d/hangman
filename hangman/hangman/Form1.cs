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

namespace hangman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int health = 12;
        int words = 220;
        string word = "";
        char[] now = new char[10];
        Random rnd = new Random();

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 4);
            if (health <= 11) g.DrawLine(pen, 20, 240, 20, 220);
            if (health <= 10) g.DrawLine(pen, 20, 220, 140, 220);
            if (health <= 9) g.DrawLine(pen, 140, 220, 140, 240);
            if (health <= 8) g.DrawLine(pen, 60, 40, 60, 220);
            if (health <= 7) g.DrawLine(pen, 60, 40, 120, 40);
            if (health <= 6) g.DrawLine(pen, 120, 40, 120, 60);
            if (health <= 5) g.DrawEllipse(pen, 100, 60, 40, 40);
            if (health <= 4) g.DrawLine(pen, 120, 100, 120, 160);
            if (health <= 3) g.DrawLine(pen, 120, 100, 140, 140);
            if (health <= 2) g.DrawLine(pen, 120, 100, 100, 140);
            if (health <= 1) g.DrawLine(pen, 120, 158, 130, 210);
            if (health <= 0) g.DrawLine(pen, 119, 158, 110, 210);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Invalidate();
            StreamReader sr = new StreamReader("List.txt");
            int choice = rnd.Next(0, words);
            while (choice >= 0)
            {
                word = sr.ReadLine();
                choice--;
            }
            label1.Text = "";
            label2.Text = "";
            for (int i = 0; i < word.Length; i++) now[i] = '*';
            for (int i = 0; i < word.Length; i++) label1.Text += now[i];
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char p = e.KeyChar;
            bool b = true;
            for (int i = 0; i < word.Length; i++) if (p == word[i] && now[i] == '*')
                {
                    now[i] = p;
                    b = false;
                }
            if (b) health--;
            label1.Text = "";
            label2.Text += p;
            for (int i = 0; i < word.Length; i++) label1.Text += now[i];
            this.Invalidate();
            if (health <= 0)
            {
                MessageBox.Show("Game Over!!! It was " + word.ToString());
                Application.Exit();
            }
            if (label1.Text == word)
            {
                MessageBox.Show("You win!!!");
                Application.Exit();
            }
        }
    }
}
