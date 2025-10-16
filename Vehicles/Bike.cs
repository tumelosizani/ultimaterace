using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace.Vehicles
{
    public class Bike : Vehicle
    {
        private readonly double breakdownChance = 0.5;
        private readonly double refuelChance = 0.1;
        private readonly int speedMin = 100;
        private readonly int speedMax = 200;

        public Bike(Scoreboard sb) 
            : base("Bike", sb.BikeDistanceToTravelToWin, sb) 
        { }

        public override async Task MoveAsync(CancellationToken token)
        {
            UpdateStatus(VehicleStatus.Racing);

            while (!HasFinished && !token.IsCancellationRequested)
            {
                if (random.NextDouble() < breakdownChance)
                {
                    UpdateStatus(VehicleStatus.Repairing);
                    await Task.Delay(1500, token);
                    UpdateStatus(VehicleStatus.Racing);
                }
                else if (random.NextDouble() < refuelChance)
                {
                    UpdateStatus(VehicleStatus.Refueling);
                    await Task.Delay(1000, token);
                    UpdateStatus(VehicleStatus.Racing);
                }
                else
                {
                    Distance += random.Next(speedMin, speedMax);
                    UpdateStatus(VehicleStatus.Racing);
                    scoreboard.BikePosition = Distance;
                }

                if (Distance >= TargetDistance)
                {
                    Distance = TargetDistance;
                    UpdateStatus(VehicleStatus.Finished);
                }

                await Task.Delay(500, token);
            }
        }
    }
}
