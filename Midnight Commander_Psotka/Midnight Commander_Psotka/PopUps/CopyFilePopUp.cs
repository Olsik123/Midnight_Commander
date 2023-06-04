using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Midnight_Commander_Psotka.PopUps
{
    class CopyFilePopUp: PopUp
    {
            public string UserMadeName { get; set; }
            public int Marked { get; set; }
            public string FirstPath { get; set; }
            public char backslash = '\u005c';

            public CopyFilePopUp(string path, string userMade)
            {
                Marked = 1;
                FirstPath = path;
                UserMadeName = userMade;
            }
            public override void Draw()
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                string space = "";
                for (int i = Console.WindowWidth / 2 - 50; i < Console.WindowWidth / 2 + 50; i++)
                {
                    space += " ";
                }
                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + i);
                    Console.WriteLine(space);
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 1);
                string line = "";
                for (int i = 0; i < 46; i++)
                {
                    line += "─";
                }
                Console.Write(" ┌" + line);
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write("Copy");
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(line + "┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 2);
                Console.Write($"{" │ "}{"The file/folder you want to copy:".PadRight(95)}{"│ "}");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 3);
                Console.Write(" │ ");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                if (FirstPath.Length > 95)
                {
                    int i = FirstPath.Length - 95;
                    string str = FirstPath;
                    str = str.Remove(41, i + 1);
                    str = str.Insert(41, "~");
                    Console.Write(str);
                }
                else
                {
                    Console.Write(FirstPath.PadRight(95));
                }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 4);
                Console.Write($"{" │ "}{"Enter path where to copy:".PadRight(95)}{"│ "}");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 5);
                Console.Write(" │ ");
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                if (UserMadeName.Length > 95)
                {
                    int i = UserMadeName.Length - 95;
                    string str = UserMadeName;
                    str = str.Remove(41, i + 1);
                    str = str.Insert(41, "~");
                    Console.Write(str);
                }
                else
                {
                    Console.Write(UserMadeName.PadRight(95));
                }
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 6);
                line = "";
                for (int i = 0; i < 96; i++)
                {
                    line += "─";
                }
                Console.Write(" ├" + line + "┤ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 7);
                line = "";
                for (int i = 0; i < 37; i++)
                {
                    line += " ";
                }
                Console.Write(" │ " + line);
                if (Marked == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< OK >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[  CANCEL  ]");
                }
                else
                {
                    Console.Write("[  OK  ]");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< CANCEL >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                Console.Write(line + " │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 8);
                line = "";
                for (int i = 0; i < 96; i++)
                {
                    line += "─";
                }
                Console.Write(" └" + line + "┘ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 50, 8 + 9);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            public override void HandleKey(ConsoleKeyInfo info)
            {
                if (info.Key == ConsoleKey.Enter)
                {
                    if (Marked == 1 && UserMadeName != "")
                    {
                        try
                        {
                            FileInfo fil = new FileInfo(FirstPath);
                            fil.MoveTo(UserMadeName);
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
                            Thread.Sleep(1000);
                        }
                        
                    }


                    this.Close();
                }
                else if (info.Key == ConsoleKey.RightArrow && Marked != 2)
                {
                    Marked++;
                }
                else if (info.Key == ConsoleKey.LeftArrow && Marked != 1)
                {
                    Marked--;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (UserMadeName.Length != 0)
                    {
                        UserMadeName = UserMadeName.Remove(UserMadeName.Length - 1);
                    }
                }
                else if (CheckKeys(info) == true || info.KeyChar == backslash || info.KeyChar == ':')
                {
                    UserMadeName += info.KeyChar;
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
            public List<string> GetFiles(DirectoryInfo d)
            {
                List<string> result = new List<string>();
                foreach (FileInfo item in d.GetFiles())
                {
                    result.Add(item.ToString());
                }
                return result;
            }
            public bool CheckKeys(ConsoleKeyInfo info)
            {
                if (info.KeyChar == backslash)
                {
                    return false;
                }
                else if (info.KeyChar == '/')
                {
                    return false;
                }
                else if (info.KeyChar == ':')
                {
                    return false;
                }
                else if (info.KeyChar == '*')
                {
                    return false;
                }
                else if (info.KeyChar == '?')
                {
                    return false;
                }
                else if (info.KeyChar == '"')
                {
                    return false;
                }
                else if (info.KeyChar == '<')
                {
                    return false;
                }
                else if (info.KeyChar == '>')
                {
                    return false;
                }
                else if (info.KeyChar == '|')
                {
                    return false;
                }
                else if (info.KeyChar == ' ')
                {
                    return false;
                }
                else if (info.KeyChar == '\0')
                {
                    return false;
                }
                else
                    return true;
            }        
    }
}
