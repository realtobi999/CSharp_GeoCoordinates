using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

/// <summary>
/// The GpxProcessor class implements the IGpxProcessor interface and processes GPX documents to extract gpx coordinate data
/// </summary>
public class GpxProcessor : IGpxProcessor
{
    /// <inheritdoc/>
    public IEnumerable<Coordinate> ParseWaypoints(Gpx gpx)
    {
        var waypoints = gpx.Document.Descendants(gpx.Namespace + "wpt")
                                    .Select(waypoint => GpxProcessor.ParseWaypoints(gpx, waypoint))
                                    .Where(waypoint => waypoint is not null);

        // Yield return coordinates
        foreach (var waypoint in waypoints)
        {
            yield return new Coordinate(
                waypoint!.Latitude,
                waypoint.Longitude,
                waypoint.Elevation
            );
        }
    }

    /// <inheritdoc/>
    public IEnumerable<CoordinatePath> ParseTracks(Gpx gpx)
    {
        var tracks = gpx.Document.Descendants(gpx.Namespace + "trk")
                                 .Select(track => track.Descendants(gpx.Namespace + "trkpt")
                                                       .Select(trackpoint => GpxProcessor.ParseWaypoints(gpx, trackpoint))
                                                       .Where(waypoint => waypoint != null)
                                 );

        foreach (var track in tracks)
        {
            yield return new CoordinatePath(track.Select(waypoint => new Coordinate(waypoint!.Latitude, waypoint.Longitude, waypoint.Elevation)));
        }
    }

    private static dynamic? ParseWaypoints(Gpx gpx, System.Xml.Linq.XElement trackpoint)
    {
        var latitude = trackpoint.Attribute("lat")?.Value;
        var longitude = trackpoint.Attribute("lon")?.Value;
        var elevation = trackpoint.Element(gpx.Namespace + "ele")?.Value;

        if (latitude == null || longitude == null || elevation == null)
        {
            return null;
        }

        return new
        {
            Latitude = double.Parse(latitude),
            Longitude = double.Parse(longitude),
            Elevation = double.Parse(elevation),
        };
    }
}
