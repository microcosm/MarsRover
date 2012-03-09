using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Command
{
    public class RoverDeployCommandTests
    {
        [TestFixture]
        public class RoverDeployCommand_Constructor
        {
            [TestCase(0, 1, CardinalDirection.North)]
            [TestCase(2, 3, CardinalDirection.West)]
            public void Given_size_and_direction_exposes_as_public_properties(int expectedX, int expectedY, CardinalDirection expectedDirection)
            {
                var expectedPoint = new GridPoint(expectedX, expectedY);

                var roverDeployCommand = new RoverDeployCommand(expectedPoint, expectedDirection);

                Assert.AreEqual(expectedPoint, roverDeployCommand.PlateauPoint);
                Assert.AreEqual(expectedDirection, roverDeployCommand.CardinalDirection);
            }
        }

        [TestFixture]
        public class RoverDeployCommand_SetReceivers
        {
            [Test]
            public void Should_accept_Receiver_arguments()
            {
                const CardinalDirection anyCardinalDirection = CardinalDirection.South;
                var anyGridPoint = new GridPoint(0, 0);
                var mockRover = new Mock<IRover>();
                var mockLandingSurface = new Mock<ILandingSurface>();
                var gridSizeCommand = new RoverDeployCommand(anyGridPoint, anyCardinalDirection);
                Assert.DoesNotThrow(() =>
                    gridSizeCommand.SetReceivers(mockRover.Object, mockLandingSurface.Object));
            }
        }

        [TestFixture]
        public class RoverDeployCommand_Execute
        {
            [Test]
            public void Should_invoke_Rover_Deploy()
            {
                const CardinalDirection expectedCardinalDirection = CardinalDirection.North;
                var expectedPoint = new GridPoint(0, 0);

                var mockRover = new Mock<IRover>();
                var mockLandingSurface = new Mock<ILandingSurface>();
                var roverDeployCommand = new RoverDeployCommand(expectedPoint, expectedCardinalDirection);
                roverDeployCommand.SetReceivers(mockRover.Object, mockLandingSurface.Object);

                roverDeployCommand.Execute();

                mockRover.Verify(x => 
                    x.Deploy(mockLandingSurface.Object, expectedPoint, expectedCardinalDirection), Times.Once());
            }
        }

        [TestFixture]
        public class RoverDeployCommand_GetCommandType
        {
            [Test]
            public void Should_return_RoverDeployCommand_type()
            {
                var point = new GridPoint(0, 0);
                var roverDeployCommand = new RoverDeployCommand(point, CardinalDirection.West);
                Assert.AreEqual(roverDeployCommand.GetCommandType(), CommandType.RoverDeployCommand);
            }
        }
    }
}
