using System.Collections.Generic;

namespace Nasa.MarsRover.Command.Interpret
{
    public interface ICommandParser
    {
        IEnumerable<ICommand> Parse(string commandString);
    }
}
