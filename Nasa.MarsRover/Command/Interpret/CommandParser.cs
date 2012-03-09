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
                 {CommandType.GridSizeCommand, ParseGridSizeCommand},
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

        private ICommand ParseGridSizeCommand(string toParse)
        {
            var arguments = toParse.Split(' ');
            var width = int.Parse(arguments[0]);
            var height = int.Parse(arguments[1]);
            
            var populatedCommand = commandFactory.CreateGridSizeCommand(width, height);
            return populatedCommand;
        }

        private ICommand ParseRoverDeployCommand(string toParse)
        {
            var arguments = toParse.Split(' ');
            
            var deployX = int.Parse(arguments[0]);
            var deployY = int.Parse(arguments[1]);

            var cardinalDirectionSignifier = arguments[2][0];
            var cardinalDirection = cardinalDirectionDictionary[cardinalDirectionSignifier];

            var plateauPoint = new GridPoint(deployX, deployY);

            var populatedCommand = commandFactory.CreateRoverDeployCommand(plateauPoint, cardinalDirection);
            return populatedCommand;
        }
    }
}