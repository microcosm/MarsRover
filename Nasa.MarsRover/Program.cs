using System;
using System.Reflection;
using System.Text;
using Autofac;

namespace Nasa.MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var commandString = buildCommandString();
            var containerBuilder = createContainerBuilder();

            using (var container = containerBuilder.Build())
            {
                var roverCommandCenter = container.Resolve<ICommandCenter>();
                roverCommandCenter.Execute(commandString);
                var roverReports = roverCommandCenter.GetCombinedRoverReport();
                displayToConsole(commandString, roverReports);
            }
        }

        private static ContainerBuilder createContainerBuilder()
        {
            var programAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();

            return builder;
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
            commandStringBuilder.AppendLine("LMLMLMLMM");
            commandStringBuilder.AppendLine("3 3 E");
            commandStringBuilder.Append("MMRMMRMRRM");
            return commandStringBuilder.ToString();
        }
    }
}
