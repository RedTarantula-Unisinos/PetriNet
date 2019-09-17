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
            SampleNet sn = new SampleNet();
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
                Console.WriteLine("AddTokens: Adds tokens to a slot; Alias: at");
                Console.WriteLine("RemoveTokens: Removes tokens to a slot; Alias: rt \n");
                Console.WriteLine("ClearTokens: Removes all tokens to a slot; Alias: crt \n");
                Console.WriteLine("CreateTransition: Creates a new transition; Alias: ct");
                Console.WriteLine("ListTransitions: Lists all existing transitions; Alias: lt \n");
                Console.WriteLine("ConnectSlotToTransition: Connects slot to a transition; Alias: cst");
                Console.WriteLine("ConnectTransitionToSlot: Connects a transition to a slot; Alias: cts");
                Console.WriteLine("ListConnections: Lists all existing connections; Alias: lc \n");
                Console.WriteLine("ListAll: Lists everything; Alias: la ");
                Console.WriteLine("Logs: Lists all logs; Alias: lg \n");
                
                Console.WriteLine("GrauA: Builds Grau A's sample; Alias: ga");
                Console.WriteLine("TokensGA: Gives 20 tokens to L1, L2, L3 and L4; Alias: tga");
                Console.WriteLine("Nuke: Clears everything; Alias: nk");
                Console.WriteLine("Exit: Exits the program; Alias: x");
                Console.WriteLine("=====");
                Console.WriteLine("HISTORY\n");
                if (!p.allLogs)
                {

                    for (int i = 0 ; i < 5 && i < p.log.ToArray().Length ; i++)
                    {
                        Console.WriteLine(p.log[p.log.ToArray().Length - 1 - i]);

                    }
                }
                else
                {
                    foreach (string log in p.log)
                    {
                        Console.WriteLine(log);
                    }
                    p.allLogs = false;
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
                else if (input == "removetokens" || input == "rt")
                    p.AddTokenManually();
                else if (input == "cleartokens" || input == "crt")
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

                else if (input == "listall" || input == "la")
                {
                    p.ListSlots();
                    p.ListTransitions();
                    p.ListConnections();
                }

                else if (input == "logs" || input == "lg")
                {
                    p.allLogs = true;
                }
                
                else if (input == "graua" || input == "ga")
                    sn.BuildGA(p);
                else if (input == "tokensta" || input == "tga")
                    sn.GATokens(p);

                else if (input == "nuke" || input == "nk")
                {
                    p = new Petri();
                    sn = new SampleNet();
                }

                else if (input == "exit" || input == "x")
                    exit = true;


                else
                    p.listStr = "(!)INVALID COMMAND(!)";
                
            }
        }


    }
}
 
