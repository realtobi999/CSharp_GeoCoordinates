using GeoCoordinates.Core.GPX;

namespace GeoCoordinates.Core.Interfaces;

/// <summary>
/// The IGpxProcessor interface provides a method for processing a GPX document to extract gpx data.
/// </summary>
public interface IGpxProcessor
{
    /// <summary>
    /// Extracts coordinates from a GPX document and returns them as an IEnumerable of Coordinate objects.
    /// </summary>
    /// <param name="gpx">The GPX document to process.</param>
    /// <returns>An IEnumerable of Coordinate objects containing the latitude, longitude, and elevation data.</returns>
    IEnumerable<Coordinate> GetCoordinates(Gpx gpx);
}
