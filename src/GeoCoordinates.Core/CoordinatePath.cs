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

    public CoordinatePath Clip(Coordinate start, Coordinate end)
    {
        var coordinates = new List<Coordinate>();
        var found = false;

        foreach(var coordinate in Coordinates)
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

        return new(coordinates);
    }

    public CoordinatePath Simplify(double epsilon)
    {
        var coordinates = Algorithms.RamerDouglasPeucker(Coordinates, epsilon);

        return new(coordinates);
    }

    public bool IsAlignedWith(CoordinatePath path, double deviation) => Algorithms.ArePathsOverlapping(Coordinates, path.Coordinates, deviation);

    public Coordinate GetFirstCoordinate() => Coordinates[0];

    public Coordinate GetLastCoordinate() => Coordinates[Coordinates.Count - 1];
}
