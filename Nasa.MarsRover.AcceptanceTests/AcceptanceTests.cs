using System.Reflection;
using System.Text;
using Autofac;
using NUnit.Framework;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.AcceptanceTests
{
    [TestFixture]
    class AcceptanceTests
    {
        private IContainer container;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var programAssembly = Assembly.GetAssembly(typeof(Program));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();

            container = builder.Build();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            container.Dispose();
        }

        [TestCase("5 5", 5, 5)]
        [TestCase("2 3", 2, 3)]
        public void Given_a_commandString_with_one_LandingSurfaceSizeCommand_creates_LandingSurface_and_sets_size(
            string landingSurfaceSizeCommandString, int expectedWidth, int expectedHeight)
        {
            var expectedSize = new Size(expectedWidth, expectedHeight);
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(landingSurfaceSizeCommandString);
            var landingSurface = commandCenter.GetLandingSurface();
            var actualSize = landingSurface.GetSize();
            Assert.AreEqual(expectedSize, actualSize);
        }

        [TestCase("1 2 N")]
        [TestCase("3 4 S")]
        [TestCase("5 6 E")]
        [TestCase("1 2 W")]
        public void Given_a_commandString_with_one_RoverDeployCommand_rovers_deploy_and_report_without_moving(
            string roverDeployCommand)
        {
            var commandString = prependLandingSurfaceSizeCommand(roverDeployCommand);
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandString);
            var roverReports = commandCenter.GetCombinedRoverReport().Split('\n');
            Assert.AreEqual(1, roverReports.Length);
            Assert.AreEqual(roverDeployCommand, roverReports[0]);
        }

        [TestCase("MRM", "2 2 E")]
        [TestCase("MMRMLM", "2 4 N")]
        [TestCase("RM", "2 1 E")]
        [TestCase("RR", "1 1 S")]
        [TestCase("LLL", "1 1 E")]
        public void Given_a_commandString_with_one_RoverExploreCommand_rovers_move_and_turn_before_reporting(
            string roverExploreCommand, string expectedReport)
        {
            var commandString = prependSizeAndDeployCommands(roverExploreCommand);
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandString);
            var roverReports = commandCenter.GetCombinedRoverReport().Split('\n');
            Assert.AreEqual(1, roverReports.Length);
            Assert.AreEqual(expectedReport, roverReports[0]);
        }

        [Test]
        public void Given_input_string_defined_in_problem_statement_produces_output_string_defined_in_problem_statement()
        {
            var inputStringAsDefinedInProblemStatement = getInputCommandString();
            var outputStringAsDefinedInProblemStatement = getExpectedReportString();
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(inputStringAsDefinedInProblemStatement);
            var actualOutputString = commandCenter.GetCombinedRoverReport();
            Assert.AreEqual(outputStringAsDefinedInProblemStatement, actualOutputString);
        }

        private static string prependSizeAndDeployCommands(string roverExploreCommand)
        {
            const string roverDeployCommand = "1 1 N";
            var sizeAndDeployCommands = prependLandingSurfaceSizeCommand(roverDeployCommand);
            var commandString = new StringBuilder();
            commandString.AppendLine(sizeAndDeployCommands);
            commandString.Append(roverExploreCommand);
            return commandString.ToString();
        }

        private static string prependLandingSurfaceSizeCommand(string roverDeployCommand)
        {
            const string landingSurfaceSizeCommand = "9 9";
            var commandString = new StringBuilder();
            commandString.AppendLine(landingSurfaceSizeCommand);
            commandString.Append(roverDeployCommand);
            return commandString.ToString();
        }

        private static string getInputCommandString()
        {
            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.AppendLine("5 5");
            commandStringBuilder.AppendLine("1 2 N");
            commandStringBuilder.AppendLine("LMLMLMLMM");
            commandStringBuilder.AppendLine("3 3 E");
            commandStringBuilder.Append("MMRMMRMRRM");
            return commandStringBuilder.ToString();
        }

        private static string getExpectedReportString()
        {
            var commandStringBuilder = new StringBuilder();
            commandStringBuilder.AppendLine("1 3 N");
            commandStringBuilder.Append("5 1 E");
            return commandStringBuilder.ToString();
        }
    }
}
