namespace Nasa.MarsRover.Command.Interpret
{
    public interface ICommandMatcher
    {
        CommandType GetCommandType(string command);
    }
}