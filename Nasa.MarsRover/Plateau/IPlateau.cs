namespace Nasa.MarsRover.Plateau
{
    public interface IPlateau
    {
        void SetSize(GridSize plateauSize);
        GridSize GetSize();
        bool IsValid(GridPoint plateauPoint);
    }
}