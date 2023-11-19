using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class Restart : Command{
        public Restart (String name) : base(name) { }
    
            public override String execute(String[] args){
                String response = "Restarting the system...";
                Console.WriteLine(response);
                Cosmos.System.Power.Reboot();
                return response;
            }
    }
}
