using System;
using System.Collections.Generic;
using System.Text;

namespace Midnight_Commander_Psotka.PopUps
{
    public class EditorReplace : PopUp
    {
        public int Replacing { get; set; }
        public string Wordfind { get; set; }
        public string Wordreplace { get; set; }
        public int WordFindMarked { get; set; }
        public int WordReplaceMarked { get; set; }
        public bool WordFindReplaceChoosen { get; set; }
        public int XAlreadySearched { get; set; }
        public int YAlreadySearched { get; set; }
        public bool ReplaceAll { get; set; }
        public event Action CopyTroughReplace;
        public event Action Find;
        public event Action Delete;
        public event Action GetMarkedTextRow;
        public event Action GetMarkedYCharStart;
        public event Action GetTopTop;
        public event Action ReturnToNormal;
        public event Action MarkedTextRowClean;
        public event Action DrawReplacing1;
        public event Action DrawReplacing2;

        public EditorReplace()
        {

        }
        
        public override void Draw()
        {           
            if (Replacing == 1)
            {
                DrawReplacing1();
            }
            else
            {
               DrawReplacing2();
             }
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if ((Char.IsLetterOrDigit(info.KeyChar) || Char.IsPunctuation(info.KeyChar) || Char.IsSymbol(info.KeyChar) || (info.KeyChar == ' ')) && Replacing == 1)
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
                WordFindMarked++;
            }
            else if (info.Key == ConsoleKey.Enter && WordFindMarked == 0 && Replacing == 1)
            {
                Replacing = 2;
                Find();
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
                MarkedTextRowClean();
                CopyTroughReplace();
                Find();
            }
            else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 1 && Replacing == 2)
            {
                ReplaceAll = true;
                Delete();
                MarkedTextRowClean();
                CopyTroughReplace();
                Find();
            }
            else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 2 && Replacing == 2)
            {
                Find();
            }
            else if (info.Key == ConsoleKey.Enter && WordReplaceMarked == 3 && Replacing == 2)
            {
                ReturnToNormal();
            }
        }
    }
}
