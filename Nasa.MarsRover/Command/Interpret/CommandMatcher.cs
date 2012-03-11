using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Nasa.MarsRover.Command.Interpret
{
    public class CommandMatcher : ICommandMatcher
    {
        private IDictionary<string, CommandType> commandTypeDictionary;
        
        public CommandMatcher()
        {
            InitializeCommandTypeDictionary();
        }

        public CommandType GetCommandType(string command)
        {
            try
            {
                var commandType = commandTypeDictionary.First(
                    regexToCommandType => new Regex(regexToCommandType.Key).IsMatch(command));

                return commandType.Value;
            }
            catch(InvalidOperationException e)
            {
                var exceptionMessage = String.Format("String '{0}' is not a valid command", command);
                throw new CommandException(exceptionMessage, e);
            }
        }

        private void InitializeCommandTypeDictionary()
        {
            commandTypeDictionary = new Dictionary<string, CommandType>
            {
                { @"^\d+ \d+$", CommandType.LandingSurfaceSizeCommand },
                { @"^\d+ \d+ [NSEW]$", CommandType.RoverDeployCommand },
                { @"^[LRM]+$", CommandType.RoverExploreCommand }
            };
        }
    }
}
