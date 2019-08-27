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

        
        private List<PetriSlot> slotsList;
        private List<PetriConnection> connectionsList;
        private List<PetriTransition> transitionsList;

        public Petri()
        {
            slotsList = new List<PetriSlot>();
            connectionsList = new List<PetriConnection>();
            transitionsList = new List<PetriTransition>();

        }
        

        public void CreateSlotLoop()
        {
            Console.WriteLine("Pick a name for the slot");
            string name = Console.ReadLine();
            CreateSlot(name);
            int id = slotsList.ToArray().Length - 1;
            Console.WriteLine("How many tokens?");
            int tokens = -1;
            Int32.TryParse(Console.ReadLine(),out tokens);
            if(tokens >=0)
            {
                Console.WriteLine("Giving " + tokens + " token(s) to the slot");
                AddTokensToSlot(id,tokens);
            }
            else
            {
                Console.WriteLine("Failed reading tokens amount, giving 0 tokens to the slot");
            }
        }
        
        internal void CreateTransitionLoop()
        {
            Console.WriteLine("Pick a name for the transition");
            string name = Console.ReadLine();
            CreateTransition(name);
        }

        
        
        public void CreateSlot(string slotName)
        {
            PetriSlot s = new PetriSlot(slotsList.ToArray().Length,slotName);
            slotsList.Add(s);
            Console.WriteLine("Created Slot: " + s.name + " with ID " + s.id);
        }

        public void CreateTransition(string transName)
        {
            PetriTransition t = new PetriTransition(transitionsList.ToArray().Length,transName);
            transitionsList.Add(t);
            Console.WriteLine("Created Transition: " + t.name + " with ID " + t.id);
        }

        
        public void ListSlots()
        {
            Console.WriteLine("LIST OF SLOTS:");
            foreach (PetriSlot s in slotsList)
            {
                Console.WriteLine(s.id + " - " + s.name + " - Contains " + s.tokens + " tokens;");
            }
        }

        public void ListTransitions()
        {
            Console.WriteLine("LIST OF TRANSITIONS:");
            foreach (PetriTransition t in transitionsList)
            {
                Console.WriteLine(t.id + " - " + t.name);
            }
        }

        public void ConnectSlotTrans(int slotID, int transitionID,int weight = 1, ConnectionType type= ConnectionType.Normal)
        {
            PetriConnection c = new PetriConnection(slotsList[slotID],transitionsList[transitionID],false,weight,type);
            transitionsList[transitionID].inputs.Add(c);
            connectionsList.Add(c);

        }

        public void ConnectTransSlot(int transitionID, int slotID,int weight = 1, ConnectionType type= ConnectionType.Normal)
        {
            PetriConnection c = new PetriConnection(slotsList[slotID],transitionsList[transitionID],true,weight,type);
            transitionsList[transitionID].outputs.Add(c);
            connectionsList.Add(c);
        }
        
        public void AddTokensToSlot(int slotID, int tokensAmount)
        {
            slotsList[slotID].AddTokens(tokensAmount);
        }
        public void RemoveTokensFromSlot(int slotID, int tokensAmount)
        {
            slotsList[slotID].RemoveTokens(tokensAmount);
        }

    }
}
