using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Rovers
{
    public interface IRover
    {
        Point Position { get; set; }
        CardinalDirection CardinalDirection { get; set; }
        void Deploy(ILandingSurface aLandingSurface, Point aPoint, CardinalDirection aDirection);
        bool IsDeployed();
    }
}