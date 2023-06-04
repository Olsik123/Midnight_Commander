using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka.Windows
{
    

        public abstract class Window
        {
            protected List<IComponent> components = new List<IComponent>();
        
            public virtual void HandleKey(ConsoleKeyInfo info)
            {               
               foreach (IComponent item in components)
               {                         
                   item.HandleKey(info);
               }
            }

            public virtual void Draw()
            {
                foreach (IComponent item in components)
                {
                    item.Draw();
                }
            }
        }
    
}
