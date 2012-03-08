using Nasa.MarsRover.Plateau;

namespace Nasa.MarsRover
{
    public interface ICommandCenter
    {
        void Execute(string commandString);
        IPlateau GetPlateau();
        string GetCombinedRoverReport();
    }
}
