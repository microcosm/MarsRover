using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.LandingSurface;
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
                x.For<ILandingSurface>().Use<Plateau>();
                x.For<Point>().Use<Point>();
                x.For<Size>().Use<Size>();
                x.For<IRoverDeployCommand>().Use<RoverDeployCommand>();
                x.For<ILandingSurfaceSizeCommand>().Use<LandingSurfaceSizeCommand>();
                x.For<ICommandInvoker>().Use<CommandInvoker>();
                x.For<ICommandMatcher>().Use<CommandMatcher>();
                x.For<ICommandParser>().Use<CommandParser>();
                x.For<ICommandFactory>().Use<CommandFactory>();
                x.For<IRoverFactory>().Use<RoverFactory>();
                x.For<IRover>().Use<Rover>();
                x.For<IReportComposer>().Use<ConsoleReportComposer>();
                x.For<ICommandCenter>().Use<CommandCenter>();
            });
        }
    }
}
