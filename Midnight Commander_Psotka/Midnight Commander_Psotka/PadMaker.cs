using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka
{
    public class PadMaker
    {
        public static int PadForResize { get; set; } = 47;
        public static void Resize()
        {
            PadForResize = Console.WindowWidth / 2 - 28;
        }
    }
}
