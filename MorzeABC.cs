using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorzeGYAK
{
    class MorzeABC
    {
        public string betu { get; set; }
        public string kod { get; set; }

        public MorzeABC(string betu, string kod)
        {
            this.betu = betu;
            this.kod = kod;
        }
    }
}
