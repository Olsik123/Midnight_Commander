using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Midnight_Commander_Psotka.PopUps
{
    public class DeletePopUp : PopUp
    {
        public string UserMadeName { get; set; }
        public bool Warning { get; set; }
        public int Marked { get; set; }
        public string FirstPath { get; set; }
        public char backslash = '\u005c';
        public DeletePopUp( string path, string userMade)
        {
            Marked = 1;
            FirstPath = path;
            UserMadeName = userMade;
        }
        public override void Draw()
        {
            if (Warning == false)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                string[] splittedPath = FirstPath.Split(@"\");
                string OnlyPath = splittedPath[splittedPath.Length - 1];

                string space = "";
                for (int i = Console.WindowWidth / 2 - 15; i < Console.WindowWidth / 2 + 15; i++)
                {
                    space += " ";
                }
                for (int i = 0; i < 9; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + i);
                    Console.WriteLine(space);

                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 1);
                Console.Write(" ┌");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write("─");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Delete ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 9; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 6, 8 + 2);
                Console.Write("Delete file?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 12, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 3);
                Console.Write(" │ ");
                if (OnlyPath.Length > 20)
                {
                    Console.Write("  ");
                    int i = OnlyPath.Length - 20;
                    string str = OnlyPath;
                    str = str.Remove(8, i + 1);
                    str = str.Insert(9, "~");
                    Console.WriteLine(str);
                }
                else
                {
                    int pad = 24 - OnlyPath.Length;
                    Console.WriteLine(OnlyPath.PadLeft(pad / 2 + OnlyPath.Length));
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 + 12, 8 + 3);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 4);
                Console.Write(" ├");
                for (int i = 0; i < 26; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┤ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 5);
                Console.Write(" │ ");
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(" ");
                }
                if (Marked == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[< YES >]");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write(" ");
                    Console.Write("[  NO  ]");
                }
                else
                {
                    Console.Write("[  YES  ]");
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[< NO >]");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                for (int i = 0; i < 3; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 6);
                Console.Write(" └");
                for (int i = 0; i < 26; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
                string space = "";
                for (int i = Console.WindowWidth / 2 - 25; i < Console.WindowWidth / 2 + 25; i++)
                {
                    space += " ";
                }
                for (int i = 0; i < 9; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + i);
                    Console.WriteLine(space);

                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 1);
                Console.Write(" ┌");
                for (int i = 0; i < 19; i++)
                {
                    Console.Write("─");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" Delete ");
                Console.ForegroundColor = ConsoleColor.White;
                for (int i = 0; i < 19; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 6, 8 + 2);
                Console.Write("Delete file?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 22, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 3);
                Console.Write(" │ ");                
                Console.WriteLine("THERE IS FILES AND FOLDERS IN THIS FOLDER");              
                Console.SetCursorPosition(Console.WindowWidth / 2 + 22, 8 + 3);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 4);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 7, 8 + 4);
                Console.WriteLine("ARE YOU SURE?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 22, 8 + 4);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 5);
                Console.Write(" ├");
                for (int i = 0; i < 46; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┤ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 6);
                Console.Write(" │ ");
                for (int i = 0; i < 13; i++)
                {
                    Console.Write(" ");
                }
                if (Marked == 1)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[< YES >]");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write(" ");
                    Console.Write("[  NO  ]");
                }
                else
                {
                    Console.Write("[  YES  ]");
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[< NO >]");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }
                for (int i = 0; i < 13; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 25, 8 + 7);
                Console.Write(" └");
                for (int i = 0; i < 46; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
            }
            
        }
        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Enter)
            {
                try
                {
                    if (Warning == false)
                    {
                        if (Marked == 1)
                        {
                            DirectoryInfo dir = new DirectoryInfo(FirstPath);
                            if (GetData(dir).Count != 0)
                            {
                                Warning = true;
                            }
                            else
                            {
                                dir.Delete(true);
                                this.Close();
                            }
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Marked == 1)
                        {
                            DirectoryInfo dir = new DirectoryInfo(FirstPath);
                            dir.Delete(true);
                            this.Close();
                        }
                        else
                        {
                            this.Close();
                        }
                    }                  
                }
                catch (UnauthorizedAccessException)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    string space = "";
                    for (int i = Console.WindowWidth / 2 - 18; i < Console.WindowWidth / 2 + 18; i++)
                    {
                        space += " ";
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 8 + i);
                        Console.WriteLine(space);
                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 9);
                    Console.Write(" ┌");
                    for (int i = 0; i < 12; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("Warning!");
                    for (int i = 0; i < 12; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┐ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 10);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 10);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 11);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 12, 11);
                    Console.Write("Access Denied!");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 11);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 12);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 12);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 13);
                    Console.Write(" └");
                    for (int i = 0; i < 32; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┘ ");
                    Thread.Sleep(1000);
                }
                catch
                {

                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    string space = "";
                    for (int i = Console.WindowWidth / 2 - 18; i < Console.WindowWidth / 2 + 18; i++)
                    {
                        space += " ";
                    }
                    for (int i = 0; i < 7; i++)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 8 + i);
                        Console.WriteLine(space);
                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 9);
                    Console.Write(" ┌");
                    for (int i = 0; i < 12; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("Warning!");
                    for (int i = 0; i < 12; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┐ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 10);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 10);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 11);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 12, 11);
                    Console.Write("This folder doesn't exist!");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 11);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 12);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 16, 12);
                    Console.Write("│ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 18, 13);
                    Console.Write(" └");
                    for (int i = 0; i < 32; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┘ ");
                }              

            }
            else if (info.Key == ConsoleKey.RightArrow && Marked != 2)
            {
                Marked++;
            }
            else if (info.Key == ConsoleKey.LeftArrow && Marked != 1)
            {
                Marked--;
            }
        }
        public List<DirectoryInfo> GetData(DirectoryInfo d)
        {
            List<DirectoryInfo> result = new List<DirectoryInfo>();
            foreach (DirectoryInfo item in d.GetDirectories())
            {
                result.Add(item);
            }
            return result;

        }

    }
}
