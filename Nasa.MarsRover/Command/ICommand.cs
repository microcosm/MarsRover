namespace Nasa.MarsRover.Command
{
    public interface ICommand
    {
        CommandType GetCommandType();
        void Execute();
    }
}
