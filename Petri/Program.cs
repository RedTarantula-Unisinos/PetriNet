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
                Console.Clear();
                if (p.listStr != "")
                {
                    Console.Write(p.listStr + "\n");
                }
                Console.WriteLine("=====");
                Console.WriteLine("POSSIBLE ACTIONS: ");
                Console.WriteLine("Run: Runs the net once; Alias: r");
                Console.WriteLine("RunAll: Runs until transitions are all disabled; Alias: ra\n");
                Console.WriteLine("CreateSlot: Creates a new slot; Alias: cs ");
                Console.WriteLine("ListSlots: Lists all existing slots; Alias: ls \n");
                Console.WriteLine("AddTokens: Adds tokens to a slot; Alias: at \n");
                Console.WriteLine("CreateTransition: Creates a new transition; Alias: ct");
                Console.WriteLine("ListTransitions: Lists all existing transitions; Alias: lt \n");
                Console.WriteLine("ConnectSlotToTransition: Connects slot to a transition; Alias: cst");
                Console.WriteLine("ConnectTransitionToSlot: Connects a transition to a slot; Alias: cts");
                Console.WriteLine("ListConnections: Lists all existing connections; Alias: lc \n");
                Console.WriteLine("ListAll: Lists everything; Alias: l \n");
                Console.WriteLine("Exit: Exits the program; Alias: x");
                Console.WriteLine("=====");
                Console.WriteLine("HISTORY\n");
                for (int i = 0 ; i < 5 && i < p.log.ToArray().Length ; i++)
                {
                    Console.WriteLine(p.log[p.log.ToArray().Length-1-i]);
                    
                }
                Console.WriteLine("=====");

                p.listStr = "";

                Console.Write("Command: ");
                string input;
                input = Console.ReadLine();
                input = input.ToLower();


                if (input == "run" || input == "r")
                    p.Run();
                else if (input == "runall" || input == "ra")
                    p.RunAll();

                else if (input == "createslot" || input == "cs")
                    p.CreateSlotLoop();
                else if (input == "listslots" || input == "ls")
                    p.ListSlots();

                else if (input == "addtokens" || input == "at")
                    p.AddTokenManually();

                else if (input == "createtransition" || input == "ct")
                    p.CreateTransitionLoop();
                else if (input == "listtransitions" || input == "lt")
                    p.ListTransitions();

                else if (input == "connectlottotransition" || input == "cst")
                    p.CreateConnectionSTLoop();
                else if (input == "connecttransitiontoslot" || input == "cts")
                    p.CreateConnectionTSLoop();
                else if (input == "listconnections" || input == "lc")
                    p.ListConnections();

                else if (input == "listall" || input == "l")
                {
                    p.ListSlots();
                    p.ListTransitions();
                    p.ListConnections();
                }

                else if (input == "exit" || input == "x")
                    exit = true;

                else
                    p.listStr = "(!)INVALID COMMAND(!)";
                
            }
        }


    }
}
 
