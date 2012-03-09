using Nasa.MarsRover.LandingSurface;
using StructureMap;

namespace Nasa.MarsRover.Command
{
    public class CommandFactory : ICommandFactory
    {
        public IGridSizeCommand CreateGridSizeCommand(int aWidth, int aHeight)
        {
            var size = new GridSize(aWidth, aHeight);

            return ObjectFactory
                .With("aSize").EqualTo(size)
                .GetInstance<IGridSizeCommand>();
        }

        public IRoverDeployCommand CreateRoverDeployCommand(GridPoint plateauPoint, CardinalDirection cardinalDirection)
        {
            return ObjectFactory
                .With("aPlateauPoint").EqualTo(plateauPoint)
                .With("aCardinalDirection").EqualTo(cardinalDirection)
                .GetInstance<IRoverDeployCommand>();
        }
    }
}
