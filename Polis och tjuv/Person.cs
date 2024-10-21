using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Person
    {

        // Position i staden
        public int X { get; set; }
        public int Y { get; set; }

        // Riktningen personen rör sig i (X och Y)
        public int XDirection { get; set; }
        public int YDirection { get; set; }

        // En lista som representerar personens föremål/inventory
        public List<string> Inventory { get; set; }

        // Konstruktor för Person, som sätter initial position och en slumpmässig riktning
        public Person(int x, int y)
        {
            X = x;
            Y = y;
            SetRandomDirection();// Slumpmässig riktning för rörelse
            Inventory = new List<string>();  // Startar med tom inventarielista
        }
        
        // Metod för att röra personen inom stadens gränser
        public void Move(int cityWidth, int cityHeight)
        {
            // Uppdaterar positionen enligt riktningen
            X += XDirection;
            Y += YDirection;

            // Om personen går utanför staden, dyker den upp på motsatta sidan
            if (X < 0) X = cityWidth - 1;
            else if (X >= cityWidth) X = 0;

            if (Y < 0) Y = cityHeight - 1;
            else if (Y >= cityHeight) Y = 0;
        }
       
        // Sätter en slumpmässig riktning för personens rörelse
        public void SetRandomDirection()
        {
            Random random = new Random();
            var directions = new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (1, 1) };
            var direction = directions[random.Next(directions.Length)];// Slumpmässigt väljer en riktning från positinerna ovan
            XDirection = direction.Item1;
            YDirection = direction.Item2;
        }
    }
    // Subbklass för medborgare
    public class Medborgare : Person
    {
        public Medborgare(int x, int y) : base(x, y)
        {
            // Medborgarens tillhörigheter
            Inventory.AddRange(new List<string> { "Nycklar", "Mobiltelefon", "Pengar", "Klocka" });
        }
    }

    // Subbklass för Tjuv
    public class Tjuv : Person
    {
        public Tjuv(int x, int y) : base(x, y)
        {
            // Tjuvens stöldgods
            Inventory = new List<string>(); // Börjar tom
        }

        public void Stjäl(Medborgare medborgare)
        {
            if (medborgare.Inventory.Count > 0)
            {
                Random random = new Random();
                int index = random.Next(medborgare.Inventory.Count);// Väljer ett slumpmässigt föremål
                string stulenSak = medborgare.Inventory[index];// Föremålet som stjäls
                medborgare.Inventory.RemoveAt(index);// Ta bort föremålet från medborgarens inventory
                Inventory.Add(stulenSak);// Lägg till föremålet i tjuvens inventory
                Console.WriteLine("Tjuv rånar medborgare på " + stulenSak);
            }
        }
    }

    // Subbklass för Polis
    public class Polis : Person
    {
        public Polis(int x, int y) : base(x, y)
        {
            // Polisen börjar också med ett tomt inventory för beslagtagna saker
            Inventory = new List<string>(); // Börjar tom
        }

        // Metod för att gripa tjuven och ta alla dess stulna saker
        public void Grip(Tjuv tjuv)
        {
            Inventory.AddRange(tjuv.Inventory); // Lägg till alla saker från tjuvens inventory till polisens inventory
            tjuv.Inventory.Clear();// Töm tjuvens inventory efter gripandet
            Console.WriteLine("Polis griper tjuv och beslagtar alla saker.");
        }
    }
}
