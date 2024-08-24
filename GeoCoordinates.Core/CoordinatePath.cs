using GeoCoordinates.Core.Helpers;

namespace GeoCoordinates.Core;

public class CoordinatePath
{
    public IReadOnlyList<Coordinate> Coordinates { get; init; }
    public double Distance { get; private set; }
    public double ElevationGain { get; private set; }
    public double ElevationLoss { get; private set; }

    public CoordinatePath(IEnumerable<Coordinate> coordinates)
    {
        Coordinates = coordinates.ToList().AsReadOnly();

        CalculatePathMetrics();
    }

    private void CalculatePathMetrics()
    {
        Distance = CoordinatePathHelpers.CalculateDistance(Coordinates);
        (ElevationGain, ElevationLoss) = CoordinatePathHelpers.CalculateElevationChange(Coordinates, 0, 0);
    }

    public bool IsAlignedWith(CoordinatePath path, double deviation)
    {
        var routeCoordinates = this.Coordinates;
        var pathCoordinates = path.Coordinates;

        for (int i = 0; i < routeCoordinates.Count; i++)
        {
            bool found = false;
            for (int j = 0; j < pathCoordinates.Count; j++)
            {
                if (routeCoordinates[i].IsWithinDistanceTo(pathCoordinates[j], deviation))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return true;
    }

    public Coordinate GetFirstCoordinate() => Coordinates[0];
    public Coordinate GetLastCoordinate() => Coordinates[Coordinates.Count - 1];
}
