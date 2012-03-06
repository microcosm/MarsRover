using System;

namespace Nasa.MarsRover.Rovers
{
    [Serializable]
    public class RoverDeployException : Exception
    {
        public RoverDeployException(string message) : base(message) { }
    }
}