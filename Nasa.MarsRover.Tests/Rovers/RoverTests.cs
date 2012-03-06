using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Plateau;
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
            Mock<IPlateau> mockPlateau;

            [SetUp]
            public void SetUp()
            {
                mockPlateau = new Mock<IPlateau>();
            }

            [TestCase(1, 2, CardinalDirection.East)]
            [TestCase(3, 4, CardinalDirection.South)]
            public void When_given_valid_deploy_point_and_direction_should_expose_as_properties(int expectedX, int expectedY, CardinalDirection expectedCardinalDirection)
            {
                var expectedPoint = new GridPoint(expectedX, expectedY);
                mockPlateau.Setup(x => x.IsValid(expectedPoint)).Returns(true);

                var rover = new Rover();
                rover.Deploy(mockPlateau.Object, expectedPoint, expectedCardinalDirection);

                Assert.AreEqual(expectedPoint, rover.Position);
                Assert.AreEqual(expectedCardinalDirection, rover.CardinalDirection);
            }

            [Test]
            public void Should_invoke_Plateau_IsValid()
            {
                var aPoint = new GridPoint(0, 0);
                mockPlateau.Setup(x => x.IsValid(aPoint)).Returns(true);
                var rover = new Rover();
                rover.Deploy(mockPlateau.Object, aPoint, CardinalDirection.South);
                mockPlateau.Verify(x => x.IsValid(aPoint), Times.Once());
            }

            [Test]
            public void When_given_invalid_deploy_point_should_throw_RoverDeployException()
            {
                var aPoint = new GridPoint(0, 0);
                var aSize = new GridSize(0, 0);
                
                mockPlateau.Setup(x => x.IsValid(aPoint)).Returns(false);
                mockPlateau.Setup(x => x.GetSize()).Returns(aSize);
                var rover = new Rover();
                
                Assert.Throws<RoverDeployException>(() => 
                    rover.Deploy(mockPlateau.Object, aPoint, CardinalDirection.West));
            }
        }

        [TestFixture]
        public class Rover_IsDeployed
        {
            Mock<IPlateau> mockPlateau;

            [SetUp]
            public void SetUp()
            {
                mockPlateau = new Mock<IPlateau>();
            }

            [Test]
            public void After_Rover_has_been_deployed_returns_true()
            {
                var point = new GridPoint(0, 0);
                mockPlateau.Setup(x => x.IsValid(point)).Returns(true);
                var rover = new Rover();
                rover.Deploy(mockPlateau.Object, point, CardinalDirection.North);

                var isDeployed = rover.IsDeployed();

                Assert.That(isDeployed);
            }

            [Test]
            public void Before_Rover_has_been_deployed_returns_false()
            {
                var point = new GridPoint(0, 0);
                mockPlateau.Setup(x => x.IsValid(point)).Returns(true);
                var rover = new Rover();

                var isDeployed = rover.IsDeployed();

                Assert.That(!isDeployed);
            }
        }
    }
}
