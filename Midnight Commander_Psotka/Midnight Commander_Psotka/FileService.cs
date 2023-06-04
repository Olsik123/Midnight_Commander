using Midnight_Commander_Psotka.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Midnight_Commander_Psotka.Windows
{
    public class FileService
    {
        public string File { get; set; }


        public FileService(string file)
        {
            this.File = file;

        }
        public List<Row> GetData()
        {
            List<string> result = new List<string>();
            List<string> resultint = new List<string>();
            List<string> resultmod = new List<string>();  
            DirectoryInfo dir = new DirectoryInfo(this.File);            
            result.Add("..");
            resultint.Add("");
            resultmod.Add("");
            
            foreach (DirectoryInfo item in dir.GetDirectories())
            {
                result.Add(item.Name);
                string str3 = item.LastWriteTime.ToString();
                str3=str3.Remove(11);
                resultmod.Add(str3);
                resultint.Add("4096");
            }


            List<Row> listrow = new List<Row>();
            for (int i = 0; i < result.Count; i++)
            {
                Row row = new Row(result[i], resultint[i], resultmod[i]);
                listrow.Add(row);

            }
            return listrow;
        }
        public List<Row> GetFiles()
        {
            List<string> result = new List<string>();
            List<string> resultint = new List<string>();
            List<string> resultmod = new List<string>();



            DirectoryInfo dir = new DirectoryInfo(this.File);
            foreach (FileInfo item in dir.GetFiles())
            {
                string Name = item.Name.ToString();
                result.Add(Name);
                string Time = item.LastWriteTime.ToString();
                Time = Time.Remove(11);
                resultmod.Add(Time);
                double Size = item.Length;
                string SizeStr = Size.ToString();
                if (SizeStr.Length > 6)
                {
                    Size *= 0.000001;
                    SizeStr = Size.ToString();
                    string[] SplitNumber = SizeStr.Split(',');
                    if (SplitNumber[0].Length > 4)
                    {
                        Size *= 0.001;
                        SizeStr = Size.ToString();
                        string[] SplitNumber2 = SizeStr.Split(',');
                        SizeStr = "GB" + SplitNumber2[0];
                    }
                    else
                    {
                        SizeStr = "MB" + SplitNumber[0];
                    }
                }
                else
                {
                    SizeStr = Size.ToString();
                }
                resultint.Add(SizeStr);
            }


            List<Row> listrow = new List<Row>();
            for (int i = 0; i < result.Count; i++)
            {
                Row row = new Row(result[i], resultint[i], resultmod[i]);
                listrow.Add(row);

            }
            return listrow;
        }



    }
}
