   using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka
{
    public class Row
    {
        public string Name { get; set; }
        public string Size { get; set; }
        public string ModifyTime { get; set; }
        public Row(string name, string size, string modifyTime)
        {
            Name = name;
            Size = size;
            ModifyTime = modifyTime;
        }
    }
}
