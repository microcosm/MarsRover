using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover
{
    public interface ICommandCenter
    {
        void Execute(string commandString);
        ILandingSurface GetLandingSurface();
        string GetCombinedRoverReport();
    }
}
