namespace Nasa.MarsRover.LandingSurface
{
    public interface ILandingSurface
    {
        void SetSize(GridSize plateauSize);
        GridSize GetSize();
        bool IsValid(GridPoint plateauPoint);
    }
}