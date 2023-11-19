using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOS.Commands
{
    internal class ChangeDirectory : Command{
        //Change Directory
        public ChangeDirectory (String name) : base(name) { }
            public override String execute(String[] args)
            {
                return "";
            }

    }
}
