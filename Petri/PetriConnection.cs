﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    
        public enum ConnectionType
        {
            Normal,
            Inhibitor, // If there's a token, the transition's not available
            Reset // Clears all tokens when activated
        }

    class PetriConnection
    {

        public int id;
        public int s;
        public int t;
        public int weight;
        public ConnectionType type;
        public bool output;

        public PetriConnection(int slotID,int slot,int transition,bool isOutput,int connectionWeight = 1,ConnectionType connectionType = ConnectionType.Normal)
        {
            id = slotID;
            s = slot;
            t = transition;
            weight = connectionWeight;
            type = connectionType;
            output = isOutput;
        }

    }
}
