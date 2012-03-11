using System;
using System.Collections.Generic;
using System.Linq;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command.Interpret
{
    public class CommandParser : ICommandParser
    {
        private readonly Func<Size, ILandingSurfaceSizeCommand> landingSurfaceSizeCommandFactory;
        private readonly Func<Point, CardinalDirection, IRoverDeployCommand> roverDeployCommandFactory;
        private readonly Func<IList<Movement>, IRoverExploreCommand> roverExploreCommandFactory;

        private readonly ICommandMatcher commandMatcher;
        private readonly IDictionary<CommandType, Func<string, ICommand>> commandParserDictionary;
        private readonly IDictionary<char, CardinalDirection> cardinalDirectionDictionary;
        private readonly IDictionary<char, Movement> movementDictionary;

        public CommandParser(ICommandMatcher aCommandMatcher, 
            Func<Size, ILandingSurfaceSizeCommand> aLandingSurfaceSizeCommandFactory, 
            Func<Point, CardinalDirection, IRoverDeployCommand> aRoverDeployCommandFactory, 
            Func<IList<Movement>, IRoverExploreCommand> aRoverExploreCommandFactory)
        {
            commandMatcher = aCommandMatcher;
            landingSurfaceSizeCommandFactory = aLandingSurfaceSizeCommandFactory;
            roverDeployCommandFactory = aRoverDeployCommandFactory;
            roverExploreCommandFactory = aRoverExploreCommandFactory;

            commandParserDictionary = new Dictionary<CommandType, Func<string, ICommand>>
            {
                 {CommandType.LandingSurfaceSizeCommand, ParseLandingSurfaceSizeCommand},
                 {CommandType.RoverDeployCommand, ParseRoverDeployCommand},
                 {CommandType.RoverExploreCommand, ParseRoverExploreCommand}
            };

            cardinalDirectionDictionary = new Dictionary<char, CardinalDirection>
            {
                 {'N', CardinalDirection.North},
                 {'S', CardinalDirection.South},
                 {'E', CardinalDirection.East},
                 {'W', CardinalDirection.West}
            };

            movementDictionary = new Dictionary<char, Movement>
            {
                 {'L', Movement.Left},
                 {'R', Movement.Right},
                 {'M', Movement.Forward}
            };
        }

        public IEnumerable<ICommand> Parse(string commandString)
        {
            var commands = commandString.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return commands.Select(
                command => commandParserDictionary[commandMatcher.GetCommandType(command)]
                    .Invoke(command)).ToList();
        }

        private ICommand ParseLandingSurfaceSizeCommand(string toParse)
        {
            var arguments = toParse.Split(' ');
            var width = int.Parse(arguments[0]);
            var height = int.Parse(arguments[1]);
            var size = new Size(width, height);

            var populatedCommand = landingSurfaceSizeCommandFactory(size);
            return populatedCommand;
        }

        private ICommand ParseRoverDeployCommand(string toParse)
        {
            var arguments = toParse.Split(' ');
            
            var deployX = int.Parse(arguments[0]);
            var deployY = int.Parse(arguments[1]);

            var directionSignifier = arguments[2][0];
            var deployDirection = cardinalDirectionDictionary[directionSignifier];

            var deployPoint = new Point(deployX, deployY);

            var populatedCommand = roverDeployCommandFactory(deployPoint, deployDirection);
            return populatedCommand;
        }

        private ICommand ParseRoverExploreCommand(string toParse)
        {
            var arguments = toParse.ToCharArray();
            var movements = arguments.Select(argument => movementDictionary[argument]).ToList();
            var populatedCommand = roverExploreCommandFactory(movements);
            return populatedCommand;
        }
    }
}