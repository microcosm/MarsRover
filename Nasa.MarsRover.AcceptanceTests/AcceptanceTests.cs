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
        public void Given_a_commandString_with_one_LandingSurfaceSizeCommand_creates_LandingSurface_and_sets_size(string landingSurfaceSizeCommandString, int expectedWidth, int expectedHeight)
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
        public void Given_a_commandString_with_one_RoverDeployCommand_rovers_deploy_and_report_without_moving(string roverDeployCommand)
        {
            var commandString = prependLandingSurfaceSizeCommand(roverDeployCommand);
            var commandCenter = container.Resolve<ICommandCenter>();
            commandCenter.Execute(commandString);
            var roverReports = commandCenter.GetCombinedRoverReport().Split('\n');
            Assert.AreEqual(1, roverReports.Length);
            Assert.AreEqual(roverDeployCommand, roverReports[0]);
        }

        private static string prependLandingSurfaceSizeCommand(string roverDeployCommand)
        {
            const string landingSurfaceSizeCommand = "9 9";
            var commandString = new StringBuilder();
            commandString.AppendLine(landingSurfaceSizeCommand);
            commandString.Append(roverDeployCommand);
            return commandString.ToString();
        }
    }
}
