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
            public void When_size_has_been_set_returns_size_with_same_values(int expectedWidth, int expectedHeight)
            {
                var expectedSize = new Size(expectedWidth, expectedHeight);
                var plateau = new Plateau();

                plateau.SetSize(expectedSize);
                var actualSize = plateau.GetSize();

                Assert.AreEqual(expectedSize, actualSize);
            }
        }

        [TestFixture]
        public class Plateau_IsValidPoint
        {
            [TestCase(1, 1, 0, 0)]
            [TestCase(1, 1, 1, 1)]
            [TestCase(5, 3, 5, 3)]
            public void When_point_is_within_size_boundary_returns_true(int boundaryX, int boundaryY, int attemptedPointX, int attemptedPointY)
            {
                var size = new Size(boundaryX, boundaryY);
                var point = new Point(attemptedPointX, attemptedPointY);
                var plateau = new Plateau();

                plateau.SetSize(size);

                var isValidPoint = plateau.IsValid(point);
                Assert.That(isValidPoint);
            }

            [TestCase(1, 1, 2, 0)]
            [TestCase(1, 1, 0, 2)]
            [TestCase(1, 1, -1, 1)]
            [TestCase(1, 1, 1, -1)]
            [TestCase(5, 3, 3, 5)]
            public void When_point_is_outside_size_boundary_returns_false(int boundaryX, int boundaryY, int attemptedX, int attemptedY)
            {
                var size = new Size(boundaryX, boundaryY);
                var point = new Point(attemptedX, attemptedY);
                var plateau = new Plateau();
                
                plateau.SetSize(size);

                var isValidPoint = plateau.IsValid(point);
                Assert.That(!isValidPoint);
            }
        }
    }
}
