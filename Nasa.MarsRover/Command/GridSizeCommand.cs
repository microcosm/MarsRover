using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command
{
    public class GridSizeCommand : IGridSizeCommand
    {
        public Size Size { get; private set; }
        private ILandingSurface landingSurface;

        public GridSizeCommand(Size aSize)
        {
            Size = aSize;
        }

        public CommandType GetCommandType()
        {
            return CommandType.GridSizeCommand;
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
