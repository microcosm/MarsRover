using System.Text;
using NUnit.Framework;
using Nasa.MarsRover.LandingSurface;
using StructureMap;

namespace Nasa.MarsRover.AcceptanceTests
{
    [TestFixture]
    class AcceptanceTests
    {
        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            Bootstrapper.Bootstrap();
        }

        [TestCase("5 5", 5, 5)]
        [TestCase("2 3", 2, 3)]
        public void Given_a_commandString_with_one_grid_size_command_creates_LandingSurface_and_sets_size(string gridSizeCommandString, int expectedWidth, int expectedHeight)
        {
            var expectedSize = new GridSize(expectedWidth, expectedHeight);
            var commandCenter = ObjectFactory.GetInstance<ICommandCenter>();
            commandCenter.Execute(gridSizeCommandString);
            var landingSurface = commandCenter.GetLandingSurface();
            var actualSize = landingSurface.GetSize();
            Assert.AreEqual(expectedSize, actualSize);
        }

        [TestCase("1 2 N")]
        [TestCase("3 4 S")]
        [TestCase("5 6 E")]
        [TestCase("1 2 W")]
        public void Given_a_commandString_with_one_rover_deploy_command_rovers_deploy_and_report_without_moving(string roverDeployCommand)
        {
            var commandString = prependGridSizeCommand(roverDeployCommand);
            var commandCenter = ObjectFactory.GetInstance<ICommandCenter>();
            commandCenter.Execute(commandString);
            var roverReports = commandCenter.GetCombinedRoverReport().Split('\n');
            Assert.AreEqual(1, roverReports.Length);
            Assert.AreEqual(roverDeployCommand, roverReports[0]);
        }

        private static string prependGridSizeCommand(string roverDeployCommand)
        {
            const string gridSizeCommand = "9 9";
            var commandString = new StringBuilder();
            commandString.AppendLine(gridSizeCommand);
            commandString.Append(roverDeployCommand);
            return commandString.ToString();
        }
    }
}
