using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Tests.Command.Interpret
{
    public class CommandParserTests
    {
        [TestFixture]
        public class CommandParser_Parse
        {
            [Test]
            public void When_Command_is_matched_as_LandingSurfaceSizeCommand_retrieves_relevant_command_object_from_factory()
            {
                const string anyLandingsurfaceSizeCommand = "2 4";

                var mockCommandMatcher = new Mock<ICommandMatcher>();
                mockCommandMatcher.Setup(x => x.GetCommandType(anyLandingsurfaceSizeCommand))
                    .Returns(CommandType.LandingSurfaceSizeCommand);

                var expectedCommand = new Mock<ILandingSurfaceSizeCommand>();
                Func<Size, ILandingSurfaceSizeCommand> landingSurfaceSizeCommandFactory = size => expectedCommand.Object;
                
                var commandParser = new CommandParser(mockCommandMatcher.Object, landingSurfaceSizeCommandFactory, null);
                var actualCommands = commandParser.Parse(anyLandingsurfaceSizeCommand);

                Assert.AreEqual(expectedCommand.Object, actualCommands.First());
            }

            [Test]
            public void When_Command_is_matched_as_RoverDeployCommand_retrieves_relevant_command_object_from_factory()
            {
                const string anyRoverDeployCommand = "1 3 N";

                var mockCommandMatcher = new Mock<ICommandMatcher>();
                mockCommandMatcher.Setup(x => x.GetCommandType(anyRoverDeployCommand))
                    .Returns(CommandType.RoverDeployCommand);

                var expectedCommand = new Mock<IRoverDeployCommand>();
                Func<Point, CardinalDirection, IRoverDeployCommand> roverDeployCommandFactory = 
                    (point, direction) => expectedCommand.Object;

                var commandParser = new CommandParser(mockCommandMatcher.Object, null, roverDeployCommandFactory);
                var actualCommands = commandParser.Parse(anyRoverDeployCommand);

                Assert.AreEqual(expectedCommand.Object, actualCommands.First());
            }
        }
    }
}
