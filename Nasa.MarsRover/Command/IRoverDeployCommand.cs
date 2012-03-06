using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public interface IRoverDeployCommand : ICommand
    {
        GridPoint PlateauPoint { get; set; }
        CardinalDirection CardinalDirection { get; set; }
        void SetReceivers(IRover aRover, IPlateau aPlateau);
    }
}