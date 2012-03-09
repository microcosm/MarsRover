using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Tests.Command
{
    public class GridSizeCommandTests
    {
        [TestFixture]
        public class GridSizeCommand_Constructor
        {
            [Test]
            public void Given_a_size_argument_exposes_as_public_property()
            {
                var size = new Size(1, 2);
                var gridSizeCommand = new GridSizeCommand(size);
                Assert.AreEqual(size, gridSizeCommand.Size);
            }
        }

        [TestFixture]
        public class GridSizeCommand_SetReceiver
        {
            [Test]
            public void Should_accept_Receiver_argument()
            {
                var anySize = new Size(0, 0);
                var mockLandingSurface = new Mock<ILandingSurface>();
                var gridSizeCommand = new GridSizeCommand(anySize);
                Assert.DoesNotThrow(() =>
                    gridSizeCommand.SetReceiver(mockLandingSurface.Object));
            }
        }

        [TestFixture]
        public class GridSizeCommand_Execute
        {
            [Test]
            public void Should_set_LandingSurface_size()
            {
                var mockLandingSurface = new Mock<ILandingSurface>();
                var anySize = new Size(0, 0);
                var gridSizeCommand = new GridSizeCommand(anySize);
                gridSizeCommand.SetReceiver(mockLandingSurface.Object);

                gridSizeCommand.Execute();

                mockLandingSurface.Verify(x => x.SetSize(anySize), Times.Once());
            }
        }

        [TestFixture]
        public class GridSizeCommand_GetCommandType
        {
            [Test]
            public void Should_return_GridSizeCommand_type()
            {
                var size = new Size(0, 0);
                var gridSizeCommand = new GridSizeCommand(size);
                Assert.AreEqual(gridSizeCommand.GetCommandType(), CommandType.GridSizeCommand);
            }
        }
    }
}
