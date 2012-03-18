using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests
{
    public class CommandCenterTests
    {
        [TestFixture]
        public class CommandCenter_Constructor
        {
            [Test]
            public void Assigns_LandingSurface_to_CommandInvoker_for_use_in_commands()
            {
                var mockLandingSurface = new Mock<ILandingSurface>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(mockLandingSurface.Object, null, mockCommandInvoker.Object, null);
                
                mockCommandInvoker.Verify(x => x.SetLandingSurface(mockLandingSurface.Object), Times.Once());
            }

            [Test]
            public void Assigns_a_rover_list_to_CommandInvoker_for_use_in_commands()
            {
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(null, null, mockCommandInvoker.Object, null);
                
                mockCommandInvoker.Verify(x => x.SetRovers(It.IsAny<IList<IRover>>()), Times.Once());
            }
        }

        [TestFixture]
        public class CommandCenter_Execute
        {
            [Test]
            public void Parses_command_string_and_invokes_all_commands()
            {
                var expectedInvocationList = new ICommand[]{};
                var mockCommandParser = new Mock<ICommandParser>();
                mockCommandParser.Setup(x => x.Parse(null)).Returns(expectedInvocationList);

                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(null, mockCommandParser.Object, mockCommandInvoker.Object, null);
                commandCenter.Execute(null);

                mockCommandInvoker.Verify(x => x.Assign(expectedInvocationList), Times.Once());
                mockCommandInvoker.Verify(x => x.InvokeAll(), Times.Once());
            }
        }

        [TestFixture]
        public class CommandCenter_GetLandingSurface
        {
            [Test]
            public void Returns_injected_LandingSurface()
            {
                var expectedLandingSurface = new Mock<ILandingSurface>();
                var mockCommandinvoker = new Mock<ICommandInvoker>();
                var commandCenter = new CommandCenter(expectedLandingSurface.Object, null, mockCommandinvoker.Object, null);

                var landingSurface = commandCenter.GetLandingSurface();

                Assert.AreEqual(expectedLandingSurface.Object, landingSurface);
            }
        }

        [TestFixture]
        public class CommandCenter_GetCombinedRoverReport
        {
            [Test]
            public void Invokes_ReportComposer_CompileReports()
            {
                const string expectedReport = "any";
                var mockReportComposer = new Mock<IReportComposer>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();
                mockReportComposer.Setup(x => x.CompileReports(It.IsAny<List<IRover>>()))
                    .Returns(expectedReport);

                var commandCenter = new CommandCenter(null, null, mockCommandInvoker.Object, mockReportComposer.Object);
                var report = commandCenter.GetCombinedRoverReport();
                Assert.AreEqual(expectedReport, report);
            }
        }
    }
}
