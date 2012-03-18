using System.Collections.Generic;
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
            public void Initializes_with_IsDeployed_equal_to_false()
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
            public void Given_valid_deploy_point_and_direction_exposes_as_properties(int expectedX, int expectedY, CardinalDirection expectedCardinalDirection)
            {
                var expectedPoint = new Point(expectedX, expectedY);
                mockLandingSurface.Setup(x => x.IsValid(expectedPoint)).Returns(true);

                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, expectedPoint, expectedCardinalDirection);

                Assert.AreEqual(expectedPoint, rover.Position);
                Assert.AreEqual(expectedCardinalDirection, rover.CardinalDirection);
            }

            [Test]
            public void Asks_if_deploy_point_is_valid_for_landing_surface()
            {
                var aPoint = new Point(0, 0);
                mockLandingSurface.Setup(x => x.IsValid(aPoint)).Returns(true);
                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, aPoint, CardinalDirection.South);
                mockLandingSurface.Verify(x => x.IsValid(aPoint), Times.Once());
            }

            [Test]
            public void Given_invalid_deploy_point_throws_RoverDeployException()
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
        public class Rover_Move
        {
            [TestCase(1, 1, CardinalDirection.South, Movement.Right, Movement.Right, Movement.Forward, 1, 2, CardinalDirection.North)]
            [TestCase(2, 4, CardinalDirection.East, Movement.Forward, Movement.Forward, Movement.Forward, 5, 4, CardinalDirection.East)]
            [TestCase(2, 2, CardinalDirection.West, Movement.Left, Movement.Forward, Movement.Forward, 2, 0, CardinalDirection.South)]
            [TestCase(4, 5, CardinalDirection.North, Movement.Left, Movement.Left, Movement.Left, 4, 5, CardinalDirection.East)]
            [TestCase(0, 0, CardinalDirection.South, Movement.Left, Movement.Forward, Movement.Forward, 2, 0, CardinalDirection.East)]
            public void Alters_position_and_direction_in_response_to_movement_list(int startX, int startY, 
                CardinalDirection startDirection, Movement firstMove, Movement secondMove, Movement thirdMove, 
                int expectedX, int expectedY, CardinalDirection expectedDirection)
            {
                var startPosition = new Point(startX, startY);
                var expectedPosition = new Point(expectedX, expectedY);
                var movements = new List<Movement> {firstMove, secondMove, thirdMove};

                var mockLandingSurface = new Mock<ILandingSurface>();
                mockLandingSurface.Setup(x => x.IsValid(startPosition)).Returns(true);

                var rover = new Rover();
                rover.Deploy(mockLandingSurface.Object, startPosition, startDirection);
                rover.Move(movements);

                Assert.AreEqual(expectedPosition.X, rover.Position.X);
                Assert.AreEqual(expectedPosition.Y, rover.Position.Y);
                Assert.AreEqual(expectedDirection, rover.CardinalDirection);
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
