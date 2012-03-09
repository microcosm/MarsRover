using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public interface ICommandFactory
    {
        ILandingSurfaceSizeCommand CreateLandingSurfaceSizeCommand(int aWidth, int aHeight);
        IRoverDeployCommand CreateRoverDeployCommand(Point aPoint, CardinalDirection aDirection);
    }
}