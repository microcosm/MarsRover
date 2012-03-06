using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover
{
    public interface ICommandCenter
    {
        GridSize GetPlateauSize();
        string GetCombinedRoverReport();
        void Execute(string commandString);
    }
}
