using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka

{
    public interface IComponent
    {
        public void HandleKey(ConsoleKeyInfo info);

        public void Draw();
    }
}
