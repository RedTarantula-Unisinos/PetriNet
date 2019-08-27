using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    
        public enum ConnectionType
        {
            Normal,
            Inhibitor,
            Reset
        }

    class PetriConnection
    {
        

        public PetriSlot s;
            public PetriTransition t;
            public int weight;
            public ConnectionType type;
            bool output;

            public PetriConnection(PetriSlot slot, PetriTransition transition, bool isOutput, int connectionWeight = 1, ConnectionType connectionType = ConnectionType.Normal)
            {
                s = slot; t = transition;
                weight = connectionWeight;
                type = connectionType;
                output = isOutput;
            }

    }
}
