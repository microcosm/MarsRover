using System;
using System.Text;
using StructureMap;

namespace Nasa.MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Bootstrap();
            var commandString = buildCommandString();
            var roverReports = executeCommandString(commandString);
            displayToConsole(commandString, roverReports);
        }

        private static string executeCommandString(string commandString)
        {
            var commandCenter = ObjectFactory.GetInstance<ICommandCenter>();
            commandCenter.Execute(commandString);
            var roverReports = commandCenter.GetCombinedRoverReport();
            return roverReports;
        }

        private static void displayToConsole(string commandString, string roverReports)
        {
            Console.WriteLine("Input:");
            Console.WriteLine(commandString);
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("Output:");
            Console.WriteLine(roverReports);
            Console.Write(Environment.NewLine);
            Console.Write("Press <enter> to exit...");
            Console.ReadLine();
        }

        private static string buildCommandString()
        {
            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.AppendLine("5 5");
            commandStringBuilder.AppendLine("1 2 N");
            //commandStringBuilder.AppendLine("LMLMLMLMM");
            commandStringBuilder.Append("3 3 E");
            //commandStringBuilder.AppendLine("MMRMMRMRRM");
            return commandStringBuilder.ToString();
        }
    }
}
