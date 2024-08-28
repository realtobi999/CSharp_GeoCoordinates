using GeoCoordinates.Core.GPX;

namespace GeoCoordinates.Core.Interfaces;

/// <summary>
/// The IGpxProcessor interface provides a method for processing a GPX document to extract gpx data.
/// </summary>
public interface IGpxProcessor
{
    /// <summary>
    /// Extracts waypoints from a GPX document and returns them as an IEnumerable of Coordinate objects.
    /// </summary>
    /// <param name="gpx">The GPX document to process.</param>
    /// <returns>An IEnumerable of Coordinate objects containing the latitude, longitude, and elevation data.</returns>
    IEnumerable<Coordinate> ParseWaypoints(Gpx gpx);

    /// <summary>
    /// Extracts tracks from a GPX document and returns them as an IEnumerable of CoordinatePath objects.
    /// </summary>
    /// <param name="gpx">The GPX document to process.</param>
    /// <returns>An IEnumerable of CoordinatePath objects containing segments of coordinates.</returns>
    IEnumerable<CoordinatePath> ParseTracks(Gpx gpx);
}
