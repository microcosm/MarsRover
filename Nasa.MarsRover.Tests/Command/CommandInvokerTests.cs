using System;
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
                var expectedLandingSurface = new Mock<ILandingSurface>();
                var landingSurfaceSizeCommand = new Mock<ILandingSurfaceSizeCommand>();
                landingSurfaceSizeCommand.Setup(x => x.GetCommandType()).Returns(CommandType.LandingSurfaceSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] { landingSurfaceSizeCommand.Object });
                commandInvoker.SetLandingSurface(expectedLandingSurface.Object);

                commandInvoker.InvokeAll();
                
                landingSurfaceSizeCommand.Verify(
                    x => x.SetReceiver(expectedLandingSurface.Object), Times.Once());
            }

            [Test]
            public void When_executing_RoverDeployCommand_sets_LandingSurface_and_new_Rover_as_command_receivers()
            {
                var expectedRover = new Mock<IRover>();
                var expectedLandingSurface = new Mock<ILandingSurface>();
                
                var mockRoverDeployCommand = new Mock<IRoverDeployCommand>();
                mockRoverDeployCommand.Setup(x => x.GetCommandType()).Returns(CommandType.RoverDeployCommand);

                Func<IRover> mockRoverFactory = () => expectedRover.Object;

                var commandInvoker = new CommandInvoker(mockRoverFactory);
                commandInvoker.Assign(new[]{mockRoverDeployCommand.Object});
                commandInvoker.SetLandingSurface(expectedLandingSurface.Object);
                commandInvoker.SetRovers(new List<IRover>());

                commandInvoker.InvokeAll();

                mockRoverDeployCommand.Verify(
                    x => x.SetReceivers(expectedRover.Object, expectedLandingSurface.Object), Times.Once());
            }

            [Test]
            public void When_executing_RoverExploreCommand_sets_LandingSurface_and_most_recently_added_Rover_as_command_receivers()
            {
                var expectedRover = new Mock<IRover>();
                var expectedLandingSurface = new Mock<ILandingSurface>();
                
                var mockRoverExploreCommand = new Mock<IRoverExploreCommand>();
                mockRoverExploreCommand.Setup(x => x.GetCommandType()).Returns(CommandType.RoverExploreCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[]{mockRoverExploreCommand.Object});
                commandInvoker.SetLandingSurface(expectedLandingSurface.Object);
                commandInvoker.SetRovers(new List<IRover>{ null, expectedRover.Object });

                commandInvoker.InvokeAll();

                mockRoverExploreCommand.Verify(
                    x => x.SetReceivers(expectedRover.Object, expectedLandingSurface.Object), Times.Once());
            }

            [Test]
            public void Invokes_Execute_for_each_command()
            {
                var mockCommand = new Mock<ILandingSurfaceSizeCommand>();
                mockCommand.Setup(x => x.GetCommandType()).Returns(CommandType.LandingSurfaceSizeCommand);

                var commandInvoker = new CommandInvoker(null);
                commandInvoker.Assign(new[] {mockCommand.Object, mockCommand.Object, mockCommand.Object});

                commandInvoker.InvokeAll();
                
                mockCommand.Verify(x => x.Execute(), Times.Exactly(3));
            }
        }
    }
}
