using GeoCoordinates.Core.Extensions;

namespace GeoCoordinates.Core.Helpers;

internal class CoordinateMath
{
    public static double Haversine(Coordinate coordinate1, Coordinate coordinate2)
    {
        const double r = 6378100; // radius of earth in meters

        var lat1 = coordinate1.Latitude.ToRadians();
        var lon1 = coordinate1.Longitude.ToRadians();
        var lat2 = coordinate2.Latitude.ToRadians();
        var lon2 = coordinate2.Longitude.ToRadians();

        var sdlat = Math.Sin((lat2 - lat1) / 2);
        var sdlon = Math.Sin((lon2 - lon1) / 2);

        return 2 * r * Math.Asin(Math.Sqrt((double)(sdlat * sdlat + Math.Cos(lat1) * Math.Cos(lat2) * sdlon * sdlon)));
    }
}
