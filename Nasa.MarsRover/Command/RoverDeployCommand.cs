﻿using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public class RoverDeployCommand : IRoverDeployCommand
    {
        public GridPoint PlateauPoint { get; set; }
        public CardinalDirection CardinalDirection { get; set; }
        private IRover rover;
        private IPlateau plateau;

        public RoverDeployCommand(GridPoint aPlateauPoint, CardinalDirection aCardinalDirection)
        {
            PlateauPoint = aPlateauPoint;
            CardinalDirection = aCardinalDirection;
        }

        public CommandType GetCommandType()
        {
            return CommandType.RoverDeployCommand;
        }

        public void Execute()
        {
            rover.Deploy(plateau, PlateauPoint, CardinalDirection);
        }

        public void SetReceivers(IRover aRover, IPlateau aPlateau)
        {
            rover = aRover;
            plateau = aPlateau;
        }
    }
}