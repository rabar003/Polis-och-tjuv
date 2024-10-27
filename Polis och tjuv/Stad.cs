using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Stad
    {
        private int bredd; // Stadens bredd
        private int höjd;  // Stadens höjd
        private List<Person> personer; // Lista över alla personer (poliser, tjuvar och medborgare) i staden
        private int antalRånadeMedborgare; // Räknar antalet rånade medborgare
        private int antalGripnaTjuvar;  // Räknar antalet gripna tjuvar

        // Konstruktor för att skapa staden med angiven storlek och antal personer
        public Stad(int bredd, int höjd, int antalPoliser, int antalTjuvar, int antalMedborgare)
        {
            this.bredd = bredd;
            this.höjd = höjd;
            personer = new List<Person>(); // Skapar en lista för alla personer
            antalRånadeMedborgare = 0;
            antalGripnaTjuvar = 0;

            // Skapa poliser
            for (int i = 0; i < antalPoliser; i++)
            {
                personer.Add(new Polis(RandomCoord(bredd), RandomCoord(höjd)));
            }

            // Skapa tjuvar
            for (int i = 0; i < antalTjuvar; i++)
            {
                personer.Add(new Tjuv(RandomCoord(bredd), RandomCoord(höjd)));
            }

            // Skapa medborgare
            for (int i = 0; i < antalMedborgare; i++)
            {
                personer.Add(new Medborgare(RandomCoord(bredd), RandomCoord(höjd)));
            }
        }
        // Genererar en slumpmässig koordinat inom stadens gränser
        private int RandomCoord(int max) => new Random().Next(max);

        // Uppdatera stadens tillstånd (rörelse och möten)
        public void Uppdatera()
        {
            foreach (var person in personer)
            {
                person.Move(bredd, höjd);
            }

            // Kolla om personer möts
            for (int i = 0; i < personer.Count; i++)
            {
                for (int j = i + 1; j < personer.Count; j++)
                {
                    // Om två personer har samma koordinater, hantera mötet
                    if (personer[i].X == personer[j].X && personer[i].Y == personer[j].Y)
                    {
                        HanteraMöte(personer[i], personer[j]);
                    }
                }
            }
            // anropa att uppdatera fängelset 
            fängelse.Uppdatera();
        }
        // Hanterar vad som händer när två personer möts
        private void HanteraMöte(Person person1, Person person2)
        {
            if (person1 is Polis && person2 is Tjuv)
            {
                ((Polis)person1).Grip((Tjuv)person2);
                fängelse.LäggTillFånge((Tjuv)person2);
                personer.Remove(person2); // Ta bort tjuven från staden
                antalGripnaTjuvar++;
            }
            else if (person1 is Tjuv && person2 is Polis)
            {
                ((Polis)person2).Grip((Tjuv)person1);
                fängelse.LäggTillFånge((Tjuv)person1);
                personer.Remove(person1); // Ta bort tjuven från staden
                antalGripnaTjuvar++;
            }
            else if (person1 is Tjuv && person2 is Medborgare)
            {
                ((Tjuv)person1).Stjäl((Medborgare)person2);
                antalRånadeMedborgare++;
            }
            else if (person1 is Medborgare && person2 is Tjuv)
            {
                ((Tjuv)person2).Stjäl((Medborgare)person1);
                antalRånadeMedborgare++;
            }
        }
        // Ritar ut stadens karta med personer och fängelset
        public void RitaStad()
        {
            char[,] karta = new char[höjd, bredd];

            // Rensa kartan
            for (int y = 0; y < höjd; y++)
            {
                for (int x = 0; x < bredd; x++)
                {
                    karta[y, x] = '-';
                }
            }

            // Placera personer på kartan
            foreach (var person in personer)
            {
                if (person is Polis)
                    karta[person.Y, person.X] = 'P';
                else if (person is Tjuv)
                    karta[person.Y, person.X] = 'T';
                else if (person is Medborgare)
                    karta[person.Y, person.X] = 'M';
            }

            // Rita ut kartan
            for (int y = 0; y < höjd; y++)
            {
                for (int x = 0; x < bredd; x++)
                {
                    Console.Write(karta[y, x]);
                }


                // Här börjar vi rita fängelset bredvid stadens karta på samma rad
                if (y == 0)
                {
                    Console.Write("    Fängelse:");
                }
                else if (y == 1 || y == fängelse.höjd + 2 )
                {
                    Console.Write("    ########");
                }
                else if (y >= 2 && y < 2 +  fängelse.Fångar.Count)
                {
                    Console.Write("    # Tjuv  #");  // Varje tjuv ritas som en rad
                }
                else if (y >= 2 + fängelse.Fångar.Count && y < 7)  // 7 är höjden på fängelset
                {
                    Console.Write("    #        #");  // Tomma rader i fängelset
                }
                else if (y == 7)
                {
                    Console.Write("    ########");
                }

                Console.WriteLine();  // Ny rad för både stadskartan och fängelset
            }
        }
        // Visar statistik för simulationen 
        public void VisaStatistik()
        {
            Console.WriteLine($"Antal rånade medborgare: {antalRånadeMedborgare}");
            Console.WriteLine($"Antal gripna tjuvar: {antalGripnaTjuvar}");
         
        }
        private Fängelse fängelse = new Fängelse();

    }
}
