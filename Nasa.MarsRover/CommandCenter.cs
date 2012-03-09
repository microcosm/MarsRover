using System.Collections.Generic;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover
{
    public class CommandCenter : ICommandCenter
    {
        private readonly ILandingSurface landingSurface;
        private readonly ICommandParser commandParser;
        private readonly ICommandInvoker commandInvoker;
        private readonly IReportComposer reportComposer;

        private readonly IList<IRover> rovers; 

        public CommandCenter(ILandingSurface aLandingSurface, ICommandParser aCommandParser, ICommandInvoker aCommandInvoker, IReportComposer aReportComposer)
        {
            rovers = new List<IRover>();
            landingSurface = aLandingSurface;
            commandParser = aCommandParser;
            commandInvoker = aCommandInvoker;
            reportComposer = aReportComposer;
            commandInvoker.SetLandingSurface(landingSurface);
            commandInvoker.SetRovers(rovers);
        }

        public void Execute(string commandString)
        {
            var commandList = commandParser.Parse(commandString);
            commandInvoker.Assign(commandList);
            commandInvoker.InvokeAll();
        }

        public ILandingSurface GetLandingSurface()
        {
            return landingSurface;
        }

        public string GetCombinedRoverReport()
        {
            return reportComposer.CompileReports(rovers);
        }
    }
}