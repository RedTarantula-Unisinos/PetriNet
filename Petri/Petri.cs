using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class Petri
    {




        


        private List<PetriSlot> slotsList;
        private List<PetriConnection> connectionsList;
        private List<PetriTransition> transitionsList;

        public void CreateSlot(string slotName)
        {
            PetriSlot s = new PetriSlot(slotsList.ToArray().Length,slotName);
            slotsList.Add(s);
            Console.WriteLine("Created Slot: " + s.name + " with ID " + s.id);
        }
        
        public void ListSlots()
        {
            Console.WriteLine("LIST OF SLOTS:");
            foreach (PetriSlot s in slotsList)
            {
                Console.WriteLine(s.id + " - " + s.name + " - Contains " + s.tokens + " tokens;");
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
