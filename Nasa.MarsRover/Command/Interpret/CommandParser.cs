using System;
using System.Collections.Generic;
using System.Linq;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Command.Interpret
{
    public class CommandParser : ICommandParser
    {
        private readonly ICommandFactory commandFactory;
        private readonly ICommandMatcher commandMatcher;
        private readonly IDictionary<CommandType, Func<string, ICommand>> commandParserDictionary;
        private readonly IDictionary<char, CardinalDirection> cardinalDirectionDictionary;

        public CommandParser(ICommandMatcher aCommandMatcher, ICommandFactory aCommandFactory)
        {
            commandMatcher = aCommandMatcher;
            commandFactory = aCommandFactory;
            commandParserDictionary = new Dictionary<CommandType, Func<string, ICommand>>
            {
                 {CommandType.LandingSurfaceSizeCommand, ParseLandingSurfaceSizeCommand},
                 {CommandType.RoverDeployCommand, ParseRoverDeployCommand}
            };
            cardinalDirectionDictionary = new Dictionary<char, CardinalDirection>
            {
                 {'N', CardinalDirection.North},
                 {'S', CardinalDirection.South},
                 {'E', CardinalDirection.East},
                 {'W', CardinalDirection.West}
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
            
            var populatedCommand = commandFactory.CreateLandingSurfaceSizeCommand(width, height);
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

            var populatedCommand = commandFactory.CreateRoverDeployCommand(deployPoint, deployDirection);
            return populatedCommand;
        }
    }
}