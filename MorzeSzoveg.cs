using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MorzeGYAK
{
    class MorzeSzoveg
    {
        public string szerzo { get; set; }
        public string idezet { get; set; }

        public MorzeSzoveg(string szerzo, string idezet)
        {
            this.szerzo = szerzo;
            this.idezet = idezet;
        }
    }
}
