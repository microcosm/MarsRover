using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;

namespace Nasa.MarsRover.Tests.Command.Interpret
{
    public class CommandMatcherTests
    {
        [TestFixture]
        public class CommandMatcher_GetCommandType
        {
            [TestCase("2 3", CommandType.LandingSurfaceSizeCommand)]
            [TestCase("162 30", CommandType.LandingSurfaceSizeCommand)]
            [TestCase("3 4 E", CommandType.RoverDeployCommand)]
            [TestCase("3 482 S", CommandType.RoverDeployCommand)]
            [TestCase("563 8 W", CommandType.RoverDeployCommand)]
            [TestCase("6 8 N", CommandType.RoverDeployCommand)]
            [TestCase("L", CommandType.RoverExploreCommand)]
            [TestCase("R", CommandType.RoverExploreCommand)]
            [TestCase("M", CommandType.RoverExploreCommand)]
            [TestCase("MML", CommandType.RoverExploreCommand)]
            [TestCase("LR", CommandType.RoverExploreCommand)]
            [TestCase("LRMLRMLRM", CommandType.RoverExploreCommand)]
            public void Given_valid_command_returns_correct_command_type(string command, CommandType expectedCommandType)
            {
                var commandMatcher = new CommandMatcher();
                var commandType = commandMatcher.GetCommandType(command);
                Assert.AreEqual(expectedCommandType, commandType);
            }

            [TestCase("")]
            [TestCase("\n")]
            [TestCase("6")]
            [TestCase("N")]
            [TestCase("6 N")]
            [TestCase("6 8 n")]
            [TestCase("6 8 T")]
            [TestCase("N 8 4")]
            [TestCase("l")]
            [TestCase("r")]
            [TestCase("m")]
            [TestCase("lrm")]
            [TestCase("L R M")]
            public void Given_invalid_command_throws_CommandException(string invalidCommand)
            {
                var commandMatcher = new CommandMatcher();
                Assert.Throws<CommandException>(
                    () => commandMatcher.GetCommandType(invalidCommand));
            }
        }
    }
}
