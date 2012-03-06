using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Command
{
    public class CommandInvokerTests
    {
        [TestFixture]
        public class CommandInvoker_Assign
        {
            [Test]
            public void Accepts_new_invocation_list()
            {
                var invocationList = new ICommand[]{};
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.Assign(invocationList));
            }
        }

        [TestFixture]
        public class CommandInvoker_SetPlateau
        {
            [Test]
            public void Accepts_new_Plateau()
            {
                var mockPlateau = new Mock<IPlateau>();
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.SetPlateau(mockPlateau.Object));
            }
        }

        [TestFixture]
        public class CommandInvoker_SetRovers
        {
            [Test]
            public void Accepts_list_of_Rovers()
            {
                var rovers = new IRover[]{};
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() => 
                    commandInvoker.SetRovers(rovers));
            }
        }

        [TestFixture]
        public class CommandInvoker_InvokeAll
        {
            [Test]
            public void When_executing_GridSizeCommand_sets_Plateau_as_command_receiver()
            {
                var mockPlateau = new Mock<IPlateau>();
                var mockGridSizeCommand = new Mock<IGridSizeCommand>();
                mockGridSizeCommand.Setup(x => x.GetCommandType()).Returns(CommandType.GridSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] { mockGridSizeCommand.Object });
                commandInvoker.SetPlateau(mockPlateau.Object);

                commandInvoker.InvokeAll();
                
                mockGridSizeCommand.Verify(x => x.SetReceiver(mockPlateau.Object), Times.Once());
            }

            [Test]
            public void When_executing_RoverDeployCommand_sets_Plateau_and_new_Rover_as_command_receivers()
            {
                var mockRover = new Mock<IRover>();
                var mockPlateau = new Mock<IPlateau>();
                var mockRoverDeployCommand = new Mock<IRoverDeployCommand>();
                mockRoverDeployCommand.Setup(x => x.GetCommandType()).Returns(CommandType.RoverDeployCommand);

                var mockRoverFactory = new Mock<IRoverFactory>();
                mockRoverFactory.Setup(x => x.CreateRover()).Returns(mockRover.Object);

                var commandInvoker = new CommandInvoker(mockRoverFactory.Object);
                commandInvoker.Assign(new[]{mockRoverDeployCommand.Object});
                commandInvoker.SetPlateau(mockPlateau.Object);
                commandInvoker.SetRovers(new List<IRover>());

                commandInvoker.InvokeAll();

                mockRoverDeployCommand.Verify(x => x.SetReceivers(mockRover.Object, mockPlateau.Object), Times.Once());
            }

            [Test]
            public void Invokes_Execute_for_each_command()
            {
                var mockGridSizeCommand = new Mock<IGridSizeCommand>();
                mockGridSizeCommand.Setup(x => x.GetCommandType()).Returns(CommandType.GridSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] { mockGridSizeCommand.Object, mockGridSizeCommand.Object, mockGridSizeCommand.Object });

                commandInvoker.InvokeAll();
                
                mockGridSizeCommand.Verify(x => x.Execute(), Times.Exactly(3));
            }
        }
    }
}
