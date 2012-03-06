using System.Linq;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Tests.Command.Interpret
{
    public class CommandParserTests
    {
        [TestFixture]
        public class CommandParser_Parse
        {
            private CommandParser commandParser;
            private Mock<ICommandMatcher> mockCommandMatcher;
            private Mock<ICommandFactory> mockCommandFactory;

            [SetUp]
            public void SetUp()
            {
                mockCommandMatcher = new Mock<ICommandMatcher>();
                mockCommandFactory = new Mock<ICommandFactory>();
                commandParser = new CommandParser(mockCommandMatcher.Object, mockCommandFactory.Object);
            }

            [TestCase("1 2", 1, 2)]
            [TestCase("467 54", 467, 54)]
            public void When_Command_is_GridSizeCommand_invokes_CommandFactory_CreateGridSizeCommand_with_parsed_values(string gridSizeCommand, int expectedWidth, int expectedHeight)
            {
                mockCommandMatcher.Setup(x => x.GetCommandType(gridSizeCommand))
                    .Returns(CommandType.GridSizeCommand);
                
                commandParser.Parse(gridSizeCommand);

                mockCommandFactory.Verify(x => 
                    x.CreateGridSizeCommand(expectedWidth, expectedHeight), Times.Once());
            }

            [TestCase("1 2 N", 1, 2, CardinalDirection.North)]
            [TestCase("467 54 E", 467, 54, CardinalDirection.East)]
            [TestCase("4 5 S", 4, 5, CardinalDirection.South)]
            [TestCase("4 5 W", 4, 5, CardinalDirection.West)]
            public void When_Command_is_RoverDeployCommand_invokes_CommandFactory_CreateRoverDeployCommand_with_parsed_values(string roverDeployCommand, int expectedX, int expectedY, CardinalDirection expectedCardinalDirection)
            {
                var expectedPoint = new GridPoint(expectedX, expectedY);

                mockCommandMatcher.Setup(x => x.GetCommandType(roverDeployCommand))
                    .Returns(CommandType.RoverDeployCommand);
                
                commandParser.Parse(roverDeployCommand);

                mockCommandFactory.Verify(x => 
                    x.CreateRoverDeployCommand(expectedPoint, expectedCardinalDirection), Times.Once());
            }

            [Test]
            public void When_Command_is_GridSizeCommand_returns_given_value_from_invocation_of_CommandFactory_CreateGridSizeCommand()
            {
                const string gridSizeCommand = "2 3";

                mockCommandMatcher.Setup(x => x.GetCommandType(gridSizeCommand))
                    .Returns(CommandType.GridSizeCommand);
                var mockGridSizeCommand = new Mock<IGridSizeCommand>();
                mockCommandFactory.Setup(x => x.CreateGridSizeCommand(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(mockGridSizeCommand.Object);

                var commands = commandParser.Parse(gridSizeCommand);

                Assert.AreEqual(mockGridSizeCommand.Object, commands.First());
            }

            [Test]
            public void When_Command_is_RoverDeployCommand_returns_given_value_from_invocation_of_CommandFactory_CreateRoverDeployCommand()
            {
                const string roverDeployCommand = "2 3 N";

                mockCommandMatcher.Setup(x => x.GetCommandType(roverDeployCommand))
                    .Returns(CommandType.RoverDeployCommand);
                var mockRoverDeployCommand = new Mock<IRoverDeployCommand>();

                mockCommandFactory.Setup(x => x.CreateRoverDeployCommand(It.IsAny<GridPoint>(), It.IsAny<CardinalDirection>()))
                    .Returns(mockRoverDeployCommand.Object);

                var commands = commandParser.Parse(roverDeployCommand);

                Assert.AreEqual(mockRoverDeployCommand.Object, commands.First());
            }
        }
    }
}
