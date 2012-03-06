using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;
using StructureMap;

namespace Nasa.MarsRover
{
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IPlateau>().Use<GridPlateau>();
                x.For<GridPoint>().Use<GridPoint>();
                x.For<GridSize>().Use<GridSize>();
                x.For<IRoverDeployCommand>().Use<RoverDeployCommand>();
                x.For<IGridSizeCommand>().Use<GridSizeCommand>();
                x.For<ICommandInvoker>().Use<CommandInvoker>();
                x.For<ICommandMatcher>().Use<CommandMatcher>();
                x.For<ICommandParser>().Use<CommandParser>();
                x.For<ICommandFactory>().Use<CommandFactory>();
                x.For<IRoverFactory>().Use<RoverFactory>();
                x.For<IRover>().Use<Rover>();
                x.For<IReportComposer>().Use<ReportComposer>();
                x.For<ICommandCenter>().Use<CommandCenter>();
            });
        }
    }
}
