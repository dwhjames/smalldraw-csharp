using System;
using System.Windows.Forms;

namespace SmallDraw
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.Run(new ShapeDraw());
        }
    }
}
