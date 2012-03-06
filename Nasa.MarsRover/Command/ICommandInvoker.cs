using System.Collections.Generic;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public interface ICommandInvoker
    {
        void SetPlateau(IPlateau aPlateau);
        void SetRovers(IList<IRover> someRovers);
        void Assign(IEnumerable<ICommand> aCommandList);
        void InvokeAll();
    }
}