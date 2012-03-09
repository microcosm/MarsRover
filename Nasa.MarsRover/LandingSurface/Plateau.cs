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

        public bool IsValid(GridPoint plateauPoint)
        {
            var isValidX = plateauPoint.X >= 0 && plateauPoint.X <= gridSize.Width;
            var isValidY = plateauPoint.Y >= 0 && plateauPoint.Y <= gridSize.Height;
            return isValidX && isValidY;
        }
    }
}
