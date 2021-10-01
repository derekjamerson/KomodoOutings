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
                if(option != -1) { PrintOutings(option); }
                else { AddOuting(); }
            }
        }
        public void PrintOptions()
        {
            string addString = "(Press 'a' to add new Outing)";
            System.Console.WriteLine("    Outings");
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
        public void PrintOutings(int input)
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
                System.Console.WriteLine($" Date: {outing.Date} Event: {outing.EventType}\n Attendees: {outing.NumberOfPeople}   Cost: ${outing.TotalCost}   $/person: ${outing.CostPerPerson}\n\n");
            }
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

        }
    }
}
