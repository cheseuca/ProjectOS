using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class Version : Command{
        public Version (String name) : base(name) { }
    
            public override String execute(String[] args){
                return "ProjectOS v0.0.1";
            }
    }
}
