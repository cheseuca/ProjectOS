using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands{
    internal class Command{
        
        public readonly string name;
        
        public Command(string name)
        {
            this.name = name;
        }

        public virtual String execute(String[] args)
        {
            return "Command not found";
        }


    }
}
