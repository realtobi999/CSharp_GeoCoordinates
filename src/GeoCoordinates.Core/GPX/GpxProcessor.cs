using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

public class GpxProcessor : IGpxProcessor
{
    public IEnumerable<Coordinate> GetCoordinates(Gpx gpx)
    {
        var waypoints = gpx.Document.Descendants(gpx.Namespace + "wpt")
                                    .Select(waypoint =>
                                    {
                                        var latitude = waypoint.Attribute("lat")?.Value;
                                        var longitude = waypoint.Attribute("lon")?.Value;
                                        var elevation = waypoint.Element(gpx.Namespace + "ele")?.Value;

                                        if (latitude is null || longitude is null || elevation is null)
                                        {
                                            return null;
                                        }

                                        // Return an anonymous type with the correctly parsed values
                                        return new
                                        {
                                            Latitude = double.Parse(latitude),
                                            Longitude = double.Parse(longitude),
                                            Elevation = double.Parse(elevation),
                                        };
                                    })
                                    .Where(waypoint => waypoint is not null);

        // Yield return coordinates
        foreach (var waypoint in waypoints)
        {
            yield return new Coordinate(waypoint!.Latitude, waypoint.Longitude, waypoint.Elevation);
        }
    }
}
