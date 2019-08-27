using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class Program
    {
        static void Main(string[] args)
        {

            Petri p = new Petri();
            bool exit = false;

            while (exit == false)
            {
                Console.WriteLine("=====");
                Console.WriteLine("POSSIBLE ACTIONS: ");
                Console.WriteLine("CreateSlot: Creates a new slot; Alias: cs");
                Console.WriteLine("ListSlots: Lists all existing slots; Alias: ls");
                Console.WriteLine("CreateTransition: Creates a new transition; Alias: ct");
                Console.WriteLine("ListTransitions: Lists all existing transitions; Alias: lt");
                Console.WriteLine("Exit: Exits the program; Alias: x");
                Console.WriteLine("=====");

                Console.Write("Command: ");
                string input;
                input = Console.ReadLine();
                input = input.ToLower();
                Console.Clear();

                
                if (input == "createslot"|| input == "cs")
                    p.CreateSlotLoop();
                else if (input == "listslots"|| input == "ls")
                    p.ListSlots();
                if (input == "createtransition" || input == "ct")
                    p.CreateTransitionLoop();
                else if (input == "listtransitions" || input == "lt")
                    p.ListTransitions();
                else if (input == "exit" || input == "x")
                    exit = true;
                
            }
        }


    }
}
 
