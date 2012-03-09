using System;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Rovers
{
    public class Rover : IRover
    {
        public GridPoint Position { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        private bool isDeployed;

        public void Deploy(ILandingSurface aLandingSurface, GridPoint aPlateauPoint, CardinalDirection aCardinalDirection)
        {
            if (aLandingSurface.IsValid(aPlateauPoint))
            {
                Position = aPlateauPoint;
                CardinalDirection = aCardinalDirection;
                isDeployed = true;
                return;
            }

            throwDeployException(aLandingSurface, aPlateauPoint);
        }

        public bool IsDeployed()
        {
            return isDeployed;
        }

        private static void throwDeployException(ILandingSurface aLandingSurface, GridPoint aPlateauPoint)
        {
            var plateauSize = aLandingSurface.GetSize();
            var exceptionMessage = String.Format("Deploy failed for point ({0},{1}). Landing surface size is {2} x {3}.",
                aPlateauPoint.X, aPlateauPoint.Y, plateauSize.Width, plateauSize.Height);
            throw new RoverDeployException(exceptionMessage);
        }
    }
}
