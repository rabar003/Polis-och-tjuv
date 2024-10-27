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
        private Fängelse fängelse;

        // Konstruktor för att skapa staden med angiven storlek och antal personer
        public Stad(int bredd, int höjd, int antalPoliser, int antalTjuvar, int antalMedborgare)
        {
            this.bredd = bredd;
            this.höjd = höjd;
            personer = new List<Person>(); // Skapar en lista för alla personer
            antalRånadeMedborgare = 0;
            antalGripnaTjuvar = 0;

            fängelse = new Fängelse(10, 10);

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
            char[,] fängelseKarta = new char[fängelse.höjd, fängelse.bredd]; // Skapa en separat fängelsekarta

            // Rensa kartan
            for (int y = 0; y < höjd; y++)
            {
                for (int x = 0; x < bredd; x++)
                {
                    karta[y, x] = '-';
                }
            }
            // Rensa fängelsekartan
            for (int y = 0; y < fängelse.höjd; y++)
            {
                for (int x = 0; x < fängelse.bredd; x++)
                {
                    fängelseKarta[y, x] = ' '; // Tomma fängelseceller
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
            // Placera fångar på fängelsekartan
            foreach (var fånge in fängelse.Fångar)
            {
                fängelseKarta[fånge.Y, fånge.X] = 'T'; // Tjuvarna ritas ut på sina positioner i fängelset
            }

            // Rita ut kartan
            for (int y = 0; y < höjd; y++)
            {
                for (int x = 0; x < bredd; x++)
                {
                    Console.Write(karta[y, x]);
                }

                // Rita fängelsekartan bredvid stadskartan
                if (y < fängelse.höjd)
                {
                    // Ändring: Lägg till en titel "Fängelse" ovanför fängelset
                    if (y == 0)
                    {
                        Console.Write("    # Fängelse #"); // Fängelse titel
                    }
                    else
                    {
                        Console.Write("    #"); // Rita vägg på sidan av fängelset
                    }

                    for (int x = 0; x < fängelse.bredd; x++)
                    {
                        // Ändring: Lägg till väggar på fängelsekartan
                        if (y == 0 || y == fängelse.höjd - 1) // Över och under fängelset
                        {
                            Console.Write('#'); // Över och under fängelse
                        }
                        else
                        {
                            Console.Write(fängelseKarta[y, x]); // Skriv ut fängelseinnehåll
                        }
                    }
                    Console.Write("#"); // Höger vägg
                }
                else
                {
                    Console.Write("          "); // Utrymme under fängelset om staden är högre
                }

                Console.WriteLine(); // Ny rad för stadskartan och fängelset
            }

           

           
        }
        // Visar statistik för simulationen 
        public void VisaStatistik()
        {
            Console.WriteLine($"Antal rånade medborgare: {antalRånadeMedborgare}");
            Console.WriteLine($"Antal gripna tjuvar: {antalGripnaTjuvar}");

        }
        

    }


}
