using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Rovers
{
    public interface IRover
    {
        GridPoint Position { get; set; }
        CardinalDirection CardinalDirection { get; set; }
        void Deploy(ILandingSurface aLandingSurface, GridPoint aPlateauPoint, CardinalDirection aCardinalDirection);
        bool IsDeployed();
    }
}