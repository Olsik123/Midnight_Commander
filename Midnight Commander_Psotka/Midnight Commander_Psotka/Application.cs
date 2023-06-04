using Midnight_Commander_Psotka.PopUps;
using Midnight_Commander_Psotka.Windows;
using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka
{
    class Application
    {
        public Application()
        {
        }

        public static Window Window { get; set; }
        public static PopUp PopUpWindow { get; set; } 
        public static Window ListWindow { get; set; }

        public static void HandleKey(ConsoleKeyInfo info)
        {

            if (Application.PopUpWindow != null)
            {
                Application.PopUpWindow.HandleKey(info);
            }
            else
            {
                Application.Window.HandleKey(info);
            }
        }      

        public static void Draw()
        {

            //if (Application.Window != null)
            //{
            //    Application.Window.Draw();
            //}
            if (Application.PopUpWindow != null)
            {
                Application.PopUpWindow.Draw();
            }
            else if (Application.Window != null)
            {
                Application.Window.Draw();
            }
        }
    }
}
