using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Command.Interpret
{
    public class CommandParserTests
    {
        [TestFixture]
        public class CommandParser_Parse
        {
            [TestCase("2 4", 2, 4)]
            [TestCase("3 1", 3, 1)]
            public void When_Command_is_matched_as_LandingSurfaceSizeCommand_retrieves_command_object_from_factory_with_parsed_values(
                string landingSurfaceSizeCommand, int expectedWidth, int expectedHeight)
            {
                var expectedSize = new Size(expectedWidth, expectedHeight);
                var expectedCommand = new Mock<ILandingSurfaceSizeCommand>();
                Func<Size, ILandingSurfaceSizeCommand> factory = size =>
                {
                    expectedCommand.Setup(x => x.Size).Returns(size);
                    return expectedCommand.Object;
                };
                var mockCommandMatcher = createMockCommandMatcher(CommandType.LandingSurfaceSizeCommand);

                var commandParser = new CommandParser(mockCommandMatcher.Object, factory, null, null);
                var actualCommand = (ILandingSurfaceSizeCommand) commandParser.Parse(landingSurfaceSizeCommand).First();

                Assert.AreEqual(expectedCommand.Object, actualCommand);
                Assert.AreEqual(expectedSize, actualCommand.Size);
            }

            [TestCase("1 3 N", 1, 3, CardinalDirection.North)]
            [TestCase("4 6 W", 4, 6, CardinalDirection.West)]
            public void When_Command_is_matched_as_RoverDeployCommand_retrieves_command_object_from_factory_with_parsed_values(
                string roverDeployCommand, int expectedX, int expectedY, CardinalDirection expectedCardinalDirection)
            {
                var expectedPoint = new Point(expectedX, expectedY);
                var expectedCommand = new Mock<IRoverDeployCommand>();
                Func<Point, CardinalDirection, IRoverDeployCommand> factory = (point, direction) =>
                {
                    expectedCommand.Setup(x => x.DeployPoint).Returns(point);
                    expectedCommand.Setup(x => x.DeployDirection).Returns(direction);
                    return expectedCommand.Object;
                };
                var mockCommandMatcher = createMockCommandMatcher(CommandType.RoverDeployCommand);

                var commandParser = new CommandParser(mockCommandMatcher.Object, null, factory, null);
                var actualCommand = (IRoverDeployCommand) commandParser.Parse(roverDeployCommand).First();

                Assert.AreEqual(expectedCommand.Object, actualCommand);
                Assert.AreEqual(expectedPoint, actualCommand.DeployPoint);
                Assert.AreEqual(expectedCardinalDirection, actualCommand.DeployDirection);
            }

            [TestCase("LMR", Movement.Left, Movement.Forward, Movement.Right)]
            [TestCase("MRL", Movement.Forward, Movement.Right, Movement.Left)]
            public void When_Command_is_matched_as_RoverExploreCommand_retrieves_command_object_from_factory_with_parsed_values(
                string roverExploreCommand, Movement expectedFirstMovement, Movement expectedSecondMovement, 
                Movement expectedThirdMovement)
            {
                var expectedCommand = new Mock<IRoverExploreCommand>();
                Func<IList<Movement>, IRoverExploreCommand> factory = movements =>
                {
                    expectedCommand.Setup(x => x.Movements).Returns(movements);
                    return expectedCommand.Object;
                };
                var mockCommandMatcher = createMockCommandMatcher(CommandType.RoverExploreCommand);

                var commandParser = new CommandParser(mockCommandMatcher.Object, null, null, factory);
                var actualCommand = (IRoverExploreCommand) commandParser.Parse(roverExploreCommand).First();

                Assert.AreEqual(expectedCommand.Object, actualCommand);
                Assert.AreEqual(expectedFirstMovement, actualCommand.Movements[0]);
                Assert.AreEqual(expectedSecondMovement, actualCommand.Movements[1]);
                Assert.AreEqual(expectedThirdMovement, actualCommand.Movements[2]);
            }

            private static Mock<ICommandMatcher> createMockCommandMatcher(CommandType commandType)
            {
                var mockCommandMatcher = new Mock<ICommandMatcher>();
                mockCommandMatcher.Setup(x => x.GetCommandType(It.IsAny<string>()))
                    .Returns(commandType);
                return mockCommandMatcher;
            }
        }
    }
}
