using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class SampleNet
    {
        bool GA;

        public SampleNet() { GA = false; } // Constructor
      

        public void BuildGA(Petri p)
        {
            p.CreateSlot("L1",0); // 0
            p.CreateSlot("L2",0); // 1
            p.CreateSlot("L3",0); // 2
            p.CreateSlot("L4",0); // 3
            p.CreateSlot("L5",0); // 4
            p.CreateSlot("L6",0); // 5
            p.CreateSlot("L7",0); // 6
            
            p.CreateTransition("T1"); // 0
            p.CreateTransition("T2"); // 1
            
            p.CreateConnectionST(0,0,2); // L1 to T1, weight 2
            p.CreateConnectionST(1,0); //  L2 to T1, weight 1
            p.CreateConnectionST(2,0); //  L3 to T1, weight 1
            
            p.CreateConnectionTS(4,0,3); // T1 to L5, weight 3
            p.CreateConnectionTS(6,0); //  T1 to L7, weight 1

            p.CreateConnectionST(3,1); // L4 to T2, weight 1
            p.CreateConnectionST(4,1,6); // L5 to T2, weight 6

            p.CreateConnectionTS(5,1); // T2 to L6, weight 1
            p.CreateConnectionTS(6,1,2); // T2 to L7, wight 2

            GA = true;
        }

        public void GATokens(Petri p)
        {
            if (GA)
            {
                p.AddTokensToSlot(0,20);
                p.AddTokensToSlot(1,20);
                p.AddTokensToSlot(2,20);
                p.AddTokensToSlot(3,20);

                string l = "Gave 20 tokens to L1, L2, L3 and L4";
                p.UpdateLogs(l);
                p.listStr = "";
            }
            else
            {
                p.UpdateLogs("Couldn't add tokens, because the GrauA net isn't active. Use the 'GrauA' command first to set it up");
            }
        }


    }
}
