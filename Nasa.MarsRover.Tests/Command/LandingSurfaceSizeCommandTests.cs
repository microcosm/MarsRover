using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.LandingSurface;

namespace Nasa.MarsRover.Tests.Command
{
    public class LandingSurfaceSizeCommandTests
    {
        [TestFixture]
        public class LandingSurfaceSizeCommand_Constructor
        {
            [Test]
            public void Given_a_size_argument_exposes_as_public_property()
            {
                var size = new Size(1, 2);
                var landingSurfaceSizeCommand = new LandingSurfaceSizeCommand(size);
                Assert.AreEqual(size, landingSurfaceSizeCommand.Size);
            }
        }

        [TestFixture]
        public class LandingSurfaceSizeCommand_SetReceiver
        {
            [Test]
            public void Should_accept_Receiver_argument()
            {
                var anySize = new Size(0, 0);
                var mockLandingSurface = new Mock<ILandingSurface>();
                var landingSurfaceSizeCommand = new LandingSurfaceSizeCommand(anySize);
                Assert.DoesNotThrow(() =>
                    landingSurfaceSizeCommand.SetReceiver(mockLandingSurface.Object));
            }
        }

        [TestFixture]
        public class LandingSurfaceSizeCommand_Execute
        {
            [Test]
            public void Should_set_LandingSurface_size()
            {
                var mockLandingSurface = new Mock<ILandingSurface>();
                var anySize = new Size(0, 0);
                var landingSurfaceSizeCommand = new LandingSurfaceSizeCommand(anySize);
                landingSurfaceSizeCommand.SetReceiver(mockLandingSurface.Object);

                landingSurfaceSizeCommand.Execute();

                mockLandingSurface.Verify(x => x.SetSize(anySize), Times.Once());
            }
        }

        [TestFixture]
        public class LandingSurfaceSizeCommand_GetCommandType
        {
            [Test]
            public void Should_return_LandingSurfaceSizeCommand_type()
            {
                var size = new Size(0, 0);
                var landingSurfaceSizeCommand = new LandingSurfaceSizeCommand(size);
                Assert.AreEqual(landingSurfaceSizeCommand.GetCommandType(), CommandType.LandingSurfaceSizeCommand);
            }
        }
    }
}
