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
}
