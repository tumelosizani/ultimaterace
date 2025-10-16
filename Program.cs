using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ultimaterace.Vehicles;

namespace ultimaterace
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Clear();
            Console.Title = "Ultimate Race Simulator";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            await PrintInstructions();
            
            var sb = new Scoreboard();
            var vehicles = new Vehicle[]
            {
                new Chopper(sb),
                new Bike(sb),
                new Tesla(sb),
                new Submarine(sb)
            };

            using var cts = new CancellationTokenSource();

            
            Console.WriteLine("Preparing race...");
            await Task.Delay(1000, cts.Token);

            Console.WriteLine("\nStarting engines in...");
            for (var i = 3; i >= 1; i--)
            {
                Console.WriteLine($"{i}...");
                await Task.Delay(700, cts.Token);
            }

            Console.WriteLine("🔥 GO GO GO!!! 🔥");
            await Task.Delay(800, cts.Token);

            // Start moving vehicles
            var tasks = vehicles.Select(v => v.MoveAsync(cts.Token)).ToArray();

            while (!vehicles.Any(v => v.HasFinished))
            {
                sb.Display(vehicles);
                await Task.Delay(1000, cts.Token);
            }

            await cts.CancelAsync();
            var winner = vehicles.First(v => v.HasFinished);

            Scoreboard.DisplayWinner(winner);
        }

        private static async Task PrintInstructions()
        {
            Console.WriteLine("WELCOME!!!");
            await Task.Delay(1000);
            Console.WriteLine("To the ULTIMATE RACE!!!");
            await Task.Delay(1000);
            Console.WriteLine("Travel from Cairo to Cape Town.");
            await Task.Delay(1000);
            Console.Write("A journey of blood");
            await Task.Delay(1000);
            Console.Write(", sweat");
            await Task.Delay(1000);
            Console.WriteLine(", and gears!");
            await Task.Delay(1000);
            Console.WriteLine("Gentlemen, start your engine!");
            await Task.Delay(1000);
            
        }

    }
}