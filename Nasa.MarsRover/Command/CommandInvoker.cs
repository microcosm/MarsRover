using System;
using System.Collections.Generic;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Command
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly IRoverFactory roverFactory;
        private readonly IDictionary<CommandType, Action<ICommand>> setReceiversMethodDictionary;

        private ILandingSurface landingSurface;
        private IList<IRover> rovers;
        private IEnumerable<ICommand> commandList;

        public CommandInvoker(IRoverFactory aRoverFactory)
        {
            roverFactory = aRoverFactory;
            
            setReceiversMethodDictionary = new Dictionary<CommandType, Action<ICommand>>
            {
                {CommandType.LandingSurfaceSizeCommand, SetReceiversOnLandingSurfaceSizeCommand},
                {CommandType.RoverDeployCommand, SetReceiversOnRoverDeployCommand}
            };
        }

        public void SetLandingSurface(ILandingSurface aLandingSurface)
        {
            landingSurface = aLandingSurface;
        }

        public void SetRovers(IList<IRover> someRovers)
        {
            rovers = someRovers;
        }

        public void Assign(IEnumerable<ICommand> aCommandList)
        {
            commandList = aCommandList;
        }

        public void InvokeAll()
        {
            foreach (var command in commandList)
            {
                setReceivers(command);
                command.Execute();
            }
        }

        private void setReceivers(ICommand command)
        {
            setReceiversMethodDictionary[command.GetCommandType()]
                .Invoke(command);
        }

        private void SetReceiversOnLandingSurfaceSizeCommand(ICommand command)
        {
            var landingSurfaceSizeCommand = (ILandingSurfaceSizeCommand) command;
            landingSurfaceSizeCommand.SetReceiver(landingSurface);
        }

        private void SetReceiversOnRoverDeployCommand(ICommand command)
        {
            var roverDeployCommand = (IRoverDeployCommand) command;
            var newRover = roverFactory.CreateRover();
            rovers.Add(newRover);
            roverDeployCommand.SetReceivers(newRover, landingSurface);
        }
    }
}