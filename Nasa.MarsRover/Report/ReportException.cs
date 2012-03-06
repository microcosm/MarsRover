using System;

namespace Nasa.MarsRover.Report
{
    [Serializable]
    public class ReportException : Exception
    {
        public ReportException(string message) : base(message) { }
    }
}