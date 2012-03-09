using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public interface IGridSizeCommand : ICommand
    {
        Size Size { get; }
        void SetReceiver(ILandingSurface aLandingSurface);
    }
}