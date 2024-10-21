using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polis_och_tjuv
{
    public class Stad
    {
        private int bredd;
        private int höjd;
        private List<Person> personer;
        private int antalRånadeMedborgare;
        private int antalGripnaTjuvar;

        public Stad(int bredd, int höjd, int antalPoliser, int antalTjuvar, int antalMedborgare)
        {
            this.bredd = bredd;
            this.höjd = höjd;
            personer = new List<Person>();
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
        private int RandomCoord(int max) => new Random().Next(max);

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
                    if (personer[i].X == personer[j].X && personer[i].Y == personer[j].Y)
                    {
                        HanteraMöte(personer[i], personer[j]);
                    }
                }
            }
        }
        private void HanteraMöte(Person person1, Person person2)
        {
            if (person1 is Polis && person2 is Tjuv)
            {
                ((Polis)person1).Grip((Tjuv)person2);
                personer.Remove(person2); // Ta bort tjuven från staden
                antalGripnaTjuvar++;
            }
            else if (person1 is Tjuv && person2 is Polis)
            {
                ((Polis)person2).Grip((Tjuv)person1);
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
            }
        }
        public void VisaStatistik()
        {
            Console.WriteLine($"Antal rånade medborgare: {antalRånadeMedborgare}");
            Console.WriteLine($"Antal gripna tjuvar: {antalGripnaTjuvar}");
        }
        private Fängelse fängelse = new Fängelse();

    }
}
