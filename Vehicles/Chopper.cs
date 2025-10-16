using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace.Vehicles
{
    public class Chopper(Scoreboard sb) : Vehicle("Chopper", sb.ChopperDistanceToTravelToWin, sb)
    {
        private readonly double breakdownChance = 0.2;
        private readonly double refuelChance = 0.1;
        private readonly int speedMin = 80;
        private readonly int speedMax = 150;

        public override async Task MoveAsync(CancellationToken token)
        {
            UpdateStatus(VehicleStatus.Racing);

            while (!HasFinished && !token.IsCancellationRequested)
            {
                // Random breakdown
                if (random.NextDouble() < breakdownChance)
                {
                    UpdateStatus(VehicleStatus.Repairing);
                    await Task.Delay(2000, token);
                    UpdateStatus(VehicleStatus.Racing);
                }
                // Random refuel
                else if (random.NextDouble() < refuelChance)
                {
                    UpdateStatus(VehicleStatus.Refueling);
                    await Task.Delay(1500, token);
                    UpdateStatus(VehicleStatus.Racing);
                }
                // Normal flying
                else
                {
                    Distance += random.Next(speedMin, speedMax);
                    UpdateStatus(VehicleStatus.Racing);
                    scoreboard.ChopperPosition = Distance;
                }

                // Check if finished
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