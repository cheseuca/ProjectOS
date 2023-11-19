using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class ShutDown : Command{
        public ShutDown (String name) : base(name) { }
    
            public override String execute(String[] args){
                Cosmos.System.Power.Shutdown();
                return "Shutting down the system...";
            }
    }
}
