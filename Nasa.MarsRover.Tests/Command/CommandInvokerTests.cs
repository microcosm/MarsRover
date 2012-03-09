using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.LandingSurface;
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
        public class CommandInvoker_SetLandingSurface
        {
            [Test]
            public void Accepts_new_LandingSurface()
            {
                var mockLandingSurface = new Mock<ILandingSurface>();
                var commandInvoker = new CommandInvoker(null);
                Assert.DoesNotThrow(() =>
                    commandInvoker.SetLandingSurface(mockLandingSurface.Object));
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
            public void When_executing_LandingSurfaceSizeCommand_sets_LandingSurface_as_command_receiver()
            {
                var mockLandingSurface = new Mock<ILandingSurface>();
                var landingSurfaceSizeCommand = new Mock<ILandingSurfaceSizeCommand>();
                landingSurfaceSizeCommand.Setup(x => x.GetCommandType()).Returns(CommandType.LandingSurfaceSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] { landingSurfaceSizeCommand.Object });
                commandInvoker.SetLandingSurface(mockLandingSurface.Object);

                commandInvoker.InvokeAll();
                
                landingSurfaceSizeCommand.Verify(x => x.SetReceiver(mockLandingSurface.Object), Times.Once());
            }

            [Test]
            public void When_executing_RoverDeployCommand_sets_LandingSurface_and_new_Rover_as_command_receivers()
            {
                var mockRover = new Mock<IRover>();
                var mockLandingSurface = new Mock<ILandingSurface>();
                var mockRoverDeployCommand = new Mock<IRoverDeployCommand>();
                mockRoverDeployCommand.Setup(x => x.GetCommandType()).Returns(CommandType.RoverDeployCommand);

                var mockRoverFactory = new Mock<IRoverFactory>();
                mockRoverFactory.Setup(x => x.CreateRover()).Returns(mockRover.Object);

                var commandInvoker = new CommandInvoker(mockRoverFactory.Object);
                commandInvoker.Assign(new[]{mockRoverDeployCommand.Object});
                commandInvoker.SetLandingSurface(mockLandingSurface.Object);
                commandInvoker.SetRovers(new List<IRover>());

                commandInvoker.InvokeAll();

                mockRoverDeployCommand.Verify(x => x.SetReceivers(mockRover.Object, mockLandingSurface.Object), Times.Once());
            }

            [Test]
            public void Invokes_Execute_for_each_command()
            {
                var mockLandingSurfaceSizeCommand = new Mock<ILandingSurfaceSizeCommand>();
                mockLandingSurfaceSizeCommand.Setup(x => x.GetCommandType()).Returns(CommandType.LandingSurfaceSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] { mockLandingSurfaceSizeCommand.Object, mockLandingSurfaceSizeCommand.Object, mockLandingSurfaceSizeCommand.Object });

                commandInvoker.InvokeAll();
                
                mockLandingSurfaceSizeCommand.Verify(x => x.Execute(), Times.Exactly(3));
            }
        }
    }
}
