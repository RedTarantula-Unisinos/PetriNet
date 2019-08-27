using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    [System.Serializable]
    class Petri
    {

        public List<string> log;
        public string listStr;
        private List<PetriSlot> slotsList;
        private List<PetriConnection> connectionsList;
        private List<PetriTransition> transitionsList;

        public Petri() // Constructor
        {
            slotsList = new List<PetriSlot>();
            connectionsList = new List<PetriConnection>();
            transitionsList = new List<PetriTransition>();
            log = new List<string>();
            listStr = "";
        }
        
        public void UpdateLogs(string _log)
        {
            if (log.ToArray().Length == 3)
            {
                log.RemoveAt(0);
            }
            log.Add(_log);
        }

        #region LISTING
        public void ListConnections()
        {
            foreach (PetriConnection c in connectionsList)
            {
                PetriSlot slot = slotsList[c.s];
                PetriTransition trans = transitionsList[c.t];

                if (c.output)
                {
                    listStr += "Connection " + c.id + ": " + "From the transition [" + trans.name + "(" + trans.id + ")" + "] to the slot [" + slot.name + "(" + slot.id + ")]; " + "Weight: " + c.weight + "\n";
                    Console.WriteLine("Connection " + c.id + ": " + "From the transition [" + trans.name + "(" + trans.id + ")" + "] to the slot [" + slot.name + "(" + slot.id + ")]; " + "Weight: " + c.weight + "\n");
                }
                else
                {
                    listStr += "Connection " + c.id + ": " + "From the slot [" + slot.name + "(" + slot.id + ")" + "] to the transition [" + trans.name + "(" + trans.id + ")]; " + "Weight: " + c.weight + "\n";
                    Console.WriteLine("Connection " + c.id + ": " + "From the slot [" + slot.name + "(" + slot.id + ")" + "] to the transition [" + trans.name + "(" + trans.id + ")]; " + "Weight: " + c.weight + "\n");
                }
            }
        }
        public void ListSlots()
        {
            Console.WriteLine("LIST OF SLOTS:");
            foreach (PetriSlot s in slotsList)
            {
                listStr += "Slot " + s.id + ": " + s.name + " - Contains " + s.tokens + " tokens;" + "\n";
                Console.WriteLine("Slot " + s.id + ": " + s.name + " - Contains " + s.tokens + " tokens;");
            }
        }
        public void ListTransitions()
        {
            Console.WriteLine("LIST OF TRANSITIONS:");
            foreach (PetriTransition t in transitionsList)
            {
                listStr += "Transition " + t.id + " - " + t.name + "\n";
                Console.WriteLine("Transition " + t.id + ": " + t.name);
            }
        }
        #endregion

        #region CREATING
        public void CreateConnectionSTLoop()
        {
            int slotID = 0;
            int transID = 0;
            int weight = 1;
            string inputType;
            ConnectionType type = ConnectionType.Normal;

            Console.WriteLine("=====");
            ListSlots();
            Console.WriteLine("=====");

            Console.Write("Slot's ID: ");
            Int32.TryParse(Console.ReadLine(),out slotID);

            if (slotID < 0)
            {
                Console.WriteLine("Failed");
                return;
            }


            Console.WriteLine("=====");
            ListTransitions();
            Console.WriteLine("=====");

            Console.Write("Transition's ID: ");
            Int32.TryParse(Console.ReadLine(),out transID);

            if (transID < 0)
            {
                Console.WriteLine("Failed");
                return;
            }

            Console.Write("Connection's Weight: ");
            Int32.TryParse(Console.ReadLine(),out weight);

            if (weight < 0)
            {
                Console.WriteLine("Forcing value of 1 to the connection");
                weight = 1;
                return;
            }

            Console.WriteLine("=====");
            Console.WriteLine("Possible Types: ");
            Console.WriteLine("- Normal");
            Console.WriteLine("- Inhibitor");
            Console.WriteLine("- Reset");
            Console.WriteLine("=====");

            bool acceptable = false;

            while (!acceptable)
            {
                Console.Write("Connection Type: ");
                inputType = Console.ReadLine().ToLower();

                if (inputType == "normal" || inputType == "n")
                {
                    type = ConnectionType.Normal;
                    acceptable = true;
                }
                else if (inputType == "inhibitor" || inputType == "i")
                {
                    type = ConnectionType.Inhibitor;
                    acceptable = true;
                }
                else if (inputType == "reset" || inputType == "r")
                {
                    type = ConnectionType.Reset;
                    acceptable = true;
                }
                else
                {
                    Console.WriteLine("Incorrect type");
                }
            }

            PetriConnection c = new PetriConnection(connectionsList.ToArray().Length,slotID,transID,false,weight,type);
            transitionsList[transID].inputs.Add(c);
            connectionsList.Add(c);

            
            PetriTransition trans = transitionsList[transID];
            PetriSlot slot = slotsList[slotID];
            string l = "Created a connection from the slot [" + slot.name + "(" + slot.id + ")" + "] to the transition [" + trans.name + "(" + trans.id + ")]" + "Id: " + c.id;;
            UpdateLogs(l);
            listStr = "";
        }
        public void CreateConnectionTSLoop()
        {
            int slotID = 0;
            int transID = 0;
            int weight = 1;
            ConnectionType type = ConnectionType.Normal;

            Console.WriteLine("=====");
            ListTransitions();
            Console.WriteLine("=====");

            Console.Write("Transition's ID: ");
            Int32.TryParse(Console.ReadLine(),out transID);

            if (transID < 0)
            {
                Console.WriteLine("Failed");
                return;
            }

            Console.WriteLine("=====");
            ListSlots();
            Console.WriteLine("=====");

            Console.Write("Slot's ID: ");
            Int32.TryParse(Console.ReadLine(),out slotID);

            if (slotID < 0)
            {
                Console.WriteLine("Failed");
                return;
            }

            Console.Write("Connection's Weight: ");
            Int32.TryParse(Console.ReadLine(),out weight);

            if (weight < 0)
            {
                Console.WriteLine("Forcing value of 1 to the connection");
                weight = 1;
                return;
            }



            PetriConnection c = new PetriConnection(connectionsList.ToArray().Length,slotID,transID,true,weight,type);
            transitionsList[transID].outputs.Add(c);
            connectionsList.Add(c);

            PetriTransition trans = transitionsList[transID];
            PetriSlot slot = slotsList[slotID];
            string l = "Created a connection from the transition [" + trans.name + "(" + trans.id + ")" + "] to the slot [" + slot.name + "(" + slot.id + ")]" + "Id: " + c.id;
            UpdateLogs(l);
            listStr = "";

        }
        public void CreateSlotLoop()
        {
            Console.Write("Slot's name: ");
            string name = Console.ReadLine();


            PetriSlot s = new PetriSlot(slotsList.ToArray().Length,name);
            slotsList.Add(s);
            Console.WriteLine("Created Slot: " + s.name + " with ID " + s.id);


            int id = slotsList.ToArray().Length - 1;
            Console.Write("How many tokens?");
            int tokens = -1;
            Int32.TryParse(Console.ReadLine(),out tokens);
            if (tokens >= 0)
            {
                AddTokensToSlot(id,tokens);
            }



            string l = "Created a slot. Id: " + id + "; Name: " + name + "; Tokens: " + tokens + ";";
            UpdateLogs(l);
        }
        public void CreateTransitionLoop()
        {
            Console.Write("Transition's name: ");
            string name = Console.ReadLine();
            
            PetriTransition t = new PetriTransition(transitionsList.ToArray().Length,name);
            transitionsList.Add(t);
            
            string l = "Created a transition. Id: " + (transitionsList.ToArray().Length-1) + "; Name: " + name + ";";
            UpdateLogs(l);
        }
        #endregion

        #region TOKENS
        public void AddTokensToSlot(int slotID, int tokensAmount)
        {
            slotsList[slotID].AddTokens(tokensAmount);
        }
        public void RemoveTokensFromSlot(int slotID, int tokensAmount)
        {
            slotsList[slotID].RemoveTokens(tokensAmount);
        }
        #endregion
    }
}
