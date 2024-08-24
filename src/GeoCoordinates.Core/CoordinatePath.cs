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
        if (coordinates.Count() < 2)
        {
            throw new ArgumentException("The path must contain at least two coordinates.");
        }

        Coordinates = coordinates.ToList().AsReadOnly();
        Distance = CoordinatePathHelpers.CalculateDistance(Coordinates);
        (ElevationGain, ElevationLoss) = CoordinatePathHelpers.CalculateElevationChange(Coordinates, 5, 5);
    }

    public bool IsAlignedWith(CoordinatePath path, double deviation) => Algorithms.ArePathsOverlapping(Coordinates, path.Coordinates, deviation);

    public IEnumerable<Coordinate> Simplify(double epsilon) => Algorithms.RamerDouglasPeucker(Coordinates, epsilon);

    public Coordinate GetFirstCoordinate() => Coordinates[0];

    public Coordinate GetLastCoordinate() => Coordinates[Coordinates.Count - 1];
}
