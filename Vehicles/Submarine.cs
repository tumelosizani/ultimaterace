using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace.Vehicles
{
    public class Submarine : Vehicle
    {
        private readonly int speedMin = 40;
        private readonly int speedMax = 80;

        public Submarine(Scoreboard sb) 
            : base("Submarine", sb.SubDistanceToTravelToWin, sb) 
        { }

        public override async Task MoveAsync(CancellationToken token)
        {
            UpdateStatus(VehicleStatus.Racing);

            while (!HasFinished && !token.IsCancellationRequested)
            {
                Distance += random.Next(speedMin, speedMax);
                UpdateStatus(VehicleStatus.Racing);
                scoreboard.SubPosition = Distance;

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