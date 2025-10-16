using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace.Vehicles
{
    public class Submarine(Scoreboard sb) : Vehicle("Submarine", sb.SubDistanceToTravelToWin, sb)
    {
        private readonly int speedMin = 40;
        private readonly int speedMax = 80;

        public override async Task MoveAsync(CancellationToken token)
        {
            UpdateStatus(VehicleStatus.Racing);

            while (!HasFinished && !token.IsCancellationRequested)
            {
                // Random speed
                Distance += random.Next(speedMin, speedMax);
                UpdateStatus(VehicleStatus.Racing);
                scoreboard.SubPosition = Distance;

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