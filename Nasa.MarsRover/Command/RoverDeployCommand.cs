using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public class RoverDeployCommand : IRoverDeployCommand
    {
        public GridPoint PlateauPoint { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        private IRover rover;
        private ILandingSurface landingSurface;

        public RoverDeployCommand(GridPoint aPlateauPoint, CardinalDirection aCardinalDirection)
        {
            PlateauPoint = aPlateauPoint;
            CardinalDirection = aCardinalDirection;
        }

        public CommandType GetCommandType()
        {
            return CommandType.RoverDeployCommand;
        }

        public void Execute()
        {
            rover.Deploy(landingSurface, PlateauPoint, CardinalDirection);
        }

        public void SetReceivers(IRover aRover, ILandingSurface aLandingSurface)
        {
            rover = aRover;
            landingSurface = aLandingSurface;
        }
    }
}
