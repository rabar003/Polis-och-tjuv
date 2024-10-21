using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Fängelse
    {
        public List<Tjuv> Fångar { get;private set; }

        public Fängelse() 
        { 
            Fångar = new List<Tjuv>();
        
        }
        
        public void LäggTillFånge(Tjuv tjuv) 
        { 
            Fångar.Add(tjuv);
            Console.WriteLine(" Tjuven har Plascerats i fängelse.");
        
        }
    }
}
