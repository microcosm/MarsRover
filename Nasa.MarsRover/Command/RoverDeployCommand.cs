using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public class RoverDeployCommand : IRoverDeployCommand
    {
        public Point DeployPoint { get; set; }
        public CardinalDirection DeployDirection { get; set; }
        private IRover rover;
        private ILandingSurface landingSurface;

        public RoverDeployCommand(Point aPoint, CardinalDirection aDirection)
        {
            DeployPoint = aPoint;
            DeployDirection = aDirection;
        }

        public CommandType GetCommandType()
        {
            return CommandType.RoverDeployCommand;
        }

        public void Execute()
        {
            rover.Deploy(landingSurface, DeployPoint, DeployDirection);
        }

        public void SetReceivers(IRover aRover, ILandingSurface aLandingSurface)
        {
            rover = aRover;
            landingSurface = aLandingSurface;
        }
    }
}
