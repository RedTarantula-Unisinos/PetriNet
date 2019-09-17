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
        int logID;
        public List<string> actionsLog;
        public string listStr;
        private List<PetriSlot> slotsList;
        private List<PetriConnection> connectionsList;
        private List<PetriTransition> transitionsList;
        public bool allLogs = false;

        public Petri() // Constructor
        {
            slotsList = new List<PetriSlot>();
            connectionsList = new List<PetriConnection>();
            transitionsList = new List<PetriTransition>();
            log = new List<string>();
            listStr = "";
            logID = 0;
        }
        
        public void UpdateLogs(string _log)
        {
            log.Add("["+logID+"] " + _log);
            logID++;
        }

        #region RUNNING
        public bool Run()
        {
            bool somethingHappened = false;
            foreach (PetriTransition transition in transitionsList)
            {
                bool enabled = true;
                foreach (PetriConnection inputConnection in transition.inputs)
                {
                    PetriSlot slot = slotsList[inputConnection.s];
                    if ((slot.tokens < inputConnection.weight && inputConnection.type != ConnectionType.Inhibitor)||(slot.tokens > 0 && inputConnection.type == ConnectionType.Inhibitor))
                    {
                        enabled = false;
                        break;
                    }
                }

                if (enabled)
                {
                    foreach (PetriConnection inputConnection in transition.inputs)
                    {
                        
                        PetriSlot slot = slotsList[inputConnection.s];
                        if (inputConnection.type != ConnectionType.Inhibitor)
                        {
                            RemoveTokensFromSlot(inputConnection.s,inputConnection.weight);
                        }
                        else if (inputConnection.type == ConnectionType.Reset)
                        {
                            RemoveTokensFromSlot(inputConnection.s,slotsList[inputConnection.s].tokens);
                        }
                        UpdateLogs("Taking " + inputConnection.weight + " tokens from slot [" + slot.name + "(" + slot.id + ")]");
                        somethingHappened = true;
                    }

                    foreach (PetriConnection outputConnection in transition.outputs)
                    {
                        PetriSlot slot = slotsList[outputConnection.s];
                        AddTokensToSlot(outputConnection.s,outputConnection.weight);
                        UpdateLogs("Gave " + outputConnection.weight + " tokens to slot [" + slot.name + "(" + slot.id + ")]");
                        somethingHappened = true;
                    }
                }
            }



            if(!somethingHappened)
            {
                UpdateLogs("No available transitions");
            }
            return somethingHappened;
        }

        public void RunAll()
        {
            bool isRunning = true;
            int timesRun = 0;
            while (isRunning && timesRun < 100)
            {
                isRunning = Run();
                timesRun++;
            }
            UpdateLogs("Finished - Ran " + timesRun + " times");
        }
#endregion

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

        #region CREATING MANUALLY
        public void CreateConnectionSTLoop()
        {
            int slotID = 0;
            int transID = 0;
            int weight = 1;
            string inputType;
            ConnectionType type = ConnectionType.Normal;
            
            bool testing = false;
            while (testing == false)
            {
                Console.WriteLine("=====");
                ListSlots();
                Console.WriteLine("=====");

                Console.Write("Slot's ID: ");
                Int32.TryParse(Console.ReadLine(),out slotID);

                if (slotID < 0 || slotID >= slotsList.ToArray().Length)
                {
                    Console.WriteLine("Failed");
                }
                else
                {
                    testing = true;
                }

                
            }

            testing = false;

            while (testing == false)
            {

                Console.WriteLine("=====");
                ListTransitions();
                Console.WriteLine("=====");

                Console.Write("Transition's ID: ");
                Int32.TryParse(Console.ReadLine(),out transID);
                if (transID < 0 || transID >= transitionsList.ToArray().Length)
                {
                    Console.WriteLine("Failed");
                }
                else
                {
                    testing = true;
                }

            }
            testing = false;
            
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
            

            while (!testing)
            {
                Console.Write("Connection Type: ");
                inputType = Console.ReadLine().ToLower();

                if (inputType == "normal" || inputType == "n")
                {
                    type = ConnectionType.Normal;
                    testing = true;
                }
                else if (inputType == "inhibitor" || inputType == "i")
                {
                    type = ConnectionType.Inhibitor;
                    testing = true;
                }
                else if (inputType == "reset" || inputType == "r")
                {
                    type = ConnectionType.Reset;
                    testing = true;
                }
                else
                {
                    Console.WriteLine("Incorrect type");
                }
            }

            if (type == ConnectionType.Normal)
            {
                Console.Write("Connection's Weight: ");
                Int32.TryParse(Console.ReadLine(),out weight);
            }
            else
            {
                weight = 0;
            }

           

            PetriConnection c = new PetriConnection(connectionsList.ToArray().Length,slotID,transID,false,weight,type);
            transitionsList[transID].inputs.Add(c);
            slotsList[slotID].outputs.Add(c);
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
            slotsList[slotID].inputs.Add(c);
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
       
        #region CREATING AUTOMATICALLY
        public void CreateConnectionST(int slot, int transition, int w = 1, ConnectionType ctype = ConnectionType.Normal)
        {
            int slotID = slot;
            int transID = transition;
            int weight = w;
            ConnectionType type = ctype;
            

            PetriConnection c = new PetriConnection(connectionsList.ToArray().Length,slotID,transID,false,weight,type);
            transitionsList[transID].inputs.Add(c);
            slotsList[slotID].outputs.Add(c);
            connectionsList.Add(c);

            
            PetriTransition trans = transitionsList[transID];
            PetriSlot petriSlot = slotsList[slotID];
            string l = "Created a connection from the slot [" + petriSlot.name + "(" + petriSlot.id + ")" + "] to the transition [" + trans.name + "(" + trans.id + ")]" + "Id: " + c.id;;
            UpdateLogs(l);
            listStr = "";
        }
        public void CreateConnectionTS(int slot, int transition, int w = 1)
        {
            int slotID = slot;
            int transID = transition;
            int weight = w;
            ConnectionType type = ConnectionType.Normal;

            PetriConnection c = new PetriConnection(connectionsList.ToArray().Length,slotID,transID,true,weight,type);
            transitionsList[transID].outputs.Add(c);
            slotsList[slotID].inputs.Add(c);
            connectionsList.Add(c);

            PetriTransition trans = transitionsList[transID];
            PetriSlot pslot = slotsList[slotID];
            string l = "Created a connection from the transition [" + trans.name + "(" + trans.id + ")" + "] to the slot [" + pslot.name + "(" + pslot.id + ")]" + "Id: " + c.id;
            UpdateLogs(l);
            listStr = "";

        }
        public void CreateSlot(string slotName,int slotTokens)
        {
            string name = slotName;


            PetriSlot s = new PetriSlot(slotsList.ToArray().Length,name);
            slotsList.Add(s);


            int id = slotsList.ToArray().Length - 1;
            int tokens = slotTokens;
            if (tokens >= 0)
            {
                AddTokensToSlot(id,tokens);
            }



            string l = "Created a slot. Id: " + id + "; Name: " + name + "; Tokens: " + tokens + ";";
            UpdateLogs(l);
        }
        public void CreateTransition(string transName)
        {
            string name = transName;
            
            PetriTransition t = new PetriTransition(transitionsList.ToArray().Length,name);
            transitionsList.Add(t);
            
            string l = "Created a transition. Id: " + (transitionsList.ToArray().Length-1) + "; Name: " + name + ";";
            UpdateLogs(l);
        }
        #endregion

        #region TOKENS

        public void AddTokenManually()
        {

            int slotID = 0;
            int amount = 0;

            Console.WriteLine("=====");
            ListSlots();
            Console.WriteLine("=====");
            
            Console.Write("Slot's ID: ");
            if (!Int32.TryParse(Console.ReadLine(),out slotID))
                return;

            Console.Write("Tokens to add: ");
            if (!Int32.TryParse(Console.ReadLine(),out amount))
                return;

            AddTokensToSlot(slotID,amount);

            string l = "Gave " + amount + " tokens to the slot [" + slotsList[slotID].name + "(" + slotID + ")]";
            UpdateLogs(l);
            listStr = "";
        }

        public void RemoveTokenManually()
        {

            int slotID = 0;
            int amount = 0;

            Console.WriteLine("=====");
            ListSlots();
            Console.WriteLine("=====");
            
            Console.Write("Slot's ID: ");
            if (!Int32.TryParse(Console.ReadLine(),out slotID))
                return;

            Console.Write("Tokens to remove: ");
            if (!Int32.TryParse(Console.ReadLine(),out amount))
                return;

            if(amount > slotsList[slotID].tokens)
            {
                amount = slotsList[slotID].tokens;
            }

            RemoveTokensFromSlot(slotID,amount);

            string l = "Took " + amount + " tokens from the slot [" + slotsList[slotID].name + "(" + slotID + ")]";
            UpdateLogs(l);
            listStr = "";
        }
        public void ClearTokensManually()
        {
            int slotID = 0;

            Console.WriteLine("=====");
            ListSlots();
            Console.WriteLine("=====");
            
            Console.Write("Slot's ID: ");
            if (!Int32.TryParse(Console.ReadLine(),out slotID))
                return;

            int amount = slotsList[slotID].tokens;

            RemoveTokensFromSlot(slotID,amount);

            string l = "Took " + amount + " tokens from the slot [" + slotsList[slotID].name + "(" + slotID + ")]";
            UpdateLogs(l);
            listStr = "";
        }

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
