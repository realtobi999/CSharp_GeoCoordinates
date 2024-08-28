namespace GeoCoordinates.Core.Extensions;

public static class IEnumerableCoordinateExtensions
{
    public static double GetDistance(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).Distance;
    }

    public static double GetElevationGain(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).ElevationGain;
    }

    public static double GetElevationLoss(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).ElevationLoss;
    }

    public static IEnumerable<Coordinate> Clip(this IEnumerable<Coordinate> source, Coordinate start, Coordinate end)
    {
        return new CoordinatePath(source).Clip(start, end).Coordinates; 
    }

    public static bool IsAlignedWith(this IEnumerable<Coordinate> source, IEnumerable<Coordinate> path, double deviation)
    {
        return new CoordinatePath(source).IsAlignedWith(new CoordinatePath(path), deviation);
    }

    public static IEnumerable<Coordinate> Simplify(this IEnumerable<Coordinate> source, double epsilon)
    {
        return new CoordinatePath(source).Simplify(epsilon).Coordinates;
    }
}
