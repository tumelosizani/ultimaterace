using System;
using System.Threading;
using System.Threading.Tasks;

namespace ultimaterace
{
    public abstract class Vehicle
    {
        protected readonly Scoreboard scoreboard;
        protected readonly Random random = new();
        public string Name { get; }
        public int TargetDistance { get; }
        public int Distance { get; protected set; }
        public VehicleStatus Status { get; private set; }
        public bool HasFinished => Status == VehicleStatus.Finished;
        public double DistanceKm => Distance / 1000.0;

        protected Vehicle(string name, int target, Scoreboard sb)
        {
            Name = name;
            TargetDistance = target;
            scoreboard = sb;
        }

        public abstract Task MoveAsync(CancellationToken token);
        
        protected void UpdateStatus(VehicleStatus newStatus)
        {
            if (Status != newStatus)
                Status = newStatus;
        }
        
       
    }
}