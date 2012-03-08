using System.Collections.Generic;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover
{
    public class CommandCenter : ICommandCenter
    {
        private readonly IPlateau plateau;
        private readonly ICommandParser commandParser;
        private readonly ICommandInvoker commandInvoker;
        private readonly IReportComposer reportComposer;

        private readonly IList<IRover> rovers; 

        public CommandCenter(IPlateau aPlateau, ICommandParser aCommandParser, ICommandInvoker aCommandInvoker, IReportComposer aReportComposer)
        {
            plateau = aPlateau;
            commandParser = aCommandParser;
            commandInvoker = aCommandInvoker;
            reportComposer = aReportComposer;
            rovers = new List<IRover>();
        }

        public void Execute(string commandString)
        {
            var commandList = commandParser.Parse(commandString);
            commandInvoker.Assign(commandList);
            commandInvoker.SetPlateau(plateau);
            commandInvoker.SetRovers(rovers);
            commandInvoker.InvokeAll();
        }

        public IPlateau GetPlateau()
        {
            return plateau;
        }

        public string GetCombinedRoverReport()
        {
            return reportComposer.CompileReports(rovers);
        }
    }
}