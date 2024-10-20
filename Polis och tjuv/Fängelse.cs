﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Fängelse
    {
        // En lista som håller alla tjuvar som är fångar i fängelset
        public List<Tjuv> Fångar { get;private set; }

        // Konstruktor för att skapa ett fängelse
        public Fängelse() 
        { 
            Fångar = new List<Tjuv>(); // Skapar en tom lista för fångarna

        }

        // Metod för att lägga till en tjuv till fängelset
        public void LäggTillFånge(Tjuv tjuv) 
        { 
            Fångar.Add(tjuv); // Lägger tjuven till listan över fångar
            Console.WriteLine(" Tjuven har Plascerats i fängelse.");
        
        }
    }
}
