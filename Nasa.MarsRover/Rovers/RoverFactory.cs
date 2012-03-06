using StructureMap;

namespace Nasa.MarsRover.Rovers
{
    public class RoverFactory : IRoverFactory
    {
        public IRover CreateRover()
        {
            return ObjectFactory.GetInstance<IRover>();
        }
    }
}
