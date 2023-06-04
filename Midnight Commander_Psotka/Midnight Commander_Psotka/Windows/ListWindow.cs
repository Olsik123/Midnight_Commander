using Midnight_Commander_Psotka.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka.Windows
{

    public class ListWindow : Window
    {
        public ListWindow(string file)
        {
            FileService service = new FileService(file);
            Table table = new Table(file, false, true);
            foreach (Row item in service.GetData())
            {
                table.Add(item.Name, item.Size, item.ModifyTime);
            }
            foreach (Row item in service.GetFiles())
            {
                table.AddFiles(item.Name, item.Size, item.ModifyTime);
            }

            FileService service2 = new FileService(file); ;
            Table table2 = new Table(file, true, false);
            foreach (Row item in service2.GetData())
            {
                table2.Add(item.Name, item.Size, item.ModifyTime);
            }
            foreach (Row item in service2.GetFiles())
            {
                table2.AddFiles(item.Name, item.Size, item.ModifyTime);
            }
            table.Unite();
            table2.Unite();
            table.SelectPath();
            table2.SelectPath();


            components.Add((IComponent)table);
            components.Add((IComponent)table2);
        }
    }
}
