using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class PetriSlot
    {
        public int id;
        public string name;
        public int tokens;

        public PetriSlot(int slotID,string slotName,int slotTokens = 0)
        {
            id = slotID;
            name = slotName;
            tokens = slotTokens;
        }

        public void AddTokens(int amount)
        {
            tokens += amount;
        }
        public void RemoveTokens(int amount)
        {
            tokens -= amount;
        }


    }
}
