using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Smertiorlife
{
    public partial class LIFE : Form
    {
        private int schetchikgeyzira = 0;
        private Graphics Graphics;
        private int resolution;
        private int rows;
        private int cols;
        private int[,] field;   //Принимает значения от 0 до 2
        int b = 0;

        public LIFE()
        {
            InitializeComponent();
        }
        private int Red(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int e = -1; e < 2; e++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + e + rows) % rows;
                    var isCheking = ((col == x) && (row == y));
                    var lifeColor = field[col, row];
                    if (lifeColor == 1 && !isCheking)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private int Blue(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int e = -1; e < 2; e++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + e + rows) % rows;
                    var isCheking = ((col == x) && (row == y));
                    var lifeColor = field[col, row];
                    if (lifeColor == 2 && !isCheking)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics.Clear(Color.Black);
            bool newGeneration = false;
            var newfield = new int[cols, rows]; // новое поле для положения клеток
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var Nightboarth = Red(x, y);
                    var lifeColor = field[x, y];
                    if (lifeColor == 1 && (Nightboarth == 2 || Nightboarth == 3))
                    {
                        newfield[x, y] = 1;
                        Graphics.FillRectangle(Brushes.Red, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        continue;
                    }
                    else if (lifeColor == 1 && (Nightboarth > 2 || Nightboarth < 3))
                    {
                        newfield[x, y] = 0;
                        Graphics.FillRectangle(Brushes.Red, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        continue;
                    }
                    if (lifeColor != 1 && Nightboarth == 3)
                    {
                        newfield[x, y] = 1;
                        Graphics.FillRectangle(Brushes.Red, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        newGeneration = true;
                        continue;
                    }
                }
            }
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    var Nightboarth = Blue(x, y);
                    var lifeColor = field[x, y];
                    if (lifeColor == 2 && (Nightboarth == 2 || Nightboarth == 3))
                    {
                        newfield[x, y] = 2;
                        Graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        continue;
                    }
                    else if (lifeColor == 2 && (Nightboarth > 2 || Nightboarth < 3))
                    {
                        newfield[x, y] = 0;
                        Graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        continue;
                    }
                    if (lifeColor != 2 && Nightboarth == 3)
                    {
                        newfield[x, y] = 2;
                        Graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution - 1, resolution - 1);
                        newGeneration = true;
                        continue;
                    }
                }
            }
            if (newGeneration)
            {
                schetchikgeyzira++;
            }
            Text = $"Schetchik {schetchikgeyzira}";
            field = newfield;
            pictureBox1.Refresh();
        }
        private void Start_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return;
            schetchikgeyzira = 0;
            Text = $"Schetchik {schetchikgeyzira}";
            nudResolution.Enabled = false;
            nudDensity.Enabled = false;
            resolution = (int)nudResolution.Value;
            rows = pictureBox1.Height / resolution;
            cols = pictureBox1.Width / resolution;
            field = new int[cols, rows];
            Random random = new Random();
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    field[i, j] = random.Next(2);
                }
            }
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
                return;
            timer1.Stop();
            nudResolution.Enabled = true;
            nudDensity.Enabled = true;
        }

        private void pictureBox1_Mouse(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
            if (e.Button == MouseButtons.Left)
            {
                Random random = new Random();
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var ValidatePosition = MousePosition(x, y);
                if (ValidatePosition)
                    field[x, y] = b;
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                var ValidatePosition = MousePosition(x, y);
                if (ValidatePosition)
                    field[x, y] = 0;
            }
        }
        private bool MousePosition(int x, int y)
        {
            return x >= 0 && y >= 0 && x < cols && y < rows;
        }

        private void bred_Click(object sender, EventArgs e)
        {
            b = 1;
        }

        private void bblue_Click(object sender, EventArgs e)
        {
            b = 2;
        }

        private void GameLife_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

