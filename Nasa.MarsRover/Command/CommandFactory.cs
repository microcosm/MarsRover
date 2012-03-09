using Nasa.MarsRover.LandingSurface;
using StructureMap;

namespace Nasa.MarsRover.Command
{
    public class CommandFactory : ICommandFactory
    {
        public IGridSizeCommand CreateGridSizeCommand(int aWidth, int aHeight)
        {
            var size = new Size(aWidth, aHeight);

            return ObjectFactory
                .With("aSize").EqualTo(size)
                .GetInstance<IGridSizeCommand>();
        }

        public IRoverDeployCommand CreateRoverDeployCommand(Point aPoint, CardinalDirection aDirection)
        {
            return ObjectFactory
                .With("aPoint").EqualTo(aPoint)
                .With("aDirection").EqualTo(aDirection)
                .GetInstance<IRoverDeployCommand>();
        }
    }
}
