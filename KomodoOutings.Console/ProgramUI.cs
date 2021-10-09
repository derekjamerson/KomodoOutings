using KomodoOutings.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomodoOutings.Console
{
    class ProgramUI
    {
        OutingRepo repo = new OutingRepo();
        int numberOfEnums = Enum.GetValues(typeof(Outing.Event)).Length;
        public void Run()
        {
            PrintTitle();
            PrintOptions();
            while (true)
            {
                int option = ReadOption();
                if(option != -1) { PrintListOfOutings(option); }
                else { AddOuting(); PrintTitle(); PrintOptions(); }
            }
        }
        public void PrintOptions()
        {
            string addString = "(Press 'a' to add new Outing)";
            System.Console.WriteLine("  Company Outings");
            System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (addString.Length / 2)) + "}", addString));
            System.Console.WriteLine($" 1. All  (${GetCost(0)})");
            int x = 1;
            for (int i = 1; i <= numberOfEnums; i++)
            {
                ++x;
                System.Console.WriteLine($" {x}. {(Outing.Event)i}  (${GetCost(i)})");
            }
        }
        public decimal GetCost(int eventType)
        {
            decimal dCost;
            if(eventType == 0)
            {
                dCost = repo.GetTotalCost(repo.GetList());
            }
            else
            {
                dCost = repo.GetTotalCost(repo.GetOutingsByEvent(eventType));
            }
            return Math.Round(dCost, 2);
        }
        public int ReadOption()
        {
            System.Console.CursorVisible = false;
            ConsoleKeyInfo userInput;
            while (true)
            {
                userInput = System.Console.ReadKey(true);
                if(userInput.KeyChar == 'a') { return -1; }
                if (char.IsDigit(userInput.KeyChar)) 
                {
                    int keyAsDigit = int.Parse(userInput.KeyChar.ToString());
                    if (keyAsDigit <= numberOfEnums + 1)
                    {
                        return int.Parse(userInput.KeyChar.ToString()); 
                    }
                }
            }
        }
        public void PrintListOfOutings(int input)
        {
            List<Outing> _list = new List<Outing>();
            int type = input - 1;
            PrintTitle();
            PrintOptions();
            System.Console.WriteLine("\n");
            if(type == 0) { _list = repo.GetList(); }
            else if(type <= numberOfEnums) { _list = repo.GetOutingsByEvent(type); }
            
            foreach(Outing outing in _list)
            {
                PrintOuting(outing);
            }
        }
        public void PrintOuting(Outing outing)
        {
            System.Console.WriteLine($" Date: {outing.EventDate.Date.ToString("d")} Event: {outing.EventType}\n Attendees: {outing.NumberOfPeople}   Cost: ${Math.Round(outing.TotalCost, 2)}   $/person: ${Math.Round(outing.CostPerPerson, 2)}\n\n");
        }
        public void PrintTitle()
        {
            System.Console.Clear();

            string title = "Komodo Outing Application";
            System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (title.Length / 2)) + "}", title));
            System.Console.WriteLine("\n\n");
        }
        public void AddOuting()
        {
            PrintTitle();
            System.Console.WriteLine(" Add New Company Outing\n");
            DateTime date = AskDate();
            System.Console.WriteLine("");
            var eventType = AskEvent();
            System.Console.WriteLine("");
            int numberOfPeople = AskNumberOfPeople();
            System.Console.WriteLine("");
            decimal totalCost = AskTotalCost();

            Outing outing = new Outing(eventType, numberOfPeople, date, totalCost);
            PrintTitle();
            PrintOuting(outing);
            System.Console.WriteLine("\n");
            string message = "Add this Company Outing?";
            System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (message.Length/ 2)) + "}", message));
            if (AskYesNo())
            {
                repo.AddToList(outing);
                System.Console.WriteLine("\n\n");
                string added = "Company Outing successfully added.";
                System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (added.Length / 2)) + "}", added));
                System.Threading.Thread.Sleep(3000);
            }
            else
            {
                PrintTitle();
                System.Console.WriteLine("\n\n");
                string notAdded = "Company Outing was NOT added.";
                System.Console.WriteLine(string.Format("{0," + ((System.Console.WindowWidth / 2) + (notAdded.Length / 2)) + "}", notAdded));
                System.Threading.Thread.Sleep(3000);
            }
        }
        public DateTime AskDate()
        {
            System.Console.CursorVisible = true;
            System.Console.Write(" Date: ");
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            while (true)
            {
                System.Console.BackgroundColor = ConsoleColor.Gray;
                System.Console.ForegroundColor = ConsoleColor.Black;
                System.Console.Write("dd/MM/yyyy");
                System.Console.ResetColor();
                System.Console.SetCursorPosition(toLeft, toTop);
                string response = System.Console.ReadLine();
                DateTime date;
                if(DateTime.TryParseExact(response, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    System.Console.SetCursorPosition(toLeft, toTop);
                    System.Console.WriteLine(new string(' ', System.Console.WindowWidth));
                    System.Console.SetCursorPosition(toLeft, toTop);
                }
            }
        }
        public Outing.Event AskEvent()
        {
            System.Console.CursorVisible = false;
            System.Console.Write(" Event Type: ");
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            PrintEventList(0);
            int selection = 1;
            while (true)
            {
                System.Console.SetCursorPosition(toLeft, toTop);
                PrintEventList(selection);
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.LeftArrow:
                        if(selection > 1)
                        {
                            selection--;
                            break;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if(selection < numberOfEnums)
                        {
                            selection++;
                            break;
                        }
                        break;
                    case ConsoleKey.Enter:
                        System.Console.WriteLine(new string(' ', System.Console.WindowWidth));
                        System.Console.SetCursorPosition(toLeft, toTop);
                        System.Console.WriteLine(Enum.GetName(typeof(Outing.Event), selection));
                        return (Outing.Event)selection;
                }
            }
        }
        public int AskNumberOfPeople()
        {
            System.Console.CursorVisible = true;
            System.Console.Write(" # of attendees: ");
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            while (true)
            {
                string response = System.Console.ReadLine();
                int attendees = default;
                if (int.TryParse(response, out attendees))
                {
                    if(attendees > 0) { return attendees; }
                }
                else
                {
                    System.Console.SetCursorPosition(toLeft, toTop);
                    System.Console.WriteLine(new string(' ', System.Console.WindowWidth));
                    System.Console.SetCursorPosition(toLeft, toTop);
                }
            }
        }
        public decimal AskTotalCost()
        {
            System.Console.CursorVisible = true;
            System.Console.Write(" TotalCost: $ ");
            int toLeft = System.Console.CursorLeft;
            int toTop = System.Console.CursorTop;
            while (true)
            {
                string response = System.Console.ReadLine();
                decimal cost = default;
                if (decimal.TryParse(response, out cost))
                {
                    if (cost > 0) { return cost; }
                }
                else
                {
                    System.Console.SetCursorPosition(toLeft, toTop);
                    System.Console.WriteLine(new string(' ', System.Console.WindowWidth));
                    System.Console.SetCursorPosition(toLeft, toTop);
                }
            }
        }
        public bool AskYesNo()
        {
            System.Console.CursorVisible = false;
            int toTop = System.Console.CursorTop;
            bool selected = false;
            bool yesOrNo = default;
            string yString = "YES";
            string nString = "NO";
            while (true)
            {
                switch (System.Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Y:
                    case ConsoleKey.D1:
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.T:
                        selected = true;
                        yesOrNo = true;
                        System.Console.ResetColor();
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(new string(' ', System.Console.WindowWidth));
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (yString.Length / 2)) + "}", ""));
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.WriteLine(yString);
                        break;
                    case ConsoleKey.N:
                    case ConsoleKey.D2:
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.F:
                        selected = true;
                        yesOrNo = false;
                        System.Console.ResetColor();
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(new string(' ', System.Console.WindowWidth));
                        System.Console.SetCursorPosition(0, toTop);
                        System.Console.Write(String.Format("{0," + ((System.Console.WindowWidth / 2) + (nString.Length / 2)) + "}", ""));
                        System.Console.BackgroundColor = ConsoleColor.White;
                        System.Console.ForegroundColor = ConsoleColor.Black;
                        System.Console.WriteLine(nString);
                        break;
                    default:
                        if (selected) { System.Console.ResetColor(); return yesOrNo; }
                        break;
                }
            }
        }
        public void PrintEventList(int x)
        {
            for (int i = 1; i <= numberOfEnums; i++)
            {
                if(x == i)
                {
                    System.Console.BackgroundColor = ConsoleColor.White;
                    System.Console.ForegroundColor = ConsoleColor.Black;
                }
                System.Console.Write(Enum.GetName(typeof(Outing.Event), i));
                System.Console.ResetColor();
                System.Console.Write("   ");
            }
        }
    }
}
