using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Report;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Tests.Report
{
    public class ConsoleReportComposerTests
    {
        [TestFixture]
        public class ConsoleReportComposer_Compose
        {
            [TestCase(1, 2, CardinalDirection.North, "1 2 N")]
            [TestCase(3, 4, CardinalDirection.East, "3 4 E")]
            [TestCase(5, 6, CardinalDirection.South, "5 6 S")]
            [TestCase(7, 8, CardinalDirection.West, "7 8 W")]
            public void Should_compose_arguments_into_expected_report_format(int coordinateX, int coordinateY, CardinalDirection cardinalDirection, string expectedReport)
            {
                var point = new Point(coordinateX, coordinateY);

                var reportComposer = new ConsoleReportComposer();
                var report = reportComposer.Compose(point, cardinalDirection);
                Assert.AreEqual(expectedReport, report);
            }
        }

        [TestFixture]
        public class ConsoleReportComposer_CompileReports
        {
            [Test]
            public void When_any_Rover_not_deployed_should_throw_ReportException()
            {
                var mockRover = new Mock<IRover>();
                mockRover.Setup(x => x.IsDeployed()).Returns(false);
                var rovers = new List<IRover> {mockRover.Object};

                var reportComposer = new ConsoleReportComposer();
                
                Assert.Throws<ReportException>(() => reportComposer.CompileReports(rovers));
            }

            [Test]
            public void When_all_Rovers_deployed_should_request_Rover_Positions_and_CardinalDirections()
            {
                var mockRover = new Mock<IRover>();
                mockRover.Setup(x => x.IsDeployed()).Returns(true);
                var rovers = new List<IRover> {mockRover.Object, mockRover.Object, mockRover.Object};

                var reportComposer = new ConsoleReportComposer();
                reportComposer.CompileReports(rovers);
                    
                mockRover.VerifyGet(x => x.Position, Times.Exactly(3));
                mockRover.VerifyGet(x => x.CardinalDirection, Times.Exactly(3));
            }
        }
    }
}
