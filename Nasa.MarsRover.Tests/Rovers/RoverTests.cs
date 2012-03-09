using Moq;
using NUnit.Framework;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Rovers
{
    public class RoverTests
    {
        public class Rover_Constructor
        {
            [Test]
            public void Should_initialize_with_IsDeployed_equal_to_false()
            {
                var rover = new Rover();
                Assert.That(!rover.IsDeployed());
            }
        }

        [TestFixture]
        public class Rover_Deploy
        {
            Mock<ILandingSurface> mockLandingSurface;

            [SetUp]
            public void SetUp()
            {
                mockLandingSurface = new Mock<ILandingSurface>();
            }

            [TestCase(1, 2, CardinalDirection.East)]
            [TestCase(3, 4, CardinalDirection.South)]
            public void When_given_valid_deploy_point_and_direction_should_expose_as_properties(int expectedX, int expectedY, CardinalDirection expectedCardinalDirection)
            {
                var expectedPoint = new Point(expectedX, expectedY);
                mockLandingSurface.Setup(x => x.IsValid(expectedPoint)).Returns(true);

                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, expectedPoint, expectedCardinalDirection);

                Assert.AreEqual(expectedPoint, rover.Position);
                Assert.AreEqual(expectedCardinalDirection, rover.CardinalDirection);
            }

            [Test]
            public void Should_invoke_LandingSurface_IsValid()
            {
                var aPoint = new Point(0, 0);
                mockLandingSurface.Setup(x => x.IsValid(aPoint)).Returns(true);
                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, aPoint, CardinalDirection.South);
                mockLandingSurface.Verify(x => x.IsValid(aPoint), Times.Once());
            }

            [Test]
            public void When_given_invalid_deploy_point_should_throw_RoverDeployException()
            {
                var aPoint = new Point(0, 0);
                var aSize = new Size(0, 0);
                
                mockLandingSurface.Setup(x => x.IsValid(aPoint)).Returns(false);
                mockLandingSurface.Setup(x => x.GetSize()).Returns(aSize);
                var rover = new Rover();
                
                Assert.Throws<RoverDeployException>(() => 
                    rover.Deploy(mockLandingSurface.Object, aPoint, CardinalDirection.West));
            }
        }

        [TestFixture]
        public class Rover_IsDeployed
        {
            Mock<ILandingSurface> mockLandingSurface;

            [SetUp]
            public void SetUp()
            {
                mockLandingSurface = new Mock<ILandingSurface>();
            }

            [Test]
            public void After_Rover_has_been_deployed_returns_true()
            {
                var point = new Point(0, 0);
                mockLandingSurface.Setup(x => x.IsValid(point)).Returns(true);
                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, point, CardinalDirection.North);

                var isDeployed = rover.IsDeployed();

                Assert.That(isDeployed);
            }

            [Test]
            public void Before_Rover_has_been_deployed_returns_false()
            {
                var point = new Point(0, 0);
                mockLandingSurface.Setup(x => x.IsValid(point)).Returns(true);
                var rover = new Rover();

                var isDeployed = rover.IsDeployed();

                Assert.That(!isDeployed);
            }
        }
    }
}
