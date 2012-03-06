using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Command
{
    public interface IGridSizeCommand : ICommand
    {
        GridSize Size { get; }
        void SetReceiver(IPlateau aPlateau);
    }
}