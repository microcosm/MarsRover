using System;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Rovers
{
    public class Rover : IRover
    {
        public Point Position { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        private bool isDeployed;

        public void Deploy(ILandingSurface aLandingSurface, Point aPoint, CardinalDirection aDirection)
        {
            if (aLandingSurface.IsValid(aPoint))
            {
                Position = aPoint;
                CardinalDirection = aDirection;
                isDeployed = true;
                return;
            }

            throwDeployException(aLandingSurface, aPoint);
        }

        public bool IsDeployed()
        {
            return isDeployed;
        }

        private static void throwDeployException(ILandingSurface aLandingSurface, Point aPoint)
        {
            var plateauSize = aLandingSurface.GetSize();
            var exceptionMessage = String.Format("Deploy failed for point ({0},{1}). Landing surface size is {2} x {3}.",
                aPoint.X, aPoint.Y, plateauSize.Width, plateauSize.Height);
            throw new RoverDeployException(exceptionMessage);
        }
    }
}
