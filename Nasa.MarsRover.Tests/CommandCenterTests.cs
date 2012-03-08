using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.Command;
using Nasa.MarsRover.Command.Interpret;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests
{
    public class CommandCenterTests
    {
        [TestFixture]
        public class CommandCenter_Interpret
        {
            [Test]
            public void Assigns_command_list_from_CommandParser_to_CommandInvoker_and_Invokes()
            {
                var expectedInvocationList = new ICommand[] { new GridSizeCommand(new GridSize(1, 2)) };
                var mockCommandParser = new Mock<ICommandParser>();
                mockCommandParser.Setup(x => x.Parse(null)).Returns(expectedInvocationList);

                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(null, mockCommandParser.Object, mockCommandInvoker.Object, null);
                commandCenter.Execute(null);

                mockCommandInvoker.Verify(x => x.Assign(expectedInvocationList), Times.Once());
                mockCommandInvoker.Verify(x => x.InvokeAll(), Times.Once());
            }

            [Test]
            public void Assigns_Plateau_to_CommandInvoker_for_use_in_commands()
            {
                var mockPlateau = new Mock<IPlateau>();
                var mockCommandParser = new Mock<ICommandParser>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(mockPlateau.Object, mockCommandParser.Object, mockCommandInvoker.Object, null);
                commandCenter.Execute(null);

                mockCommandInvoker.Verify(x => x.SetPlateau(mockPlateau.Object), Times.Once());
            }

            [Test]
            public void Assigns_a_rover_list_to_CommandInvoker_for_use_in_commands()
            {
                var mockCommandParser = new Mock<ICommandParser>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var commandCenter = new CommandCenter(null, mockCommandParser.Object, mockCommandInvoker.Object, null);
                commandCenter.Execute(null);

                mockCommandInvoker.Verify(x => x.SetRovers(It.IsAny<IList<IRover>>()), Times.Once());   
            }
        }

        [TestFixture]
        public class CommandCenter_GetPlateau
        {
            [Test]
            public void Returns_value_from_invocation_of_Plateau_GetPlateau()
            {
                var expectedPlateau = new Mock<IPlateau>();
                var commandCenter = new CommandCenter(expectedPlateau.Object, null, null, null);

                var plateau = commandCenter.GetPlateau();

                Assert.AreEqual(expectedPlateau, plateau);
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
                mockReportComposer.Setup(x => x.CompileReports(It.IsAny<List<IRover>>()))
                    .Returns(expectedReport);

                var commandCenter = new CommandCenter(null, null, null, mockReportComposer.Object);
                var report = commandCenter.GetCombinedRoverReport();
                Assert.AreEqual(expectedReport, report);
            }
        }
    }
}
