using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Command
{
    public class RoverExploreCommandTests
    {
        [TestFixture]
        public class RoverDeployCommand_Constructor
        {
            [Test]
            public void Given_list_of_movements_exposes_as_public_property()
            {
                var expectedMovements = new List<Movement> {Movement.Left, Movement.Right};

                var roverExploreCommand = new RoverExploreCommand(expectedMovements);

                Assert.AreEqual(expectedMovements, roverExploreCommand.Movements);
            }
        }

        [TestFixture]
        public class RoverExploreCommand_SetReceiver
        {
            [Test]
            public void Should_accept_Receiver_argument()
            {
                var mockRover = new Mock<IRover>();
                var landingSurfaceSizeCommand = new RoverExploreCommand(null);
                Assert.DoesNotThrow(() =>
                    landingSurfaceSizeCommand.SetReceiver(mockRover.Object));
            }
        }

        [TestFixture]
        public class RoverExploreCommand_Execute
        {
            [Test]
            public void Should_invoke_Rover_Move()
            {
                var expectedMovements = new List<Movement> {Movement.Left, Movement.Right};
                var mockRover = new Mock<IRover>();
                var roverExploreCommand = new RoverExploreCommand(expectedMovements);
                roverExploreCommand.SetReceiver(mockRover.Object);

                roverExploreCommand.Execute();

                mockRover.Verify(x =>
                    x.Move(expectedMovements), Times.Once());
            }
        }

        [TestFixture]
        public class RoverExploreCommand_GetCommandType
        {
            [Test]
            public void Should_return_RoverExploreCommand_type()
            {
                var roverExploreCommand = new RoverExploreCommand(null);
                Assert.AreEqual(roverExploreCommand.GetCommandType(), CommandType.RoverExploreCommand);
            }
        }
    }
}
