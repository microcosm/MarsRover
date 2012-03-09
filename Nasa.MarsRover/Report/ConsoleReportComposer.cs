using System.Collections.Generic;
using System.Text;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Report
{
    public class ConsoleReportComposer : IReportComposer
    {
        private readonly IDictionary<CardinalDirection, char> cardinalDirectionDictionary;

        public ConsoleReportComposer()
        {
            cardinalDirectionDictionary = new Dictionary<CardinalDirection, char>
            {
                 {CardinalDirection.North, 'N'},
                 {CardinalDirection.South, 'S'},
                 {CardinalDirection.East, 'E'},
                 {CardinalDirection.West, 'W'}
            };
        }

        public string Compose(Point aPoint, CardinalDirection aCardinalDirection)
        {
            var reportItem1 = aPoint.X;
            var reportItem2 = aPoint.Y;
            var reportItem3 = cardinalDirectionDictionary[aCardinalDirection];
            var report = new StringBuilder();
            report.AppendFormat("{0} {1} {2}", reportItem1, reportItem2, reportItem3);
            return report.ToString();
        }

        public string CompileReports(IEnumerable<IRover> rovers)
        {
            var reports = composeEachReport(rovers);
            return convertToString(reports);
        }

        private StringBuilder composeEachReport(IEnumerable<IRover> rovers)
        {
            var reports = new StringBuilder();
            foreach (var rover in rovers)
            {
                ensureRoverIsDeployed(rover);
                var report = Compose(rover.Position, rover.CardinalDirection);
                reports.AppendLine(report);
            }
            return reports;
        }

        private static string convertToString(StringBuilder reports)
        {
            return reports.ToString()
                .TrimEnd('\n', '\r');
        }

        private static void ensureRoverIsDeployed(IRover rover)
        {
            if(!rover.IsDeployed())
            {
                throw new ReportException("Cannot create report because one or more rovers are not deployed");
            }
        }
    }
}
