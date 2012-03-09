namespace Nasa.MarsRover.LandingSurface
{
    public class Plateau : ILandingSurface
    {
        private GridSize gridSize { get; set; }

        public void SetSize(GridSize plateauSize)
        {
            gridSize = plateauSize;
        }

        public GridSize GetSize()
        {
            return gridSize;
        }

        public bool IsValid(Point aPoint)
        {
            var isValidX = aPoint.X >= 0 && aPoint.X <= gridSize.Width;
            var isValidY = aPoint.Y >= 0 && aPoint.Y <= gridSize.Height;
            return isValidX && isValidY;
        }
    }
}
