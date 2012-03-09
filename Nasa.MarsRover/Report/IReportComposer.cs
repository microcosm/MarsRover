using System.Collections.Generic;
using Nasa.MarsRover.LandingSurface;
using Nasa.MarsRover.Rovers;

namespace Nasa.MarsRover.Report
{
    public interface IReportComposer
    {
        string Compose(GridPoint aPlateauPoint, CardinalDirection aCardinalDirection);
        string CompileReports(IEnumerable<IRover> rovers);
    }
}
