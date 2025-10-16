using System;
using System.Linq;
using System.Text;

namespace ultimaterace
{
    public class Scoreboard
    {
        public readonly int ChopperDistanceToTravelToWin = 7400;
        public readonly int BikeDistanceToTravelToWin = 9800;
        public readonly int TeslaDistanceToTravelToWin = 10200;
        public readonly int SubDistanceToTravelToWin = 12000;

        public int ChopperPosition;
        public int BikePosition;
        public int TeslaPosition;
        public int SubPosition;

        private readonly int barWidth = 40;

        public void Display(Vehicle[] vehicles)
        {
            lock (Console.Out)
            {
                Console.Clear();
                
                // Ensure UTF-8 encoding for special characters
                Console.OutputEncoding = Encoding.UTF8;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("🏁  ULTIMATE RACE DASHBOARD  🏁");
                Console.ResetColor();
                Console.WriteLine("--------------------------------------------\n");
                Console.WriteLine($"{"Vehicle",-12} | {"Progress",-40} | {"KM",8} |{"% Done",6} | Status");

                foreach (var v in vehicles)
                {
                    DrawVehicleProgress(v);
                }

                Console.WriteLine("\n--------------------------------------------");
                Console.WriteLine($"⏱️  {DateTime.Now:T}");
            }
        }

        private void DrawVehicleProgress(Vehicle vehicle)
        {
            // pct = distance / targetDistance
            var percentage = (double)vehicle.Distance / vehicle.TargetDistance;
            if (percentage > 1) percentage = 1;

            var filled = (int)(percentage * barWidth);
            var remaining = barWidth - filled;

            Console.ForegroundColor = GetColor(vehicle);
            var bar = new string('█', filled) + new string('░', remaining);

            Console.WriteLine($"{vehicle.Name,-12} | {bar} | {vehicle.DistanceKm,5:F2}KM |{percentage * 100,5:F1}% | {vehicle.Status}");
            Console.ResetColor();
        }

        protected virtual ConsoleColor GetColor(Vehicle v)
        {
            return v.Status switch
            {
                VehicleStatus.Finished => ConsoleColor.Green,
                VehicleStatus.Refueling or VehicleStatus.Charging => ConsoleColor.Yellow,
                VehicleStatus.Faulted or VehicleStatus.Repairing => ConsoleColor.Red,
                _ => ConsoleColor.Cyan
            };
        }

        public static void DisplayWinner(Vehicle winner)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n🏆🏆🏆  THE WINNER IS  🏆🏆🏆\n");
            Console.WriteLine($"✨ {winner.Name} ✨");
            Console.ResetColor();
            Console.WriteLine("\n🎉 Congratulations! The race is complete!\n");
        }
    }
}
