using GeoCoordinates.Core.Helpers;

namespace GeoCoordinates.Core;

/// <summary>
/// Represents a path composed of a sequence of <c>Coordinate</c> objects. 
/// Provides functionality to calculate distance, elevation gain/loss, clipping, and path simplification.
/// </summary>
public class CoordinatePath
{
    public IReadOnlyList<Coordinate> Coordinates { get; init; }
    public double Distance { get; init; }
    public double ElevationGain { get; init; }
    public double ElevationLoss { get; init; }

    /// <summary>
    /// Initializes a new instance of the <c>CoordinatePath</c> class, calculating distance, elevation gain, and elevation loss for the path.
    /// </summary>
    /// <param name="coordinates">The sequence of coordinates representing the path. Must contain at least two coordinates.</param>
    /// <exception cref="ArgumentException">Thrown when fewer than two coordinates are provided.</exception>
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

    /// <summary>
    /// Initializes a new instance of the <c>CoordinatePath</c> class with pre-calculated distance, elevation gain, and elevation loss.
    /// </summary>
    /// <param name="coordinates">The sequence of coordinates representing the path. Must contain at least two coordinates.</param>
    /// <param name="distance">The pre-calculated distance of the path in meters.</param>
    /// <param name="elevationGain">The pre-calculated elevation gain of the path in meters.</param>
    /// <param name="elevationLoss">The pre-calculated elevation loss of the path in meters.</param>
    /// <exception cref="ArgumentException">Thrown when fewer than two coordinates are provided.</exception>
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

    /// <summary>
    /// Clips the path between the specified start and end coordinates, creating a new path that consists of only the clipped section.
    /// </summary>
    /// <param name="start">The starting coordinate of the clip.</param>
    /// <param name="end">The ending coordinate of the clip.</param>
    /// <returns>A new <c>CoordinatePath</c> that represents the clipped portion of the path.</returns>
    public CoordinatePath Clip(Coordinate start, Coordinate end)
    {
        var coordinates = CoordinatePathHelpers.Clip(Coordinates, start, end);
        return new(coordinates);
    }

    /// <summary>
    /// Simplifies the path using the Ramer-Douglas-Peucker algorithm.
    /// </summary>
    /// <param name="epsilon">The epsilon tolerance for path simplification.</param>
    /// <returns>A new <c>CoordinatePath</c> that represents the simplified path.</returns>
    public CoordinatePath Simplify(double epsilon)
    {
        var coordinates = Algorithms.RamerDouglasPeucker(Coordinates, epsilon);
        return new(coordinates);
    }

    /// <summary>
    /// Determines if the current path is aligned with another path within the given deviation.
    /// </summary>
    /// <param name="path">The other <c>CoordinatePath</c> to compare for alignment.</param>
    /// <param name="deviation">The allowed deviation for considering the paths aligned, in meters.</param>
    /// <returns><c>true</c> if the paths are aligned within the given deviation; otherwise, <c>false</c>.</returns>
    public bool IsAlignedWith(CoordinatePath path, double deviation)
    {
        return Algorithms.ArePathsOverlapping(Coordinates, path.Coordinates, deviation);
    }

    /// <summary>
    /// Gets the first coordinate of the path.
    /// </summary>
    /// <returns>The first <c>Coordinate</c> of the path.</returns>
    public Coordinate GetFirstCoordinate()
    {
        return Coordinates[0];
    }

    /// <summary>
    /// Gets the last coordinate of the path.
    /// </summary>
    /// <returns>The last <c>Coordinate</c> of the path.</returns>
    public Coordinate GetLastCoordinate()
    {
        return Coordinates[Coordinates.Count - 1];
    }
}
