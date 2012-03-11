using System.Collections.Generic;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public interface IRoverExploreCommand : ICommand
    {
        IList<Movement> Movements { get; }
        void SetReceiver(IRover aRover);
    }
}