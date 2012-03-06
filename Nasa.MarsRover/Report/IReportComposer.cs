using System.Collections.Generic;
using Nasa.MarsRover.Plateau;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Report
{
    public interface IReportComposer
    {
        string Compose(GridPoint aPlateauPoint, CardinalDirection aCardinalDirection);
        string CompileReports(IEnumerable<IRover> rovers);
    }
}
