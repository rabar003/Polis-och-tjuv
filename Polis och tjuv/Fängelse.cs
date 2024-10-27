using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Fängelse
    {
        public int bredd ; // bredd på fängelset 
        public int höjd ; // höjd på fängelset 
        // En lista som håller alla tjuvar som är fångar i fängelset
        public List<Tjuv> Fångar { get;private set; }

        public Fängelse(int bredd, int höjd)
        {
            this.bredd = bredd;
            this.höjd = höjd;
            Fångar = new List<Tjuv>(); // Skapar en tom lista för fångarna
        }

        // Konstruktor för att skapa ett fängelse
        public Fängelse() 
        { 
            Fångar = new List<Tjuv>(); // Skapar en tom lista för fångarna

        }


        // Metod för att lägga till en tjuv till fängelset
        public void LäggTillFånge(Tjuv tjuv) 
        {
            // Generera unika positioner för fångarna
            int x, y;
            do
            {
                x = new Random().Next(bredd);
                y = new Random().Next(höjd);
            } while (Fångar.Any(fånge => fånge.X == x && fånge.Y == y)); // Kontrollera om positionen redan är upptagen

            tjuv.X = x;
            tjuv.Y = y;

            Fångar.Add(tjuv); // Lägger tjuven till listan över fångar
            Console.WriteLine("Tjuven har placerats i fängelse.");
        }

        public void Uppdatera() 
        {
            foreach (var fånge in Fångar)
            {
                fånge.MoveInPrison(höjd, bredd);
            }
        
        
        }
    }
}
