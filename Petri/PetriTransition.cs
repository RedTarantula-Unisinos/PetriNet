using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class PetriTransition
    {
        public int id;
        public string name;
        public List<PetriConnection> inputs;
        public List<PetriConnection> outputs;

        public PetriTransition(int transId,string transName)
        {
            id = transId; name = transName;
        }

    }
}
