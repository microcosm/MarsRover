namespace Nasa.MarsRover.LandingSurface
{
    public interface ILandingSurface
    {
        void SetSize(Size aSize);
        Size GetSize();
        bool IsValid(Point aPoint);
    }
}