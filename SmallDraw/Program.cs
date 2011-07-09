using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallDraw
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.Run(new ShapeForm());
        }
    }
}
