using Midnight_Commander_Psotka.Components;
using Midnight_Commander_Psotka.Windows;
using System;
using System.Collections.Generic;

namespace Midnight_Commander_Psotka
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            string root = @"C:\";
            Application.Window = new ListWindow(root);
            Console.CursorVisible = false;
            Console.WindowWidth = 150;
            Console.WindowHeight = 27;
            while (true)
            {
                int Height = Console.WindowHeight;
                int Width = Console.WindowWidth;
                Application.Draw();
                ConsoleKeyInfo info = Console.ReadKey();
                Application.HandleKey(info);
                if (Console.WindowHeight != Height || Console.WindowWidth != Width)
                {
                    PadMaker.Resize();
                }
            }
        }
    }
}
