namespace GeoCoordinates.Core.Interfaces;

public interface IGpxHandler
{
    CoordinatePath LoadGpx(string filepath);
}
