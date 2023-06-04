using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Midnight_Commander_Psotka.PopUps
{
    class MakeDirPopUp : PopUp
    {
        public string UserMadeName { get; set; }
        public int Marked { get; set; }
        public bool Type { get; set; }
        // False = Dir || True = File
        public int column { get; set; }
        public string FirstPath { get; set; }
        public char backslash = '\u005c';
        public MakeDirPopUp(string path, string userMade)
        {
            column = 2;
            Type = false;
            Marked = 1;
            FirstPath = path;
            UserMadeName = userMade;       
        }
        public override void Draw()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            string space = "";
            for (int i = Console.WindowWidth / 2 - 35; i < Console.WindowWidth / 2 + 35; i++)
            {
                space += " ";
            }
            for (int i = 0; i < 10; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + i);
                Console.WriteLine(space);
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 1);
            Console.Write(" ┌");
            for (int i = 0; i < 19; i++)
            {
                Console.Write("─");
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write(" Create a new Directory/File ");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < 18; i++)
            {
                Console.Write("─");
            }
            Console.Write("┐ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 2);
            Console.Write(" │ ");
            Console.Write("Enter Directory/File name:".PadRight(65));
            Console.Write("│ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 3);
            Console.Write(" │ ");
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            if (UserMadeName.Length > 65)
            {
                int i = UserMadeName.Length - 65;
                string str = UserMadeName;
                str = str.Remove(31, i + 1);
                str = str.Insert(31, "~");
                Console.Write(str);
            }
            else
            {
                Console.Write(UserMadeName.PadRight(65));
            }
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Write("│ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 4);
            Console.Write(" ├");
            for (int i = 0; i < 66; i++)
            {
                Console.Write("─");
            }
            Console.Write("┤ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 5);
            Console.Write(" │");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 12, 8 + 5);
            if (Type == false)
            {
                Console.Write("Directory[X] ");
                Console.Write("File[ ] ");
            }
            else
            {
                Console.Write("Directory[ ] ");
                Console.Write("File[X]");
            }
            Console.SetCursorPosition(Console.WindowWidth / 2 + 33, 8 + 5);
            Console.Write("│ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 6);
            string line = "";
            for (int i = 0; i < 66; i++)
            {
                line += " ";
            }
            Console.Write(" │"+line+"│ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 7);
            Console.Write(" │ ");
            for (int i = 0; i < 22; i++)
            {
                Console.Write(" ");
            }
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
            for (int i = 0; i < 22; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 8);
            Console.Write(" └");
            for (int i = 0; i < 66; i++)
            {
                Console.Write("─");
            }
            Console.Write("┘ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 9);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Gray;
        }
        public override void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.Enter)
            {
                if (Marked == 1 && UserMadeName != "" && Type == false)
                {
                    try
                    {
                        UserMadeName = FirstPath + @"\" + UserMadeName;
                        DirectoryInfo dir = new DirectoryInfo(UserMadeName);
                        dir.Create();
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
                else if (Marked == 1 && UserMadeName != "" && Type == true)
                {
                    try
                    {
                        UserMadeName = FirstPath + @"\" + UserMadeName;
                        FileInfo file = new FileInfo(UserMadeName);
                        file.Create();
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
            else if (info.Key == ConsoleKey.Tab)
            {
                Type = !Type;
            }
            else if (CheckKeys(info) == true)
            {
                UserMadeName += info.KeyChar;
            }
            
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
