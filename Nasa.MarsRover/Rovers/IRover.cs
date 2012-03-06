using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Rovers
{
    public interface IRover
    {
        GridPoint Position { get; set; }
        CardinalDirection CardinalDirection { get; set; }
        void Deploy(IPlateau aPlateau, GridPoint aPlateauPoint, CardinalDirection aCardinalDirection);
        bool IsDeployed();
    }
}