using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Smertiorlife
{
    internal static class Program
    {
        private static int cols;
        private static int rows;
        private static int resolution;
        private static int[,] field;

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LIFE());
        }
        private static int PodschetSosedey(int x, int y)
        {
            int count = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int e = -1; e < 2; e++)
                {
                    var col = (x + i + cols) % cols;
                    var row = (y + e + rows) % rows;
                    var isCheking = col == x && row == y;
                    var life = field[col, row];
                    if (life >0 && !isCheking)
                        count++;

                }
            }
            return count;
        }
    }
}
