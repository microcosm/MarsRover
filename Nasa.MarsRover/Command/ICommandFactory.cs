using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Command
{
    public interface ICommandFactory
    {
        IGridSizeCommand CreateGridSizeCommand(int aWidth, int aHeight);
        IRoverDeployCommand CreateRoverDeployCommand(GridPoint plateauPoint, CardinalDirection cardinalDirection);
    }
}