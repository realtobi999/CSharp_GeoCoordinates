namespace GeoCoordinates.Core.Helpers;

internal static class CoordinatePathHelpers
{
    public static double CalculateDistance(IEnumerable<Coordinate> coordinates)
    {
        var distance = 0.0;

        for (int i = 0; i < coordinates.Count() - 1; i++)
        {
            distance += CoordinateMath.Haversine(coordinates.ElementAt(i), coordinates.ElementAt(i + 1));
        }

        return distance;
    }

    public static (double gain, double loss) CalculateElevationChange(IEnumerable<Coordinate> coordinates, double distanceThreshold, double verticalThreshold)
    {
        var gain = 0.0;
        var loss = 0.0;

        for (int i = 0; i < coordinates.Count() - 1; i++)
        {
            var current = coordinates.ElementAt(i);
            var next = coordinates.ElementAt(i + 1);

            double distance = CoordinateMath.Haversine(current, next);

            if (distance >= distanceThreshold)
            {
                var elevationDifference = next.Elevation - current.Elevation;

                if (elevationDifference >= verticalThreshold)
                {
                    gain += elevationDifference;
                }
                else if (elevationDifference <= -verticalThreshold)
                {
                    loss += elevationDifference;
                }
            }
        }

        return (gain, Math.Abs(loss));
    }

    public static IEnumerable<Coordinate> Clip(IEnumerable<Coordinate> source, Coordinate start, Coordinate end)
    {
        var coordinates = new List<Coordinate>();
        var found = false;

        foreach (var coordinate in source)
        {
            if (coordinate == start)
            {
                found = true;
            }
            if (found)
            {
                coordinates.Add(coordinate);
            }
            if (coordinate == end)
            {
                break;
            }
        }

        if (!found)
        {
            throw new ArgumentException("Start coordinate not found in the path.");
        }
        if (found && coordinates.Last() != end)
        {
            throw new ArgumentException("End coordinate not found in the path.");
        }

        return coordinates;
    }
}
