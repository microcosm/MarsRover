using System;
using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover.Rovers
{
    public class Rover : IRover
    {
        public GridPoint Position { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        private bool isDeployed;

        public void Deploy(IPlateau aPlateau, GridPoint aPlateauPoint, CardinalDirection aCardinalDirection)
        {
            if (aPlateau.IsValid(aPlateauPoint))
            {
                Position = aPlateauPoint;
                CardinalDirection = aCardinalDirection;
                isDeployed = true;
                return;
            }

            throwDeployException(aPlateau, aPlateauPoint);
        }

        public bool IsDeployed()
        {
            return isDeployed;
        }

        private static void throwDeployException(IPlateau aPlateau, GridPoint aPlateauPoint)
        {
            var plateauSize = aPlateau.GetSize();
            var exceptionMessage = String.Format("Deploy failed for point ({0},{1}). Plateau size is {2} x {3}.",
                aPlateauPoint.X, aPlateauPoint.Y, plateauSize.Width, plateauSize.Height);
            throw new RoverDeployException(exceptionMessage);
        }
    }
}
