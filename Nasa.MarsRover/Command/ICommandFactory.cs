using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public interface ICommandFactory
    {
        IGridSizeCommand CreateGridSizeCommand(int aWidth, int aHeight);
        IRoverDeployCommand CreateRoverDeployCommand(Point aPoint, CardinalDirection aDirection);
    }
}