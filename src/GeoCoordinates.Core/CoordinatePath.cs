using GeoCoordinates.Core.Helpers;

namespace GeoCoordinates.Core;

public class CoordinatePath
{
    public IReadOnlyList<Coordinate> Coordinates { get; init; }
    public double Distance { get; init; }
    public double ElevationGain { get; init; }
    public double ElevationLoss { get; init; }

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

    public CoordinatePath(IEnumerable<Coordinate> coordinates, double distance, double elevationGain, double elevationLoss)
    {
        if (coordinates.Count() < 2)
        {
            throw new ArgumentException("The path must contain at least two coordinates.");
        }

        Coordinates = coordinates.ToList().AsReadOnly();
        Distance = distance;
        ElevationGain = elevationGain;
        ElevationLoss = elevationLoss;
    }

    public CoordinatePath Clip(Coordinate start, Coordinate end)
    {
        var coordinates = CoordinatePathHelpers.Clip(Coordinates, start, end); 

        return new(coordinates);
    }

    public CoordinatePath Simplify(double epsilon)
    {
        var coordinates = Algorithms.RamerDouglasPeucker(Coordinates, epsilon);

        return new(coordinates);
    }

    public bool IsAlignedWith(CoordinatePath path, double deviation)
    {
        return Algorithms.ArePathsOverlapping(Coordinates, path.Coordinates, deviation);
    } 

    public Coordinate GetFirstCoordinate() => Coordinates[0];

    public Coordinate GetLastCoordinate() => Coordinates[Coordinates.Count - 1];
}
