using Midnight_Commander_Psotka.PopUps;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Midnight_Commander_Psotka.Components
{
    public class Editor : IComponent
    {
        public string Path { get; set; }
        public string Pad { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int XCursorPosition { get; set; }
        public int YCursorPosition { get; set; }
        public int LeftEdge { get; set; }
        public int RightEdge { get; set; }
        public int TopTop { get; set; }
        public int BottomTop { get; set; }
        public bool Finding { get; set; }
        public int Replacing { get; set; }
        public string Wordfind { get; set; }
        public string Wordreplace { get; set; }
        public int WordFindMarked { get; set; }
        public int WordReplaceMarked { get; set; }
        public bool WordFindReplaceChoosen { get; set; }
        public int XAlreadySearched { get; set; }
        public int YAlreadySearched { get; set; }
        public bool ReplaceAll { get; set; }
        public bool Edited { get; set; }
        public int TypeOfDialog { get; set; }
        public int Marked { get; set; }
        public int Marking { get; set; }
        public int Above { get; set; }
        public int MarkedXCharStart{ get; set; }
        public int MarkedYCharStart { get; set; }
        public int MarkedXCharEnd { get; set; }
        public int MarkedYCharEnd { get; set; }
        public List<string> TextRow { get; set; } = new List<string>();
        public List<string> MarkedTextRow { get; set; } = new List<string>();
        public Editor(string path)
        {
            Pad = "";
            for (int i = 0; i <= Console.WindowWidth; i++)
            {
                Pad += "               ";
            }
            Above = 1;
            X = 0;
            Y = 0;
            Edited = false;
            Finding = false;
            Wordfind = "";
            WordFindMarked = 0;
            WordFindReplaceChoosen = false;
            Wordreplace = "";
            WordReplaceMarked = 0;
            XAlreadySearched = 0;
            YAlreadySearched = 0;
            ReplaceAll = false;
            LeftEdge = 0;
            RightEdge = Console.WindowWidth;
            BottomTop = Console.WindowHeight-1;
            TopTop = 0;
            Path = path;
            Marked = 1;
            Marking = 0;
            using StreamReader sr = new StreamReader(Path);
            {
                while (!sr.EndOfStream)
                {
                    TextRow.Add(sr.ReadLine());
                }
            }
            Console.Clear();
        }
        public void Draw()
        {
            using StreamReader sr = new StreamReader(Path);
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.White;
                int topTop = BottomTop - Console.WindowHeight+1;
                string editLabel = MakeLabel(1);
                string markedLabel = MakeLabel(2);
                Console.CursorVisible = false;
                Console.SetCursorPosition(0, 0);
                string stringCovert = "0";
                try
                {
                    stringCovert = TextRow[Y].Substring(X, 1);                   
                }
                catch 
                {

                }
                int currentChar = 0;
                int charMax = 0;
                foreach (string item in TextRow)
                {
                    foreach (char character in item)
                    {
                        charMax++;
                    }
                }
                int maxI = 0;
                int maxI2 = 0;
                foreach (string item in TextRow)
                {
                    if (Y > maxI)
                    {
                        foreach (char character in item)
                        {
                            currentChar++;
                        }
                    }
                    else if (Y == maxI)
                    {
                        foreach (char character in item)
                        {
                            if (X >= maxI2)
                            {
                                currentChar++;
                            }
                            maxI2++;
                        }
                    }
                    maxI++;
                }
                int ascii = (int)Convert.ToChar(stringCovert);
                string hex = "0x0" + String.Format("{0:X}", (int)Convert.ToChar(stringCovert));
                Console.Write($"{Path} [{markedLabel}{editLabel}--] L:  { topTop} + { Y + 1 - topTop} { topTop + Console.WindowHeight - 1}/{ TextRow.Count} ({ currentChar}/{ charMax}) { ascii} { hex}   ".PadRight(Console.WindowWidth));
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                DrawRows();
                Console.CursorVisible = true;
                DrawDownLabels();
                Console.SetCursorPosition(XCursorPosition, YCursorPosition + 1);
            }
            if (TypeOfDialog == 1)
            {
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                string space = "";
                for (int i = Console.WindowWidth / 2 - 23; i < Console.WindowWidth / 2 + 23; i++)
                {
                    space += " ";
                }
                for (int i = 0; i < 7; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + i);
                    Console.WriteLine(space);
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 1);
                Console.Write(" ┌");
                for (int i = 0; i < 19; i++)
                {
                    Console.Write("─");
                }
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Quit");
                Console.ForegroundColor = ConsoleColor.Black;
                for (int i = 0; i < 19; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 2);
                Console.Write(" │");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 15, 8 + 2);
                Console.Write("Are you sure you want to quit?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 21, 8 + 2);
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 3);
                Console.Write(" │");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 21, 8 + 3);
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 4);
                Console.Write(" │ ");
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(" ");
                }
                if (Marked == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< QUIT >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[  CANCEL  ]");
                }
                else
                {
                    Console.Write("[  QUIT  ]");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< CANCEL >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < 9; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 5);
                Console.Write(" └");
                for (int i = 0; i < 42; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
                DrawDownLabels();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 23, 8 + 6);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else if (TypeOfDialog == 2)
            {
                Console.CursorVisible = false;
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
                string space = "";
                for (int i = Console.WindowWidth / 2 - 35; i < Console.WindowWidth / 2 + 35; i++)
                {
                    space += " ";
                }
                for (int i = 0; i < 7; i++)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + i);
                    Console.WriteLine(space);
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 1);
                Console.Write(" ┌");
                for (int i = 0; i < 29; i++)
                {
                    Console.Write("─");
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" Quit? ");
                Console.ForegroundColor = ConsoleColor.Black;
                for (int i = 0; i < 30; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 2);
                Console.Write(" │ ");
                Console.Write("               You made changes in your text file!".PadRight(65));
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 3);
                Console.Write(" │ ");
                Console.Write("                     Do you want to save them?".PadRight(65));
                Console.Write("│ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 4);
                Console.Write(" │ ");
                for (int i = 0; i < 18; i++)
                {
                    Console.Write(" ");
                }
                if (Marked == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< YES >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[  NO  ]");
                    Console.Write("[  CANCEL  ]");
                }
                else if (Marked == 2)
                {
                    Console.Write("[  YES  ]");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< NO >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write("[  CANCEL  ]");
                }
                else
                {
                    Console.Write("[  YES  ]");
                    Console.Write("[  NO  ]");
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Write("[< CANCEL >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < 17; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 5);
                Console.Write(" └");
                for (int i = 0; i < 66; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
                DrawDownLabels();
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 6);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Gray;       
                
            }
        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (Finding == true)
            {
                if (Char.IsLetterOrDigit(info.KeyChar) || Char.IsPunctuation(info.KeyChar) || Char.IsSymbol(info.KeyChar) || (info.KeyChar == ' '))
                {
                    Wordfind += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {                   
                        if (Wordfind.Length != 0)
                        {
                            Wordfind = Wordfind.Remove(Wordfind.Length - 1);
                        }
                }
                else if (info.Key == ConsoleKey.RightArrow && WordFindMarked != 1)
                {
                    WordFindMarked++;
                }
                else if (info.Key == ConsoleKey.Enter && WordFindMarked == 0)
                {
                    bool thereis = false;
                    foreach (string item in TextRow)
                    {
                        if (item.Contains(Wordfind) == true)
                        {
                            thereis = true;
                        }
                    }
                    if (thereis == true)
                    {
                        int IndexOfColumnFind = -1;
                        int IndexOfRowFind = -1;
                        for (int i = Y; i < TextRow.Count; i++)
                        {
                            IndexOfColumnFind = TextRow[i].IndexOf(Wordfind);
                            IndexOfRowFind = i;
                            if (i == Y && IndexOfColumnFind < X)
                            {
                                IndexOfColumnFind = -1;
                            }
                            if (IndexOfColumnFind != -1)
                            {
                                break;
                            }
                        }
                        if (IndexOfColumnFind + Wordfind.Length >= RightEdge || IndexOfColumnFind <= LeftEdge)
                        {
                            LeftEdge = Math.Max(TextRow[IndexOfRowFind].Length - Console.WindowWidth / 2, 0);
                            RightEdge = LeftEdge + Console.WindowWidth;
                            
                        }
                        if (IndexOfRowFind >= BottomTop || IndexOfRowFind <= TopTop)
                        {
                            TopTop = Math.Max(IndexOfRowFind - Console.WindowHeight / 2, 0);
                            BottomTop = TopTop + Console.WindowHeight-1;
                        }
                        Marking = 2;
                        MarkedXCharStart = IndexOfColumnFind;
                        MarkedXCharEnd = MarkedXCharStart + Wordfind.Length - 1;
                        MarkedYCharStart = IndexOfRowFind;
                        MarkedYCharEnd = MarkedYCharStart;
                        MarkedTextRow.Clear();
                        MarkedTextRow.Add(Wordfind);
                        X = MarkedXCharStart;
                        Y = MarkedYCharStart;
                        XCursorPosition = X - LeftEdge;
                        YCursorPosition = Y - TopTop;
                        Finding = false;
                        Wordfind = "";
                        WordFindMarked = 0;
                    }
                }
                else if (info.Key == ConsoleKey.Enter && WordFindMarked == 1)
                {
                    Finding = false;
                    Wordfind = "";
                    WordFindMarked = 0;
                }
                else if (info.Key == ConsoleKey.LeftArrow && WordFindMarked != 0)
                {
                    WordFindMarked--;
                }
            }
            else if (Replacing != 0)
            {
                if ((Char.IsLetterOrDigit(info.KeyChar) || Char.IsPunctuation(info.KeyChar) || Char.IsSymbol(info.KeyChar) || (info.KeyChar == ' ') )&& Replacing == 1)
                {
                    if (WordFindReplaceChoosen == false)
                    {
                        Wordfind += info.KeyChar;
                    }
                    else
                    {
                        Wordreplace += info.KeyChar;
                    }
                }
                else if (info.Key == ConsoleKey.Backspace && Replacing == 1)
                {
                    if (WordFindReplaceChoosen == false)
                    {
                        if (Wordfind.Length != 0)
                        {
                            Wordfind = Wordfind.Remove(Wordfind.Length - 1);
                        }
                    }
                    else
                    {
                        if (Wordreplace.Length != 0)
                        {
                            Wordreplace = Wordreplace.Remove(Wordreplace.Length - 1);
                        }
                    }
                }
                else if (info.Key == ConsoleKey.Tab && Replacing == 1)
                {
                    WordFindReplaceChoosen = !WordFindReplaceChoosen;
                }
                else if (info.Key == ConsoleKey.RightArrow && WordFindMarked != 1 && Replacing == 1)
                {
                    WordFindMarked++;
                }
                else if (info.Key == ConsoleKey.LeftArrow && WordFindMarked != 0 && Replacing == 1)
                {
                    WordFindMarked--;
                }
                else if (info.Key == ConsoleKey.Enter && WordFindMarked == 0 && Replacing == 1)
                {
                    Replacing = 2;
                    Lol();
                    Find();
                    Lol();
                }
                else if (info.Key == ConsoleKey.Enter && WordFindMarked == 1 && Replacing == 1)
                {
                    ReturnToNormal();
                }
                else if (info.Key == ConsoleKey.RightArrow && WordReplaceMarked != 3 && Replacing == 2)
                {
                    WordReplaceMarked++;
                }
                else if (info.Key == ConsoleKey.LeftArrow && WordReplaceMarked != 0 && Replacing == 2)
                {
                    WordReplaceMarked--;
                }
                else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 0 && Replacing == 2)
                {
                    Delete();
                    MarkedTextRow.Clear();                    
                    CopyTroughReplace();
                    Find();
                    Lol();
                }
                else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 1 && Replacing == 2)
                {
                    ReplaceAll = true;
                    Delete();
                    MarkedTextRow.Clear();
                    CopyTroughReplace();
                    Find();
                    Lol();
                }
                else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 2 && Replacing == 2)
                {
                    Find();
                    Lol();
                }
                else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 3 && Replacing == 2)
                {
                    ReturnToNormal();
                }
            }
            else if (info.Key == ConsoleKey.RightArrow && Marked != 2 && TypeOfDialog == 1)
            {
                Marked++;
            }
            else if (info.Key == ConsoleKey.LeftArrow && Marked != 1 && TypeOfDialog == 1)
            {
                Marked--;
            }
            else if (info.Key == ConsoleKey.Enter && TypeOfDialog == 1)
            {

                if (Marked == 1)
                {
                    Application.Window = Application.ListWindow;
                }
                else
                {
                    Console.CursorVisible = true;
                    TypeOfDialog = 0;
                    Marked = 1;
                }
            }
            else if (info.Key == ConsoleKey.RightArrow && Marked != 3 && TypeOfDialog == 2)
            {
                Marked++;
            }
            else if (info.Key == ConsoleKey.LeftArrow && Marked != 1 && TypeOfDialog == 2)
            {
                Marked--;
            }
            else if (info.Key == ConsoleKey.Enter && TypeOfDialog == 2)
            {
                if (Marked == 1)
                {
                    FileInfo fil = new FileInfo(Path);
                    fil.Delete();
                    fil.Create().Close();
                    using StreamWriter sw = new StreamWriter(Path);
                    {
                        foreach (string item in TextRow)
                        {
                            sw.WriteLine(item);
                        }
                    }
                    Edited = false;
                    Application.Window = Application.ListWindow;
                }
                else if (Marked == 2)
                {
                    Application.Window = Application.ListWindow;
                }
                else
                {
                    Console.CursorVisible = true;
                    TypeOfDialog = 0;
                    Marked = 1;
                }
            }
            else if (info.Key == ConsoleKey.RightArrow && X < TextRow[Y].Length)
            {
                X++;
                XCursorPosition++;
                if (X > RightEdge - 1)
                {
                    MoveRight();
                }

            }
            else if (info.Key == ConsoleKey.LeftArrow && X != 0)
            {
                X--;
                XCursorPosition--;
                if (X <= LeftEdge - 1)
                {
                    MoveLeft();
                }
            }
            else if (info.Key == ConsoleKey.DownArrow && Y < TextRow.Count - 1)
            {
                Y++;
                YCursorPosition++;
                if (X > TextRow[Y].Length)
                {
                    X = TextRow[Y].Length;
                    if (TextRow[Y].Length > Console.WindowWidth)
                    {
                        XCursorPosition = Console.WindowWidth / 2 + 1;
                        LeftEdge = Math.Max(TextRow[Y].Length - 1 - Console.WindowWidth / 2, 0);
                        RightEdge = Math.Max(TextRow[Y].Length - 1 + Console.WindowWidth / 2, Console.WindowWidth);
                    }
                    else
                    {
                        XCursorPosition = TextRow[Y].Length;
                        LeftEdge = 0;
                        RightEdge = Console.WindowWidth;
                    }
                }
                if (Y > TopTop + Console.WindowHeight - 3)
                {
                    MoveDown();
                }
            }
            else if (info.Key == ConsoleKey.UpArrow && Y != 0)
            {
                Y--;
                YCursorPosition--;
                if (X > TextRow[Y].Length)
                {
                    X = TextRow[Y].Length;
                    if (TextRow[Y].Length > Console.WindowWidth)
                    {
                        XCursorPosition = Console.WindowWidth / 2 + 1;
                        LeftEdge = Math.Max(TextRow[Y].Length - 1 - Console.WindowWidth / 2, 0);
                        RightEdge = Math.Max(TextRow[Y].Length - 1 + Console.WindowWidth / 2, Console.WindowWidth);
                    }
                    else
                    {
                        XCursorPosition = TextRow[Y].Length;
                        LeftEdge = 0;
                        RightEdge = Console.WindowWidth;
                    }
                }
                if (Y < TopTop)
                {
                    MoveUp();
                }
            }
            else if (info.Key == ConsoleKey.Enter)
            {
                Edited = true;
                if (X == TextRow[Y].Length)
                {
                    BackspaceMinusRow();
                }
                else
                {
                    try
                    {
                        string Behind = TextRow[Y].Substring(X);
                        TextRow[Y] = TextRow[Y].Remove(X);
                        string A = TextRow[Y];
                        TextRow.Insert(Y + 1, Behind);
                        string B = TextRow[Y];
                    }
                    catch
                    {
                        BackspaceMinusRow();
                    }
                    if (X > TextRow[Y].Length)
                    {
                        X = TextRow[Y].Length;
                        XCursorPosition = Math.Min(TextRow[Y].Length, Console.WindowWidth / 2);
                        SetEdges();
                    }
                }
                if (Y > TopTop + Console.WindowHeight - 2)
                {
                    MoveDown();
                }
            }
            else if (Char.IsLetterOrDigit(info.KeyChar) || Char.IsPunctuation(info.KeyChar) || Char.IsSymbol(info.KeyChar) || (info.KeyChar == ' '))
            {
                Edited = true;
                try
                {
                    TextRow[Y] = TextRow[Y].Insert(X, info.KeyChar.ToString());
                }
                catch
                {
                    TextRow.Add(info.KeyChar.ToString());
                }
                X++;
                XCursorPosition++;
                if (X > RightEdge - 1)
                {
                    MoveRight();
                }
            }
            else if (info.Key == ConsoleKey.Backspace)
            {
                Edited = true;
                if (X != 0)
                {
                    Edited = true;
                    TextRow[Y] = TextRow[Y].Remove(X - 1, 1);
                    X--;
                    XCursorPosition--;
                }
                else if (X == 0 && Y != 0)
                {
                    try
                    {
                        TextRow[Y - 1] += TextRow[Y];
                        string lol = TextRow[Y - 1];
                        X = TextRow[Y - 1].Length;
                        if (TextRow[Y - 1].Length > Console.WindowWidth)
                        {
                            XCursorPosition = Console.WindowWidth / 2 + 1;
                            LeftEdge = Math.Max(TextRow[Y - 1].Length - 1 - Console.WindowWidth / 2, 0);
                            RightEdge = Math.Max(TextRow[Y - 1].Length - 1 + Console.WindowWidth / 2, Console.WindowWidth);
                        }
                        else
                        {
                            XCursorPosition = TextRow[Y - 1].Length;
                            LeftEdge = 0;
                            RightEdge = Console.WindowWidth;
                        }
                        TextRow.RemoveAt(Y);
                        Y--;
                        YCursorPosition--;
                    }
                    catch
                    {
                    }
                }
                if (X <= LeftEdge - 1)
                {
                    MoveLeft();
                }
                if (Y < TopTop)
                {
                    MoveUp();
                }
            }
            else if (info.Key == ConsoleKey.F2)
            {
                FileInfo fil = new FileInfo(Path);
                fil.Delete();
                fil.Create().Close();
                using StreamWriter sw = new StreamWriter(Path);
                {
                    foreach (string item in TextRow)
                    {
                        sw.WriteLine(item);
                    }
                }
                Edited = false;
            }   
            else if (info.Key == ConsoleKey.F3)
            {
                if (Marking == 0)
                {
                    MarkedXCharStart = X;
                    MarkedYCharStart = Y;
                    Marking++;
                }
                else if (Marking == 1 )
                {
                    if (Y>MarkedYCharStart || Y==MarkedYCharStart && X >= MarkedXCharStart)
                    {
                        MarkedXCharEnd = X;
                        MarkedYCharEnd = Y;
                    }
                    else
                    {
                        MarkedXCharEnd = MarkedXCharStart;
                        MarkedYCharEnd = MarkedYCharStart;
                        MarkedXCharStart = X;
                        MarkedYCharStart = Y;
                    }
                    Marking++;
                    for (int i = MarkedYCharStart;  i <= MarkedYCharEnd; i++)
                    {
                        if (MarkedYCharEnd == MarkedYCharStart)
                        {
                            if (X == TextRow[Y].Length)
                            {
                                MarkedTextRow.Add(TextRow[i].Substring(MarkedXCharStart, MarkedXCharEnd - MarkedXCharStart));
                            }
                            else
                            {
                                MarkedTextRow.Add(TextRow[i].Substring(MarkedXCharStart, MarkedXCharEnd - MarkedXCharStart + 1));
                            }
                        }
                        else if (i == MarkedYCharStart)
                        {
                            MarkedTextRow.Add(TextRow[i].Remove(0, MarkedXCharStart));
                        }
                        else if (i == MarkedYCharEnd)
                        {
                            try
                            {
                                MarkedTextRow.Add(TextRow[i].Remove(MarkedXCharEnd + 1));
                            }
                            catch
                            {
                                MarkedTextRow.Add(TextRow[i]);
                            }
                        }
                        else
                        {
                            MarkedTextRow.Add(TextRow[i]);
                        }                       
                    }
                }
                else
                {
                    MarkedTextRow.Clear();
                    Marking = 0;
                }
            }
            else if (info.Key == ConsoleKey.F4)
            {
                Edited = true;
                Replacing = 1;
                DrawReplaceDialog();
            }
            else if (info.Key == ConsoleKey.F5)
            {
                Edited = true;
                if (Marking == 2)
                {
                    Copy();
                    MarkedTextRow.Clear();
                    Marking = 0;
                }
                else
                {
                    DisplayError();
                }
            }
            else if (info.Key == ConsoleKey.F6)
            {
                Edited = true;
                if (Marking == 2)
                {
                    int XBefore = X;
                    int YBefore = Y-DeleteCount();
                    int YBeforeWithoutDelete = Y;
                    int MarkedXCharStartBefore = MarkedXCharStart;
                    if (Y <= DeleteCount()||Y<MarkedYCharStart)
                    {
                        YBefore = Y;
                    }
                    Copy();
                    Delete();
                    if (MarkedYCharStart == MarkedYCharEnd)
                    {
                        MarkedXCharEnd = MarkedXCharEnd - MarkedXCharStart + XBefore;
                    }
                    MarkedXCharStart = XBefore;
                    if (MarkedYCharStart == MarkedYCharEnd && MarkedYCharStart == YBeforeWithoutDelete && XBefore>MarkedXCharStartBefore)
                    {
                        MarkedXCharStart = XBefore - MarkedTextRow[0].Length;
                        MarkedXCharEnd -= MarkedTextRow[0].Length;
                    }
                    MarkedYCharStart = YBefore;
                    MarkedYCharEnd = MarkedYCharStart + MarkedTextRow.Count-1;
                    //X = XBefore;
                    //Y = YBefore;
                    //XCursorPosition = X - TopTop;
                    //YCursorPosition = Y - LeftEdge;
                }
                else
                {
                    DisplayError();
                }
            }
            else if (info.Key == ConsoleKey.F7)
            {
                Finding = true;
                DrawFindDialog();
            }
            else if (info.Key == ConsoleKey.F8)
            {
                Edited = true;
                if (Marking == 2)
                {
                    Delete();
                    MarkedTextRow.Clear();
                    Marking = 0;
                    if (X < RightEdge)
                    {
                        XCursorPosition = X - LeftEdge;
                    }
                    else
                    {
                        X = 0;
                        XCursorPosition = 0;
                        LeftEdge = 0;
                        RightEdge = Console.WindowWidth;
                    }
                    if (Y > TextRow.Count)
                    {
                        Y = TextRow.Count - 1;
                        if (Y < Console.WindowHeight)
                        {
                            TopTop = 0;
                            BottomTop = Console.WindowHeight-1;
                            YCursorPosition = Y;
                        }
                        else
                        {
                            YCursorPosition = Console.WindowHeight - 1;
                            TopTop = TextRow.Count - Console.WindowHeight;
                            BottomTop = TextRow.Count-1;
                        }
                        X = TextRow[Y].Length;
                        if (X < RightEdge)
                        {
                            XCursorPosition = X - LeftEdge;
                        }
                        else
                        {
                            X = 0;
                            XCursorPosition = 0;
                            LeftEdge = 0;
                            RightEdge = Console.WindowWidth;
                        }
                    }
                }
                else
                {
                    DisplayError();
                }
            }
            else if (info.Key == ConsoleKey.F10)
            {
                if (Edited == true)
                {
                    TypeOfDialog = 2;
                }
                else
                {
                    TypeOfDialog = 1;
                }                
            }                      
        }
        public void DisplayError()
        {
            
        }
        public void DrawDownLabels()
        {
            Console.SetCursorPosition(0, Console.WindowHeight-1);
            Console.ResetColor();
            List<string> HelpLabels = new List<string>();
            HelpLabels.Add(" ");
            HelpLabels.Add("Help");
            HelpLabels.Add("Save");
            HelpLabels.Add("Mark");
            HelpLabels.Add("Replace");
            HelpLabels.Add("Copy");
            HelpLabels.Add("Move");
            HelpLabels.Add("Find");
            HelpLabels.Add("Delete");
            HelpLabels.Add("-");
            HelpLabels.Add("Quit");
            for (int i = 1; i < HelpLabels.Count; i++)
            {
                if (i < 10)
                {
                    Console.Write(" " + i);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(HelpLabels[i].PadRight((Console.WindowWidth - 20) / 10, ' '));
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(i);
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(HelpLabels[i].PadRight((Console.WindowWidth - 20) / 10, ' '));
                    Console.ResetColor();
                }



            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
        }
        public void DrawRows()
        {   
            if (Finding == false&&Replacing ==0)
            {
                int topTop = BottomTop - Console.WindowHeight+1;
                int rowNumber = 1;
                int YDrew = TopTop;
                for (int i = topTop; i < Math.Min(TextRow.Count, BottomTop - 1); i++)
                {
                    Console.SetCursorPosition(0, rowNumber);
                    rowNumber++;
                    string RowTrimmed = TextRow[i] + Pad;
                    RowTrimmed = RowTrimmed.Remove(0, Math.Min(LeftEdge, RowTrimmed.Length));
                    if (RowTrimmed.Length > Console.WindowWidth)
                    {
                        RowTrimmed = RowTrimmed.Remove(Console.WindowWidth);
                    }
                    if (Marking == 0)
                    {
                        Console.Write(RowTrimmed);
                    }
                    else if ((Y > MarkedYCharStart || Y == MarkedYCharStart && X >= MarkedXCharStart) && Marking == 1)
                    {
                        if (YDrew < MarkedYCharStart || YDrew > Y)
                        {
                            Console.Write(RowTrimmed);
                            YDrew++;
                        }
                        else
                        {
                            int XDrew = LeftEdge;
                            foreach (char item in RowTrimmed)
                            {
                                if ((YDrew == MarkedYCharStart && XDrew >= MarkedXCharStart || YDrew > MarkedYCharStart) && (YDrew < Y || YDrew == Y && XDrew <= X))
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                if (XDrew > TextRow[YDrew].Length - 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string space = "                                                                                                                                                      ";
                                    try
                                    {
                                        space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                    }
                                    catch
                                    {

                                    }
                                    Console.Write(space);
                                    break;

                                }
                                Console.Write(item);
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                XDrew++;
                            }
                            YDrew++;
                        }
                    }
                    else if ((Y < MarkedYCharStart || Y == MarkedYCharStart && X < MarkedXCharStart) && Marking == 1)
                    {
                        if (YDrew > MarkedYCharStart || YDrew < Y)
                        {
                            Console.Write(RowTrimmed);
                            YDrew++;
                        }
                        else
                        {
                            int XDrew = LeftEdge;
                            foreach (char item in RowTrimmed)
                            {
                                if ((YDrew == MarkedYCharStart && XDrew <= MarkedXCharStart || YDrew < MarkedYCharStart) && (YDrew > Y || YDrew == Y && XDrew >= X))
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                if (XDrew > TextRow[YDrew].Length - 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string space = "                                                                                                                                                      ";
                                    try
                                    {
                                        space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                    }
                                    catch
                                    {

                                    }
                                    Console.Write(space);
                                    break;

                                }
                                Console.Write(item);
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                XDrew++;
                            }
                            YDrew++;
                        }
                    }
                    else if (Marking == 2)
                    {
                        if (YDrew < MarkedYCharStart || YDrew > MarkedYCharEnd)
                        {
                            Console.Write(RowTrimmed);
                            YDrew++;
                        }
                        else
                        {
                            int XDrew = LeftEdge;
                            foreach (char item in RowTrimmed)
                            {
                                if ((YDrew == MarkedYCharStart && XDrew >= MarkedXCharStart || YDrew > MarkedYCharStart) && (YDrew < MarkedYCharEnd || YDrew == MarkedYCharEnd && XDrew <= MarkedXCharEnd))
                                {
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                }
                                if (XDrew > TextRow[YDrew].Length - 1)
                                {
                                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    string space = "                                                                                                                                                      ";
                                    try
                                    {
                                        space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                    }
                                    catch
                                    {
                                        space = space.Remove(0);
                                    }
                                    Console.Write(space);
                                    break;
                                }
                                Console.Write(item);
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                XDrew++;
                            }
                            YDrew++;
                        }
                    }
                }
                if (rowNumber < Console.WindowHeight)
                {
                    while (rowNumber < Console.WindowHeight)
                    {
                        if (rowNumber < Console.WindowHeight - 1)
                        {

                            string RowTrimmed = Pad.Remove(Console.WindowWidth);
                            Console.WriteLine(RowTrimmed);
                        }
                        else
                        {
                            string RowTrimmed = Pad.Remove(Console.WindowWidth);
                            Console.Write(RowTrimmed);
                        }
                        rowNumber++;
                    }
                }
            }
            else if (Finding == true)
            {
                DrawFindDialog();
            }
            else if (Replacing != 0)
            {
                DrawReplaceDialog();
            }
           
        }
        public void Find()
        {
            bool thereis = false;
            foreach (string item in TextRow)
            {
                if (item.Contains(Wordfind) == true)
                {
                    thereis = true;
                }
            }
            if (thereis == true)
            {
                int IndexOfColumnFind = -1;
                int IndexOfRowFind = -1;
                for (int i = YAlreadySearched; i < TextRow.Count; i++)
                {
                    IndexOfColumnFind = TextRow[i].IndexOf(Wordfind);
                    int ColumnBefore = IndexOfColumnFind;
                    IndexOfRowFind = i;
                    if (i == YAlreadySearched && IndexOfColumnFind <= XAlreadySearched)
                    {
                        try
                        {
                            IndexOfColumnFind = TextRow[i].IndexOf(Wordfind, XAlreadySearched + 1);
                        }
                        catch
                        {
                            IndexOfColumnFind = -1;
                        }
                        if (IndexOfColumnFind == ColumnBefore)
                        {
                            IndexOfColumnFind = -1;
                        }
                    }
                    if (IndexOfColumnFind != -1)
                    {
                        break;
                    }
                }
                if (IndexOfColumnFind + Wordfind.Length >= RightEdge|| IndexOfColumnFind<=LeftEdge)
                {
                    LeftEdge = Math.Max(TextRow[IndexOfRowFind].Length - Console.WindowWidth / 2, 0);
                    RightEdge = LeftEdge + Console.WindowWidth;
                }
                if (IndexOfRowFind >= BottomTop|| IndexOfRowFind<=TopTop)
                {
                    TopTop = Math.Max(IndexOfRowFind - Console.WindowHeight / 2, 0);
                    BottomTop = TopTop + Console.WindowHeight-1;
                }
                YAlreadySearched = IndexOfRowFind;
                XAlreadySearched = IndexOfColumnFind;
                if (XAlreadySearched == -1)
                {
                    XAlreadySearched++;
                }
                if (YAlreadySearched == -1)
                {
                    YAlreadySearched++;
                }
                if (IndexOfColumnFind == -1)
                {
                    ReturnToNormal();
                }
                Marking = 2;
                MarkedXCharStart = IndexOfColumnFind;
                MarkedXCharEnd = MarkedXCharStart + Wordfind.Length - 1;
                MarkedYCharStart = IndexOfRowFind;
                MarkedYCharEnd = MarkedYCharStart;           
                MarkedTextRow.Clear();
                MarkedTextRow.Add(Wordfind);
                DrawReplaceDialog();
            }
            else
            {   if (ReplaceAll != true)
                {
                    ReturnToNormal();
                    ReplaceAll = false;
                }
                else
                {
                    ReplaceAll = false;
                }
            }

        }
        public void ReturnToNormal()
        {
            Replacing = 0;
            Wordfind = "";
            Wordreplace = "";
            WordFindMarked = 0;
            Marking = 0;
            MarkedTextRow.Clear();
            WordFindReplaceChoosen = false;
            XAlreadySearched = 0;
            YAlreadySearched = 0;
            ReplaceAll = false;
            TopTop = 0;
            BottomTop = Console.WindowHeight-1;
            X = 0;
            Y = 0;
            XCursorPosition = 0;
            YCursorPosition = 0;

        }
        public void DrawReplaceDialog()
        {
            if (Replacing == 1)
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
                for (int i = 0; i < 26; i++)
                {
                    Console.Write("─");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Find and replace");
                Console.ForegroundColor = ConsoleColor.Black;
                for (int i = 0; i < 25; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┐ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 2);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 3);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 13, 8 + 3);
                Console.WriteLine("What word you want to find?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 3);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 4);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 31, 8 + 4);
                if (WordFindReplaceChoosen == false)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" ");
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 8 + 4);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(Wordfind.PadRight(60));
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 4);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 5);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 13, 8 + 5);
                Console.WriteLine("What word you want to replace?");
                Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 5);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 6);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 31, 8 + 6);
                if (WordFindReplaceChoosen == true)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" ");
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 8 + 6);
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine(Wordreplace.PadRight(60));
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 6);
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 7);
                Console.Write(" ├");
                for (int i = 0; i < 66; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┤ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 8);
                Console.Write(" │ ");
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(" ");
                }
                if (WordFindMarked == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("[< OKEY >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                    Console.Write("[  CANCEL  ]");
                }
                else
                {
                    Console.Write("[  OKEY  ]");
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write("[< CANCEL >]");
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                for (int i = 0; i < 21; i++)
                {
                    Console.Write(" ");
                }
                Console.Write(" │ ");
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 9);
                Console.Write(" └");
                for (int i = 0; i < 66; i++)
                {
                    Console.Write("─");
                }
                Console.Write("┘ ");
            }
            else
            {
                if (MarkedYCharStart<=Console.WindowHeight/2+TopTop)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    string space = "";
                    for (int i = Console.WindowWidth / 2 - 35; i < Console.WindowWidth / 2 + 35; i++)
                    {
                        space += " ";
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight/2+1+ i);
                        Console.WriteLine(space);

                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 1);
                    Console.Write(" ┌");
                    for (int i = 0; i < 29; i++)
                    {
                        Console.Write("─");
                    }
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("Replace");
                    Console.ForegroundColor = ConsoleColor.Black;
                    for (int i = 0; i < 30; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┐ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 2);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, Console.WindowHeight / 2 + 1 + 2);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 3);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, Console.WindowHeight / 2 + 1 + 3);
                    Console.WriteLine(Wordfind.PadRight(60));
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, Console.WindowHeight / 2 + 1 + 3);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 4);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, Console.WindowHeight / 2 + 1 + 4);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("Replace to:");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, Console.WindowHeight / 2 + 1 + 4);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 5);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, Console.WindowHeight / 2 + 1 + 5);
                    Console.WriteLine(Wordreplace.PadRight(60));
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, Console.WindowHeight / 2 + 1 + 5);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 6);
                    Console.Write(" ├");
                    for (int i = 0; i < 66; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┤ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 7);
                    Console.Write(" │ ");
                    Console.Write("        ");
                    if (WordReplaceMarked == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< REPLACE >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else if (WordReplaceMarked == 1)
                    {
                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< ALL >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else if (WordReplaceMarked == 2)
                    {

                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< SKIP >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else
                    {
                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< CANCEL >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("         ");
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, Console.WindowHeight / 2 + 1 + 8);
                    Console.Write(" └");
                    for (int i = 0; i < 66; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┘ ");
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    string space = "";
                    for (int i = Console.WindowWidth / 2 - 35; i < Console.WindowWidth / 2 + 35; i++)
                    {
                        space += " ";
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2 - 35,  1 + i);
                        Console.WriteLine(space);

                    }
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35,  1);
                    Console.Write(" ┌");
                    for (int i = 0; i < 29; i++)
                    {
                        Console.Write("─");
                    }
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write("Replace");
                    Console.ForegroundColor = ConsoleColor.Black;
                    for (int i = 0; i < 30; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┐ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 2);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 2);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 3);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 3);
                    Console.WriteLine(Wordfind.PadRight(60));
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 3);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 4);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 4);
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("Replace to:");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 4);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 5);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 5);
                    Console.WriteLine(Wordreplace.PadRight(60));
                    Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 5);
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 6);
                    Console.Write(" ├");
                    for (int i = 0; i < 66; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┤ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 7);
                    Console.Write(" │ ");
                    Console.Write("        ");
                    if (WordReplaceMarked == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< REPLACE >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else if (WordReplaceMarked == 1)
                    {
                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< ALL >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else if (WordReplaceMarked == 2)
                    {

                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< SKIP >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.Write(" ");
                        Console.Write("[  CANCEL  ]");
                    }
                    else
                    {
                        Console.Write("[  REPLACE  ]");
                        Console.Write(" ");
                        Console.Write("[  ALL  ]");
                        Console.Write(" ");
                        Console.Write("[  SKIP  ]");
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write("[< CANCEL >]");
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    Console.Write("         ");
                    Console.Write(" │ ");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8);
                    Console.Write(" └");
                    for (int i = 0; i < 66; i++)
                    {
                        Console.Write("─");
                    }
                    Console.Write("┘ ");
                }
            }
        }
        public void DrawFindDialog()
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            string space = "";
            for (int i = Console.WindowWidth / 2 - 35; i < Console.WindowWidth / 2 + 35; i++)
            {
                space += " ";
            }
            for (int i = 0; i < 8; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + i);
                Console.WriteLine(space);

            }
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 1);
            Console.Write(" ┌");
            for (int i = 0; i < 31; i++)
            {
                Console.Write("─");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Find");
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < 31; i++)
            {
                Console.Write("─");
            }
            Console.Write("┐ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 2);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 2);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 3);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 13, 8 + 3);
            Console.WriteLine("What word you want to find?");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 3);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 4);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 8 + 4);
            Console.SetCursorPosition(Console.WindowWidth / 2 - 30, 8 + 4);
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine(Wordfind.PadRight(60));
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(Console.WindowWidth / 2 + 32, 8 + 4);
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 5);
            Console.Write(" ├");
            for (int i = 0; i < 66; i++)
            {
                Console.Write("─");
            }
            Console.Write("┤ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 6);
            Console.Write(" │ ");
            for (int i = 0; i < 20; i++)
            {
                Console.Write(" ");
            }
            if (WordFindMarked == 0)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("[< OKEY >]");
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(" ");
                Console.Write("[  CANCEL  ]");
            }
            else
            {
                Console.Write("[  OKEY  ]");
                Console.Write(" ");
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("[< CANCEL >]");
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            for (int i = 0; i < 21; i++)
            {
                Console.Write(" ");
            }
            Console.Write(" │ ");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 35, 8 + 7);
            Console.Write(" └");
            for (int i = 0; i < 66; i++)
            {
                Console.Write("─");
            }
            Console.Write("┘ ");
        }
        public void SetEdges()
        {
            LeftEdge = Math.Max(TextRow[Y].Length - Console.WindowWidth / 2, 0);
            RightEdge = LeftEdge + Console.WindowWidth;
        }
        public void BackspaceMinusRow()
        {
            TextRow.Insert(Y + 1, "");
            X = 0;
            XCursorPosition = 0;
            Y++;
            YCursorPosition++;
            SetEdges();
        }
        public int DeleteCount()
        {
            int delete = 0;
            for (int i = MarkedYCharStart; i <= MarkedYCharEnd; i++)
            {
              
                if (i == MarkedYCharStart)
                {
                    if (MarkedXCharStart == 0)
                    {                        
                        delete++;
                    }
                }
                else if (i == MarkedYCharEnd)
                {
                    if (MarkedXCharEnd == TextRow[i].Length || (MarkedXCharEnd == 0 && TextRow[i].Length == 1))
                    {
                        delete++;
                    }                
                }
                else
                {
                    delete++;
                }
            }
            return delete;
        }
        public void Lol()
        {
            DrawDownLabels();
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            int topTop = BottomTop - Console.WindowHeight+1;
            int rowNumber = 1;
            int YDrew = TopTop;
            for (int i = topTop; i < Math.Min(TextRow.Count, BottomTop - 1); i++)
            {
                Console.SetCursorPosition(0, rowNumber);
                rowNumber++;
                string RowTrimmed = TextRow[i] + Pad;
                RowTrimmed = RowTrimmed.Remove(0, Math.Min(LeftEdge, RowTrimmed.Length));
                if (RowTrimmed.Length > Console.WindowWidth)
                {
                    RowTrimmed = RowTrimmed.Remove(Console.WindowWidth);
                }
                if (Marking == 0)
                {
                    Console.Write(RowTrimmed);
                }
                else if ((Y > MarkedYCharStart || Y == MarkedYCharStart && X >= MarkedXCharStart) && Marking == 1)
                {
                    if (YDrew < MarkedYCharStart || YDrew > Y)
                    {
                        Console.Write(RowTrimmed);
                        YDrew++;
                    }
                    else
                    {
                        int XDrew = LeftEdge;
                        foreach (char item in RowTrimmed)
                        {
                            if ((YDrew == MarkedYCharStart && XDrew >= MarkedXCharStart || YDrew > MarkedYCharStart) && (YDrew < Y || YDrew == Y && XDrew <= X))
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            if (XDrew > TextRow[YDrew].Length - 1)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                string space = "                                                                                                                                                      ";
                                space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                Console.Write(space);
                                break;

                            }
                            Console.Write(item);
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            XDrew++;
                        }
                        YDrew++;
                    }
                }
                else if ((Y < MarkedYCharStart || Y == MarkedYCharStart && X < MarkedXCharStart) && Marking == 1)
                {
                    if (YDrew > MarkedYCharStart || YDrew < Y)
                    {
                        Console.Write(RowTrimmed);
                        YDrew++;
                    }
                    else
                    {
                        int XDrew = LeftEdge;
                        foreach (char item in RowTrimmed)
                        {
                            if ((YDrew == MarkedYCharStart && XDrew <= MarkedXCharStart || YDrew < MarkedYCharStart) && (YDrew > Y || YDrew == Y && XDrew >= X))
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            if (XDrew > TextRow[YDrew].Length - 1)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                string space = "                                                                                                                                                      ";
                                space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                Console.Write(space);
                                break;

                            }
                            Console.Write(item);
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            XDrew++;
                        }
                        YDrew++;
                    }
                }
                else if (Marking == 2)
                {
                    if (YDrew < MarkedYCharStart || YDrew > MarkedYCharEnd)
                    {
                        Console.Write(RowTrimmed);
                        YDrew++;
                    }
                    else
                    {
                        int XDrew = LeftEdge;
                        foreach (char item in RowTrimmed)
                        {
                            if ((YDrew == MarkedYCharStart && XDrew >= MarkedXCharStart || YDrew > MarkedYCharStart) && (YDrew < MarkedYCharEnd || YDrew == MarkedYCharEnd && XDrew <= MarkedXCharEnd))
                            {
                                Console.BackgroundColor = ConsoleColor.Red;
                                Console.ForegroundColor = ConsoleColor.Black;
                            }
                            if (XDrew > TextRow[YDrew].Length - 1)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.White;
                                string space = "                                                                                                                                                      ";
                                try
                                {
                                    space = space.Remove(Console.WindowWidth - 1 - XDrew);
                                }
                                catch
                                {

                                }
                                Console.Write(space);
                                break;

                            }
                            Console.Write(item);
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.ForegroundColor = ConsoleColor.White;
                            XDrew++;
                        }
                        YDrew++;
                    }
                }
            }
            if (rowNumber < Console.WindowHeight)
            {
                while (rowNumber < Console.WindowHeight)
                {
                    if (rowNumber < Console.WindowHeight - 1)
                    {

                        string RowTrimmed = Pad.Remove(Console.WindowWidth);
                        Console.WriteLine(RowTrimmed);
                    }
                    else
                    {
                        string RowTrimmed = Pad.Remove(Console.WindowWidth);
                        Console.Write(RowTrimmed);
                    }
                    rowNumber++;
                }
            }
        }
        public void Delete()
        {
            int delete = 0;
            for (int i = MarkedYCharStart; i <= MarkedYCharEnd; i++)
            {
                if (i == MarkedYCharStart && i == MarkedYCharEnd)
                {
                    if (MarkedXCharStart == 0&& MarkedXCharEnd == TextRow[i].Length-1)
                    {
                        TextRow.RemoveAt(i);
                        delete++;
                    }
                    else
                    {
                        int add = 0;
                        if (MarkedYCharStart == MarkedYCharEnd && MarkedYCharStart == Y && X<=MarkedXCharStart)
                        {
                            add = MarkedTextRow[0].Length;
                        }
                        TextRow[i] = TextRow[i].Remove(MarkedXCharStart+add, MarkedXCharEnd - MarkedXCharStart + 1);
                        X = TextRow[Y].Length;
                    }
                    
                }
                else if (i == MarkedYCharStart)
                {
                    if (MarkedXCharStart == 0)
                    {
                        TextRow.RemoveAt(i);
                        delete++;
                    }
                    else
                    {
                        try
                        {
                            TextRow[i] = TextRow[i].Remove(MarkedXCharStart);
                        }
                        catch
                        {

                        }
                    }
                }
                else if (i == MarkedYCharEnd)
                {
                    if (MarkedXCharEnd == TextRow[i - delete].Length || (MarkedXCharEnd == 0 && TextRow[i - delete].Length == 1))
                    {
                        TextRow.RemoveAt(i - delete);
                    }
                    else
                    {
                        try
                        {
                            TextRow[i - delete] = TextRow[i - delete].Remove(0, MarkedXCharEnd + 1);
                        }
                        catch
                        {

                        }
                    }
                }
                else
                {
                    TextRow.RemoveAt(i - delete);
                    delete++;
                }
            }
        }
        public void CopyTroughReplace()
        {
            MarkedTextRow.Clear();
            MarkedTextRow.Add(Wordreplace);
            if (MarkedXCharStart == TextRow[MarkedYCharStart].Length)
            {
                TextRow[MarkedYCharStart] += MarkedTextRow[0];
            }
            else
            {
                string residue = TextRow[MarkedYCharStart].Substring(MarkedXCharStart);
                TextRow[MarkedYCharStart] = TextRow[MarkedYCharStart].Remove(MarkedXCharStart);
                TextRow[MarkedYCharStart] += MarkedTextRow[0];
                TextRow[MarkedYCharStart] += residue;
            }
            if (ReplaceAll == true)
            {
                Find();
                Delete();
                MarkedTextRow.Clear();
                CopyTroughReplace();               
            }
        }
        public void Copy()
        {
            string residue = "";
            bool dontmove = false;
            for (int i = 0; i < MarkedTextRow.Count; i++)
            {

                if (i == 0)
                {
                    if (X == TextRow[Y].Length)
                    {
                        TextRow[Y + i] += MarkedTextRow[i];
                        dontmove = true;
                    }
                    else
                    {
                        residue = TextRow[Y + i].Substring(X);
                        TextRow[Y + i] = TextRow[Y + i].Remove(X);
                        TextRow[Y + i] += MarkedTextRow[i];
                        if (0 == MarkedTextRow.Count - 1)
                        {
                            TextRow[Y + i] += residue;
                        }
                    }
                }
                else if (i == MarkedTextRow.Count - 1)
                {
                    if (dontmove == false)
                    {
                        TextRow.Insert(Y + i, MarkedTextRow[i] + residue);
                    }
                    else
                    {
                        TextRow.Insert(Y + i, MarkedTextRow[i]);
                    }
                }
                else
                {
                    TextRow.Insert(Y + i, MarkedTextRow[i]);
                }
            }
        }
        public void MoveDown()
        {
            YCursorPosition--;
            TopTop++;
            BottomTop++;
        }
        public void MoveUp()
        {
            YCursorPosition++;
            TopTop--;
            BottomTop--;
        }
        public void MoveRight()
        {
            RightEdge++;
            LeftEdge++;
            XCursorPosition--;
        }
        public void MoveLeft()
        {
            RightEdge--;
            LeftEdge--; 
            XCursorPosition++;
        }
        public string MakeLabel(int Type)
        {
            if (Type == 1)
            {
                if (Edited == true)
                    return "M";
            }
            else if (Type == 2)
            {
                if (Marking != 0)
                    return "B";
            }
            return "-";
        }
    }
}
