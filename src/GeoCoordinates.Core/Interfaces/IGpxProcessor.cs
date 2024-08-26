using GeoCoordinates.Core.GPX;

namespace GeoCoordinates.Core.Interfaces;

public interface IGpxProcessor
{
    IEnumerable<Coordinate> GetCoordinates(Gpx gpx);

}
