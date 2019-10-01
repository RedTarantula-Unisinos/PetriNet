using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri
{
    class SampleNet
    {
        bool GA, PROJECT;

        public SampleNet() { GA = false; PROJECT = false; } // Constructor
      

        public void BuildGA(Petri p)
        {
            if(PROJECT)
            {
                p.UpdateLogs("Couldn't set GrauA up, because Project is already active");
                return;
            }
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

        public void EvolexNet(Petri p)
        {
            if(GA)
            {
                p.UpdateLogs("Couldn't set Project up, because GrauA is already active");
                return;
            }
            p.CreateSlot("Carnivore Creatures",0); // 0
            p.CreateSlot("Herbivore Creatures",0); // 1
            p.CreateSlot("Prey",0); // 2
            p.CreateSlot("Fruit Trees",0); // 3
            p.CreateSlot("Available Fruit",0); // 4
            p.CreateSlot("Targeted Fruit",0); // 5

            p.CreateTransition("Look For Prey"); // 0
            p.CreateTransition("Look For Fruit"); // 1
            p.CreateTransition("Move On (Carnivore)"); // 2
            p.CreateTransition("Attempt Escape (Herbivore)"); // 3
            p.CreateTransition("Spawn Fruits"); // 4
            p.CreateTransition("Hunt Prey"); // 5
            p.CreateTransition("Eat Fruit"); // 6

            p.CreateConnectionST(0,0); // Carnivore Creature to Look For Prey
            p.CreateConnectionST(0,2); // Carnivore Creature to Idle (Carnivore)
            p.CreateConnectionST(1,0); // Herbivore Creature to Look For Prey
            p.CreateConnectionST(1,2,1,ConnectionType.Inhibitor); // Herbivore Creature to Move On (Carnivore) - Inhibitor
            p.CreateConnectionST(1,1); // Herbivore Creature to Look For Fruit
            p.CreateConnectionST(1,3); // Herbivore Creature to Attempt Escape (Herbivore)
            p.CreateConnectionST(3,4,2); // Fruit Trees to Spawn Fruits - Weight 2
            p.CreateConnectionST(4,1); // Available Fruit to Look For Fruit
            p.CreateConnectionST(4,3,1,ConnectionType.Inhibitor); // Available Fruit to Attempt Escape (Herbivore) - Inhibitor
            p.CreateConnectionST(2,5); // Prey to Hunt Prey
            p.CreateConnectionST(5,6); // Targeted Fruit to Eat Fruit

            p.CreateConnectionTS(2,0); // Look For Prey to Prey
            p.CreateConnectionTS(0,5); // Hunt Prey to Carnivore Creatures
            p.CreateConnectionTS(4,4,5); // Spawn Fruits to Available Fruits - Weight 5
            p.CreateConnectionTS(3,4); // Spawn Fruits to Fruit Trees
            p.CreateConnectionTS(5,1); // Look For Fruit to Targeted Fruit
            p.CreateConnectionTS(1,6); // Eat Fruit to Herbivore Creatures

            PROJECT = true;
        }

        public void EvolexTokens(Petri p)
        {
            if (PROJECT)
            {
                p.AddTokensToSlot(1,25);
                p.AddTokensToSlot(0,2);
                p.AddTokensToSlot(3,20);
                p.AddTokensToSlot(4,100);

                p.UpdateLogs("Spawning 25 herbivore creatures");
                p.UpdateLogs("Spawning 2 carnivore creatures");
                p.UpdateLogs("Spawning 20 fruit trees");
                p.UpdateLogs("Spawning 100 available fruits");
                    
                p.listStr = "";
            }
            else
            {
                p.UpdateLogs("Couldn't add tokens, because the Project net isn't active. Use the 'Project' command first to set it up");
            }
        }
    }
}
