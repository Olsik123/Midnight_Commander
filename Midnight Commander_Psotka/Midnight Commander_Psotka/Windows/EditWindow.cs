using Midnight_Commander_Psotka.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka.Windows
{
    public class EditWindow : Window
    {
        public Editor Editor { get; set; }
        public string Path { get; set; }
        public EditWindow(string path)
        {
            Path = path;
            Editor = new Editor(path);
            components.Add((IComponent)Editor);
        }
    }
}
