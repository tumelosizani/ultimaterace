using System;
using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace.Vehicles
{
    public class Tesla(Scoreboard sb) : Vehicle("Tesla", sb.TeslaDistanceToTravelToWin, sb)
    {
        private readonly double breakdownChance = 0.1;
        private readonly double chargeChance = 0.15;
        private readonly int speedMin = 120;
        private readonly int speedMax = 180;

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
                else if (random.NextDouble() < chargeChance)
                {
                    UpdateStatus(VehicleStatus.Charging);
                    await Task.Delay(1200, token);
                    UpdateStatus(VehicleStatus.Racing);
                }
                else
                {
                    Distance += random.Next(speedMin, speedMax);
                    UpdateStatus(VehicleStatus.Racing);
                    scoreboard.TeslaPosition = Distance;
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