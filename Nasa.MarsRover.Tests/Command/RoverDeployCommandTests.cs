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
                var expectedPoint = new Point(expectedX, expectedY);

                var roverDeployCommand = new RoverDeployCommand(expectedPoint, expectedDirection);

                Assert.AreEqual(expectedPoint, roverDeployCommand.DeployPoint);
                Assert.AreEqual(expectedDirection, roverDeployCommand.DeployDirection);
            }
        }

        [TestFixture]
        public class RoverDeployCommand_SetReceivers
        {
            [Test]
            public void Accepts_Receiver_arguments()
            {
                const CardinalDirection anyCardinalDirection = CardinalDirection.South;
                var anyPoint = new Point(0, 0);
                var mockRover = new Mock<IRover>();
                var mockLandingSurface = new Mock<ILandingSurface>();
                var landingSurfaceSizeCommand = new RoverDeployCommand(anyPoint, anyCardinalDirection);
                Assert.DoesNotThrow(() =>
                    landingSurfaceSizeCommand.SetReceivers(mockRover.Object, mockLandingSurface.Object));
            }
        }

        [TestFixture]
        public class RoverDeployCommand_Execute
        {
            [Test]
            public void Invokes_Rover_Deploy()
            {
                const CardinalDirection expectedCardinalDirection = CardinalDirection.North;
                var expectedPoint = new Point(0, 0);

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
            public void Returns_RoverDeployCommand_type()
            {
                var point = new Point(0, 0);
                var roverDeployCommand = new RoverDeployCommand(point, CardinalDirection.West);
                Assert.AreEqual(roverDeployCommand.GetCommandType(), CommandType.RoverDeployCommand);
            }
        }
    }
}
