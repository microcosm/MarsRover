using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public interface IGridSizeCommand : ICommand
    {
        GridSize Size { get; }
        void SetReceiver(ILandingSurface aLandingSurface);
    }
}