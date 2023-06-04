using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka.PopUps
{
    public abstract class PopUp
    {
        public abstract void HandleKey(ConsoleKeyInfo info);

        public abstract void Draw();

        public void Close()
        {
            Application.PopUpWindow = null;
        }
    }
}
