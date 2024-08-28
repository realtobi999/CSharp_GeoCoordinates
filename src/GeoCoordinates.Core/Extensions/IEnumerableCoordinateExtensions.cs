namespace GeoCoordinates.Core.Extensions;

/// <summary>
/// Provides extension methods for <c>IEnumerable&lt;Coordinate&gt;</c> 
/// that act as wrappers around the <c>CoordinatePath</c> class, 
/// enabling operations like distance calculation, elevation data retrieval, and path manipulation.
/// </summary>
public static class IEnumerableCoordinateExtensions
{
    /// <summary>
    /// Calculates the total distance for the sequence of coordinates.
    /// This is a wrapper around the <c>CoordinatePath.Distance</c> property.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <returns>The total distance of the path.</returns>
    public static double GetDistance(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).Distance;
    }

    /// <summary>
    /// Calculates the total elevation gain for the sequence of coordinates.
    /// This is a wrapper around the <c>CoordinatePath.ElevationGain</c> property.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <returns>The total elevation gain of the path.</returns>
    public static double GetElevationGain(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).ElevationGain;
    }

    /// <summary>
    /// Calculates the total elevation loss for the sequence of coordinates.
    /// This is a wrapper around the <c>CoordinatePath.ElevationLoss</c> property.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <returns>The total elevation loss of the path.</returns>
    public static double GetElevationLoss(this IEnumerable<Coordinate> source)
    {
        return new CoordinatePath(source).ElevationLoss;
    }

    /// <summary>
    /// Clips the sequence of coordinates between the specified start and end coordinates.
    /// This is a wrapper around the <c>CoordinatePath.Clip(Coordinate, Coordinate)</c> method.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <param name="start">The coordinate at which to start clipping.</param>
    /// <param name="end">The coordinate at which to end clipping.</param>
    /// <returns>A new sequence of coordinates representing the clipped portion of the path.</returns>
    public static IEnumerable<Coordinate> Clip(this IEnumerable<Coordinate> source, Coordinate start, Coordinate end)
    {
        return new CoordinatePath(source).Clip(start, end).Coordinates;
    }

    /// <summary>
    /// Determines whether the source sequence of coordinates is aligned with the specified path, 
    /// within a given deviation.
    /// This is a wrapper around the <c>CoordinatePath.IsAlignedWith(CoordinatePath, double)</c> method.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <param name="path">The sequence of coordinates to compare for alignment.</param>
    /// <param name="deviation">The allowed deviation for alignment comparison.</param>
    /// <returns><c>true</c> if the paths are aligned within the given deviation; otherwise, <c>false</c>.</returns>
    public static bool IsAlignedWith(this IEnumerable<Coordinate> source, IEnumerable<Coordinate> path, double deviation)
    {
        return new CoordinatePath(source).IsAlignedWith(new CoordinatePath(path), deviation);
    }

    /// <summary>
    /// Simplifies the sequence of coordinates using the specified epsilon tolerance.
    /// This is a wrapper around the <c>CoordinatePath.Simplify(double)</c> method.
    /// </summary>
    /// <param name="source">The sequence of coordinates representing the path.</param>
    /// <param name="epsilon">The tolerance used for path simplification.</param>
    /// <returns>A simplified sequence of coordinates.</returns>
    public static IEnumerable<Coordinate> Simplify(this IEnumerable<Coordinate> source, double epsilon)
    {
        return new CoordinatePath(source).Simplify(epsilon).Coordinates;
    }
}
