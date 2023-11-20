using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class Help : Command{

        public Help (String name) : base(name){ }

        public override String execute(String[] args)
        {
            return @"
            clear       To clear the text
            restart     To reboot
            shutdown    To turn off the system
            version     To show the current verion";

        }
    }
}
