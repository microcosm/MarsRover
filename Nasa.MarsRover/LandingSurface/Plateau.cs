namespace Nasa.MarsRover.LandingSurface
{
    public class Plateau : ILandingSurface
    {
        private Size size { get; set; }

        public void SetSize(Size aSize)
        {
            size = aSize;
        }

        public Size GetSize()
        {
            return size;
        }

        public bool IsValid(Point aPoint)
        {
            var isValidX = aPoint.X >= 0 && aPoint.X <= size.Width;
            var isValidY = aPoint.Y >= 0 && aPoint.Y <= size.Height;
            return isValidX && isValidY;
        }
    }
}
