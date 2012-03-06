using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Plateau;

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
                var size = new GridSize(1, 2);
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
                var anyGridSize = new GridSize(0, 0);
                var mockPlateau = new Mock<IPlateau>();
                var gridSizeCommand = new GridSizeCommand(anyGridSize);
                Assert.DoesNotThrow(() =>
                    gridSizeCommand.SetReceiver(mockPlateau.Object));
            }
        }

        [TestFixture]
        public class GridSizeCommand_Execute
        {
            [Test]
            public void Should_set_Plateau_size()
            {
                var mockPlateau = new Mock<IPlateau>();
                var anySize = new GridSize(0, 0);
                var gridSizeCommand = new GridSizeCommand(anySize);
                gridSizeCommand.SetReceiver(mockPlateau.Object);

                gridSizeCommand.Execute();

                mockPlateau.Verify(x => x.SetSize(anySize), Times.Once());
            }
        }

        [TestFixture]
        public class GridSizeCommand_GetCommandType
        {
            [Test]
            public void Should_return_GridSizeCommand_type()
            {
                var size = new GridSize(0, 0);
                var gridSizeCommand = new GridSizeCommand(size);
                Assert.AreEqual(gridSizeCommand.GetCommandType(), CommandType.GridSizeCommand);
            }
        }
    }
}
