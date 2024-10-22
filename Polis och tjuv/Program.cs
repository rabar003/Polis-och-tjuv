namespace Polis_och_tjuv
{
    internal class Program
    {
        // inte många commits
        static void Main(string[] args)
        {

            // Skapar en ny stad med specifika parametrar för antalet poliser, tjuvar, medborgare etc.
            Stad stad = new Stad(75, 25, 10, 20, 30);
            // En oändlig loop för att kontinuerligt uppdatera och visa staden
            while (true)
            {
                // Rensar konsolen inför varje ny iteration (så att det ser ut som en ny uppdatering)
                Console.Clear();

                // Ritar staden i konsolen (visar stadens tillstånd, som antagligen poliser, tjuvar, medborgare osv.)
                stad.RitaStad();

                // Uppdaterar stadens tillstånd (antagligen positioner för poliser, tjuvar, händelser etc.)
                stad.Uppdatera();

                // För att visa statistiken 
                stad.VisaStatistik();


                Thread.Sleep(1500); // paus mellan varje iteration

            }
        }
    }
}
