using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Command
{
    public class GridSizeCommand : IGridSizeCommand
    {
        public GridSize Size { get; private set; }
        private IPlateau plateau;

        public GridSizeCommand(GridSize aSize)
        {
            Size = aSize;
        }

        public CommandType GetCommandType()
        {
            return CommandType.GridSizeCommand;
        }

        public void Execute()
        {
            plateau.SetSize(Size);
        }

        public void SetReceiver(IPlateau aPlateau)
        {
            plateau = aPlateau;
        }
    }
}
