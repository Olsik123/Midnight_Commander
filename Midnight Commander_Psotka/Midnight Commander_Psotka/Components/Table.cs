using Midnight_Commander_Psotka.PopUps;
using Midnight_Commander_Psotka.Windows;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Midnight_Commander_Psotka.Components
{
    public class Table : IComponent
    {

        public int Height { get; set; } = 20;
        public int selected = 0;
        public int secondTime = 0;
        public bool selectedToMove;
        public int marked = 0;
        public int start = 0;
        public int end;
        public string selectedPath;
        public string selectedFile;
        public string StartingPath;
        public static string secondPath;
        public bool Denied = false;
        public static int Edited = 0;
        public bool NotAFolder = false;
        public int HalfData;
        public string Disk = "C";
        public bool choosingADisk = false;
        //public event Action<string,int> PopUp;

        //false=left true=right
        public bool side;

        public int PadHeigt;

        public static bool blocked;
        public string Path { get; set; }
        public List<Row> Data { get; set; }
        public List<Row> DataFiles { get; set; } = new List<Row>();
        public List<Row> DataUnited { get; set; } = new List<Row>();

        public Table(string path,bool Side,bool Selectedtomove)
        {
            Data = new List<Row>();
            Path = path;
            StartingPath = path;
            end = Height;
            side = Side;
            selectedToMove = Selectedtomove;
            blocked = false;

        }

        public void Unite()
        {
            DataUnited.Clear();
            foreach (Row item in Data)
            {
                DataUnited.Add(new Row(item.Name, item.Size, item.ModifyTime));
            }
            HalfData = DataUnited.Count;
            foreach (Row item in DataFiles)
            {
                DataUnited.Add(new Row(item.Name, item.Size, item.ModifyTime));
            }
        }
        public void Add(string name, string size, string modifyTime)
        {
            Data.Add(new Row(name, size, modifyTime));
        }
        public void AddFiles(string name, string size, string modifyTime)
        {
            DataFiles.Add(new Row(name, size, modifyTime));
        }
       
        public void Draw()
        {
            if (Edited !=0)
            {
                Remake();
            }
            this.DrawHeadline();
            this.DrawLabels();
            this.DrawData();
            this.DrawMidline();
            this.DrawName();
            this.DrawDownline();
            if (selectedToMove == false)
            {
                secondPath = StartingPath;
            }
        }
        public void DrawHeadline()
        {
            if (side == false)
            {
                Console.SetCursorPosition(0, 0);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, 0);
            }
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Write("╔");
            if (selectedToMove == true)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.White;
            }
            if (choosingADisk == true)
            {
                Console.Write(' ' + "CHOOSE A DISK!");
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("".PadRight(PadMaker.PadForResize+11, '═'));
            }
            else if (StartingPath.Length < 70)
            {
                Console.Write(' ' + StartingPath);
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("".PadRight(PadMaker.PadForResize - StartingPath.Length+25, '═'));
            }
            else
            {
                int i = StartingPath.Length - 70;
                string str = StartingPath;
                str = str.Remove(35, i + 1);
                str = str.Insert(34, "~");
                Console.Write(' ' + str);
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            Console.Write("╗");

            Console.WriteLine();

        }
        public void DrawLabels()
        {
            if (side == false)
            {
                Console.SetCursorPosition(0, 1);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, 1);
            }
            Console.Write("║" + " " + "Name".PadRight(PadMaker.PadForResize, ' ') + " ");
            Console.Write("│" + " " + "Size".PadRight(7, ' ') + " ");
            Console.Write("│" + " " + "ModifyTime".PadRight(11, ' ') + " ");
            Console.WriteLine("║");
        }


        public void DrawData()
        {
            this.DrawRow('║', '│', ' ');
        }
        public void DrawRow(char sep1, char sep2, char pad)
        {

            int index;
            int RowNumber = start;
            int RowsDrew = 0;
            int indexBefore = 0;
            List<Row> selectedData = new List<Row>();
            for (int i = (start); i <= Math.Min(end - 1, DataUnited.Count - 1); i++)
            {
                selectedData.Add(DataUnited[i]);
            }
            foreach (Row item in selectedData)
            {
                index = indexBefore;
                RowsDrew++;
                if (side == false)
                {
                    Console.SetCursorPosition(0, index + 2);
                }
                else
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2, index + 2);
                }
                if (start != 0 && index == selected - start && selectedToMove == true)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (start == 0 && index == selected && selectedToMove == true)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }

                if (HalfData > RowNumber && choosingADisk == false)
                {
                    if (item.Name.Length > 20)
                    {
                        int i = item.Name.Length - 20;
                        string str = item.Name;
                        str = str.Remove(8, i + 1);
                        str = str.Insert(9, "~");
                        Console.Write($"{sep1}{pad}{@"\"}{str.PadRight(PadMaker.PadForResize-1, pad)}{pad}");
                    }
                    else
                    {
                        Console.Write($"{sep1}{pad}{@"\"}{item.Name.PadRight(PadMaker.PadForResize-1, pad)}{pad}");
                    }
                    Console.Write($"{sep2}{pad}{Convert.ToString(item.Size).PadLeft(7, pad)}{pad}");
                    Console.Write($"{sep2}{pad}{Convert.ToString(item.ModifyTime).PadRight(11, pad)}{pad}");
                    Console.Write(sep1);
                }
                else
                {
                    if (choosingADisk == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    if (choosingADisk == true && HalfData <= RowNumber)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }

                    if (item.Name.Length > 20)
                    {
                        int i = item.Name.Length - 20;
                        string str = item.Name;
                        str = str.Remove(8, i + 1);
                        str = str.Insert(9, "~");
                        Console.Write($"{sep1}{pad}{str.PadRight(PadMaker.PadForResize, pad)}{pad}");
                    }
                    else
                    {
                        Console.Write($"{sep1}{pad}{item.Name.PadRight(PadMaker.PadForResize, pad)}{pad}");
                    }
                    Console.Write($"{sep2}{pad}{Convert.ToString(item.Size).PadLeft(7, pad)}{pad}");
                    Console.Write($"{sep2}{pad}{Convert.ToString(item.ModifyTime).PadRight(11, pad)}{pad}");
                    Console.Write(sep1);
                }
                index++;
                RowNumber++;
                indexBefore = index;
                if (start != 0)
                {
                    index -= (start - 1);
                }
                Console.ResetColor();
                Console.BackgroundColor = ConsoleColor.DarkBlue;
            }
            int plus = 0;
            if (RowsDrew < 20)
            {
                for (int i = RowsDrew; i < 20; i++)
                {
                    if (side == false)
                    {
                        Console.SetCursorPosition(0, RowsDrew + 2 + plus);
                    }
                    else
                    {
                        Console.SetCursorPosition(Console.WindowWidth / 2, RowsDrew + 2 + plus);
                    }                   
                    Console.Write($"{sep1}{pad}{" ".PadRight(PadMaker.PadForResize, pad)}{pad}");
                    Console.Write($"{sep2}{pad}{" ".PadLeft(7, pad)}{pad}");
                    Console.Write($"{sep2}{pad}{" ".PadRight(11, pad)}{pad}");
                    Console.Write($"{sep1}");
                    plus++;
                }

            }
        }

        public void DrawMidline()
        {
            if (side == false)
            {
                Console.SetCursorPosition(0, Height + 2);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Height + 2);
            }
            Console.Write("╠");
            for (int o = 0; o <= PadMaker.PadForResize+25; o++)
            {
                Console.Write("─");
            }
            Console.WriteLine("╣");

        }

        public void DrawName()
        {
            if (side == false)
            {
                Console.SetCursorPosition(0, Height + 3);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Height + 3);
            }
            if (DataUnited[selected].Name.Length < 70)
            {
                Console.Write("║" + " " + DataUnited[selected].Name.PadRight(PadMaker.PadForResize+25, ' '), " ");
                Console.Write('║');
            }
            else
            {
                int i = DataUnited[selected].Name.Length - 70;
                string str = DataUnited[selected].Name;
                str = str.Remove(35, i + 1);
                str = str.Insert(34, "~");
                Console.Write("║" + " " + str);
            }
        }
        public void DrawDownline()
        {
            if (side == false)
            {
                Console.SetCursorPosition(0, Height + 4);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Height + 4);
            }


            if (choosingADisk == true)
            {
                Console.Write("╚");
                Console.Write("".PadLeft(PadMaker.PadForResize+25, '═'));
                Console.Write("═");
                Console.Write("╝");
            }
            else
            {
                DriveInfo drive = new DriveInfo(StartingPath);
                Console.Write("╚");
                string str = ((drive.TotalSize / 1073741824) - (drive.AvailableFreeSpace / 1073741824) + "/" + (drive.TotalSize / 1073741824) + "GB " + (100 - (drive.AvailableFreeSpace * 100 / drive.TotalSize)) + "%");
                Console.Write(str.PadLeft(PadMaker.PadForResize+25, '═'));
                Console.Write("═");
                Console.Write("╝");
            }
            if (side == false)
            {
                Console.SetCursorPosition(0, Height + 5);
            }
            else
            {
                Console.SetCursorPosition(Console.WindowWidth / 2, Height + 5);
            }
            if (Denied == true || NotAFolder == true)
            {
                DisplayError();
            }
            else
            {
                if (side == false)
                {
                    string space = " ";
                    Console.SetCursorPosition(0, Height + 5);
                    for (int i = 0; i <= PadMaker.PadForResize+25; i++)
                    {
                        space += " ";
                    }
                    Console.WriteLine(space);
                }
                else
                {
                    string space = "";
                    Console.SetCursorPosition(Console.WindowWidth / 2, Height + 5);
                    for (int i = 0; i <= PadMaker.PadForResize + 25; i++)
                    {
                        space += " ";
                    }
                    Console.WriteLine(space);
                }

            }
            if (side == false)
            {
                DrawDownLabels();
            }


        }

        public void HandleKey(ConsoleKeyInfo info)
        {
            if (info.Key == ConsoleKey.UpArrow && selected > 0 && selectedToMove == true)
            {
                selected--;
                SelectPath();
                if (selected == (start - 1) && start != 0)
                {
                    start--;
                    end--;
                }

            }
            else if (info.Key == ConsoleKey.DownArrow && selected < DataUnited.Count - 1 && selectedToMove == true)
            {
                selected++;
                SelectPath();
                if (selected == end && end < (DataUnited.Count))
                {
                    start++;
                    end++;
                }
            }
            else if (info.Key == ConsoleKey.Tab)
            {
                if (selectedToMove == false)
                    selectedToMove = true;
                else
                    selectedToMove = false;
            }
            else if (info.Key == ConsoleKey.Enter && selectedToMove == true)
            {
                if (SelectBeyondHalf() == false)
                {
                    if (choosingADisk == true)
                    {
                        Data.Clear();
                        DataFiles.Clear();
                        string ChoosenDisk = selectedPath.Trim('.');
                        if (ChoosenDisk.Length == 4)
                            ChoosenDisk = ChoosenDisk.Remove(0, 3);
                        else
                        {
                            if (ChoosenDisk.Length > 1)
                                ChoosenDisk = ChoosenDisk.Remove(1);
                        }


                        ChoosenDisk = ChoosenDisk.Trim('.');
                        Disk = ChoosenDisk;
                        choosingADisk = false;
                        FileService service = new FileService($"{Disk}{@":\"}");
                        foreach (Row item in service.GetData())
                        {
                            this.Add(item.Name, item.Size, item.ModifyTime);
                        }
                        foreach (Row item in service.GetFiles())
                        {
                            this.AddFiles(item.Name, item.Size, item.ModifyTime);
                        }

                        selected = 0;
                        start = 0;
                        end = Height;
                        this.Unite();
                        this.SelectPathDisk();
                    }
                    else if (selected == 0 && StartingPath == $"{Disk}{@":\"}")
                    {
                        choosingADisk = true;
                        DriveInfo[] drives = DriveInfo.GetDrives();
                        List<DriveInfo> onlineDrives = new List<DriveInfo>();
                        List<DriveInfo> offlineDrives = new List<DriveInfo>();
                        foreach (DriveInfo item in drives)
                        {
                            if (item.IsReady == true)
                            {
                                onlineDrives.Add(item);
                            }
                            else
                            {
                                offlineDrives.Add(item);
                            }
                        }
                        Data.Clear();
                        DataFiles.Clear();
                        foreach (DriveInfo item in onlineDrives)
                        {
                            string onlyLetter = item.Name.Remove(1, 2);
                            this.Add(onlyLetter, "", "");
                        }
                        foreach (DriveInfo item in offlineDrives)
                        {
                            string onlyLetter = item.Name.Remove(1, 2);
                            this.AddFiles(onlyLetter, "", "NOT ACTIVED");
                        }
                        this.Unite();
                        this.SelectPathDisk();
                        this.SelectDisk();
                        selected = 0;
                        start = 0;
                        end = Height;
                    }
                    else
                    {
                        try
                        {
                            string trying = selectedPath.Remove(0, 3);
                            string trying2;
                            if (StartingPath == $"{Disk}{@":\"}")
                            {
                                trying2 = StartingPath + trying;
                            }
                            else
                            {
                                trying2 = StartingPath + @"\" + trying;
                            }
                            DirectoryInfo dir = new DirectoryInfo(trying2);
                            foreach (DirectoryInfo item in dir.GetDirectories())
                            {

                            }
                        }
                        catch
                        {
                            Denied = true;
                        }
                        if (Denied == false)
                        {
                            SwitchFolder();
                        }
                    }
                }
                else
                {
                    NotAFolder = true;
                }
            }
            else if (info.Key == ConsoleKey.F1)
            {
                Console.Clear();
                Console.WindowWidth = 150;
                Console.WindowHeight = 27;
                Console.CursorVisible = false;
            }
            else if (info.Key == ConsoleKey.F4 && ActuallyMarked() == true )
            {             
                if (SelectBeyondHalf() == true)
                {
                    string FullSelectedPath = StartingPath + @"\" + selectedFile;
                    Application.ListWindow = Application.Window;
                    Application.Window = new EditWindow(FullSelectedPath);
                }
                else
                {
                    DisplayErrorNotAFoldier();
                }
                
            }
            else if (info.Key == ConsoleKey.F5 && ActuallyMarked() == true )
            {
                if (SelectBeyondHalf() == false)
                {
                    string substrinSelectedPath = selectedPath.Remove(0, 3);
                    string FullSelectedPath = StartingPath + @"\" + substrinSelectedPath;
                    Edited = 2;
                    Application.PopUpWindow = new CopyPopUp(FullSelectedPath, secondPath);
                    //COPY FOLDER
                }
                else
                {
                    string FullSelectedPath = StartingPath + @"\" + selectedFile;
                    Edited = 2;
                    Application.PopUpWindow = new CopyFilePopUp(FullSelectedPath, secondPath);
                    //COPY FILE
                }

            }
            else if (info.Key == ConsoleKey.F6 && ActuallyMarked() == true )
            {

                if (SelectBeyondHalf() == false)
                {
                    string substrinSelectedPath = selectedPath.Remove(0, 3);
                    string FullSelectedPath = StartingPath + @"\" + substrinSelectedPath;
                    Edited = 2;
                    Application.PopUpWindow = new MovePopUp(FullSelectedPath, secondPath);
                    //MOVE FOLDER

                }
                else
                {
                    string FullSelectedPath = StartingPath + @"\" + selectedFile;
                    Edited = 2;
                    Application.PopUpWindow = new MoveFilePopUp(FullSelectedPath, secondPath);
                    //MOVE FILE
                }

            }
            else if (info.Key == ConsoleKey.F7 && selectedToMove == true && choosingADisk == false)
            {
                Edited = 2;
                Application.PopUpWindow = new MakeDirPopUp(StartingPath, "");
                //MAKEDIR              
            }
            else if (info.Key == ConsoleKey.F8 && ActuallyMarked() == true )
            {
                string substrinSelectedPath = selectedPath.Remove(0, 3);
                string FullSelectedPath = StartingPath + @"\"+ substrinSelectedPath;
                Edited = 2;
                Application.PopUpWindow = new DeletePopUp(FullSelectedPath, "");
                //RemoveDir
            }
        }
        public bool ActuallyMarked()
        {
            if (selectedToMove == true && choosingADisk == false && selected != 0)
                return true;
            return false;
        }
        public void SwitchFolder()
        {
            Data.Clear();
            DataFiles.Clear();
            if (selected == 0 && StartingPath != $"{Disk}{@":\.."}")
            {
                string OneLetterPath = StartingPath.Substring(StartingPath.Length - 2);
                if (OneLetterPath.Substring(0, 1) != @"\" && OneLetterPath.Substring(1, 1) == @"\")
                {
                    StartingPath = StartingPath.Remove(StartingPath.Length - 1, 1);
                }
                DirectoryInfo dir = new DirectoryInfo(StartingPath);
                StartingPath = dir.Parent.ToString();
            }
            if (selectedPath.Length > 2)
            {
                var result = selectedPath.Substring(selectedPath.Length - 3);
                if (result == @"\..")
                    selectedPath = selectedPath.Remove(selectedPath.Length - 2, 2);
            }
            selectedPath = selectedPath.Remove(0, 3);
            if (StartingPath.Substring(StartingPath.Length - 1) == @"\")
            {
                StartingPath += selectedPath;
            }
            else
            {
                StartingPath += @"\" + selectedPath;
            }
            FileService service = new FileService(StartingPath);
            foreach (Row item in service.GetData())
            {
                this.Add(item.Name, item.Size, item.ModifyTime);
            }
            foreach (Row item in service.GetFiles())
            {
                this.AddFiles(item.Name, item.Size, item.ModifyTime);
            }

            selected = 0;
            start = 0;
            end = Height;
            this.Unite();
            this.SelectPath();

        }
        public void SelectPath()
        {
            if (SelectBeyondHalf() == false)
            {
                selectedPath = Data[selected].Name;
                if (Path == $"{Disk}{@":\"}")
                {
                    selectedPath = Path + selectedPath;
                }
                else
                {
                    selectedPath = Path + @"\"[0] + selectedPath;
                }
            }
            else
            {
                selectedFile = DataUnited[selected].Name;
            }
        }
        public void SelectPathDisk()
        {
            Path = Disk + ":" + @"\";
            selectedPath = Data[selected].Name;
            if (Path == $"{Disk}{@":\"}")
            {
                selectedPath = Path + selectedPath;
            }
            else
            {
                selectedPath = Path + @"\"[0] + selectedPath;
            }
            selectedPath = selectedPath.Remove(selectedPath.Length - 2);
            StartingPath = selectedPath;
            StartingPath.Trim('.');
        }
        public void SelectDisk()
        {
            selectedPath = Data[selected].Name;
        }
        public bool SelectBeyondHalf()
        {
            if (HalfData > selected)
                return false;
            return true;
        } 
        public void Remake()
        {
            Data.Clear();
            DataFiles.Clear();
            FileService service = new FileService(StartingPath);
            foreach (Row item in service.GetData())
            {
                this.Add(item.Name, item.Size, item.ModifyTime);
            }
            foreach (Row item in service.GetFiles())
            {
                this.AddFiles(item.Name, item.Size, item.ModifyTime);
            }

            selected = 0;
            start = 0;
            end = Height;
            this.Unite();
            this.SelectPath();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Edited --;
        }
        public void DisplayErrorNotAFoldier()
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
            Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 11);
            Console.Write("This is not a foler!");
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
            NotAFolder = false;
            Thread.Sleep(750);
            Application.Draw();
        }
        public void DisplayError()
        {

            if (Denied == true)
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
                Console.SetCursorPosition(Console.WindowWidth / 2 - 7, 11);
                Console.Write("Access denied! ");
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


                Denied = false;
                Thread.Sleep(750);
                Application.Draw();
            }

            if (NotAFolder == true && choosingADisk == false)
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
                Console.SetCursorPosition(Console.WindowWidth / 2 - 10, 11);
                Console.Write("This is not a folder! ");
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
                NotAFolder = false;
                Thread.Sleep(750);
                Application.Draw();
            }
            if (NotAFolder == true && choosingADisk == true)
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
                Console.SetCursorPosition(Console.WindowWidth / 2 - 13, 11);
                Console.Write("This is not an active disk!");
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
                NotAFolder = false;
                Thread.Sleep(750);
                Application.Draw();
            }
            
        }
        public void DrawDownLabels()
        {
            Console.SetCursorPosition(0, Height + 6);
            Console.ResetColor();
            List<string> HelpLabels = new List<string>();
            HelpLabels.Add(" ");
            HelpLabels.Add("Help");
            HelpLabels.Add("Menu");
            HelpLabels.Add("View");
            HelpLabels.Add("Edit");
            HelpLabels.Add("Copy");
            HelpLabels.Add("RenMov");
            HelpLabels.Add("Mkdir");
            HelpLabels.Add("Delete");
            HelpLabels.Add("PullDn");
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

    }

}
