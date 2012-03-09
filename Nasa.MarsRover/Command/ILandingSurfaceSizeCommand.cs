using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public interface ILandingSurfaceSizeCommand : ICommand
    {
        Size Size { get; }
        void SetReceiver(ILandingSurface aLandingSurface);
    }
}