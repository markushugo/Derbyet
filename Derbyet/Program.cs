namespace Derbyet
{
    public class Program
    {
        private static string? winner;

        public static void Main(string[] args)
        {
            Horse horse1 = new Horse("Lightning");
            Horse horse2 = new Horse("Thunder");
            Horse horse3 = new Horse("Storm");

            Thread horseRun1 = new Thread(() => Run(horse1));
            Thread horseRun2 = new Thread(() => Run(horse2));
            Thread horseRun3 = new Thread(() => Run(horse3));

            horseRun1.Start();
            horseRun2.Start();
            horseRun3.Start();

            Console.WriteLine("Løbet er startet!");

            horseRun1.Join();
            horseRun2.Join();
            horseRun3.Join();

            Console.WriteLine($"----------------Vinderen er {winner}!----------------");

        }
        public class Horse
        {
            public string Name { get; set; }
            public int Distance { get; set; }

            public Horse(string name)
            {
                Name = name;
                Distance = 100;
            }
        }
        static void Run(Horse horse)
        {

            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(Random.Shared.Next(200, 800));
                Console.WriteLine($"{horse.Name} har tilbagelagt {horse.Distance} meter.");
                horse.Distance += 100;
            }

            if (Interlocked.CompareExchange(ref winner, horse.Name, null) is null)
            {
                Console.WriteLine($"{horse.Name} er i mål og er vinderen!");
            }
            else
            {
                Console.WriteLine($"{horse.Name} er i mål.");
            }
        }
        
    }
}
