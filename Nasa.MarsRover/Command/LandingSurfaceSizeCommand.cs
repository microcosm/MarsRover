using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public class LandingSurfaceSizeCommand : ILandingSurfaceSizeCommand
    {
        public Size Size { get; private set; }
        private ILandingSurface landingSurface;

        public LandingSurfaceSizeCommand(Size aSize)
        {
            Size = aSize;
        }

        public CommandType GetCommandType()
        {
            return CommandType.LandingSurfaceSizeCommand;
        }

        public void Execute()
        {
            landingSurface.SetSize(Size);
        }

        public void SetReceiver(ILandingSurface aLandingSurface)
        {
            landingSurface = aLandingSurface;
        }
    }
}
