using System.Collections.Generic;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public class RoverExploreCommand : IRoverExploreCommand
    {
        public IList<Movement> Movements { get; private set; }
        public void SetReceivers(IRover aRover, ILandingSurface aLandingSurface)
        {
            throw new System.NotImplementedException();
        }

        public RoverExploreCommand(IList<Movement> someMovements)
        {
        }

        public CommandType GetCommandType()
        {
            throw new System.NotImplementedException();
        }

        public void Execute()
        {
            throw new System.NotImplementedException();
        }
    }
}
