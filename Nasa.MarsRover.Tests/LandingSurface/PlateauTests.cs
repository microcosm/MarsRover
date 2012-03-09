using NUnit.Framework;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Tests.LandingSurface
{
    public class PlateauTests
    {
        [TestFixture]
        public class Plateau_GetSize
        {
            [TestCase(1, 2)]
            [TestCase(4, 5)]
            public void When_size_has_been_set_should_return_size_with_same_values(int expectedWidth, int expectedHeight)
            {
                var expectedSize = new GridSize(expectedWidth, expectedHeight);
                
                var grid = new Plateau();
                grid.SetSize(expectedSize);

                var gridSize = grid.GetSize();

                Assert.AreEqual(expectedSize, gridSize);
            }
        }

        [TestFixture]
        public class Plateau_IsValidPoint
        {
            [TestCase(1, 1, 0, 0)]
            [TestCase(1, 1, 1, 1)]
            [TestCase(5, 3, 5, 3)]
            public void When_point_is_within_PlateauSize_boundary_should_return_true(int boundaryX, int boundaryY, int attemptedPointX, int attemptedPointY)
            {
                var size = new GridSize(boundaryX, boundaryY);
                var point = new Point(attemptedPointX, attemptedPointY);

                var grid = new Plateau();
                grid.SetSize(size);

                var isValid = grid.IsValid(point);
                Assert.That(isValid);
            }

            [TestCase(1, 1, 2, 0)]
            [TestCase(1, 1, 0, 2)]
            [TestCase(1, 1, -1, 1)]
            [TestCase(1, 1, 1, -1)]
            [TestCase(5, 3, 3, 5)]
            public void When_point_is_outside_PlateauSize_boundary_should_return_false(int boundaryX, int boundaryY, int attemptedX, int attemptedY)
            {
                var size = new GridSize(boundaryX, boundaryY);
                var point = new Point(attemptedX, attemptedY);

                var grid = new Plateau();
                grid.SetSize(size);

                var isValid = grid.IsValid(point);
                Assert.That(!isValid);
            }
        }
    }
}
