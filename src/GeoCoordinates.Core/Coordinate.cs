using GeoCoordinates.Core.Extensions;
using GeoCoordinates.Core.Helpers;

namespace GeoCoordinates.Core;

/// <summary>
/// Represents a geographical coordinate with latitude, longitude, and elevation values.
/// </summary>
public class Coordinate
{
    /// <summary>
    /// The format used for string representation of a coordinate (latitude|longitude|elevation).
    /// </summary>
    public const string StringCoordinateFormat = "latitude|longitude|elevation";

    /// <summary>
    /// The maximum possible elevation point in meters (e.g., Mount Everest at 8848 meters).
    /// </summary>
    public const double MaximumElevationPoint = 8848;

    /// <summary>
    /// The lowest possible elevation point in meters (e.g., the Dead Sea at -420 meters).
    /// </summary>
    public const double LowestElevationPoint = -420;

    /// <summary>
    /// Latitude of the coordinate, ranging from -90 to 90 degrees.
    /// </summary>
    public double Latitude { get; }

    /// <summary>
    /// Longitude of the coordinate, ranging from -180 to 180 degrees.
    /// </summary>
    public double Longitude { get; }

    /// <summary>
    /// Elevation of the coordinate in meters.
    /// </summary>
    public double Elevation { get; }

    /// <summary>
    /// Initializes a new instance of the <c>Coordinate</c> class with the specified latitude, longitude, and elevation.
    /// </summary>
    /// <param name="latitude">The latitude of the coordinate.</param>
    /// <param name="longitude">The longitude of the coordinate.</param>
    /// <param name="elevation">The elevation of the coordinate in meters.</param>
    /// <exception cref="FormatException">Thrown when latitude, longitude, or elevation values are out of range.</exception>
    public Coordinate(double latitude, double longitude, double elevation)
    {
        if (latitude < -90 || latitude > 90)
        {
            throw new FormatException("Latitude must be between -90 and 90 degrees.");
        }

        if (longitude < -180 || longitude > 180)
        {
            throw new FormatException("Longitude must be between -180 and 180 degrees.");
        }

        if (LowestElevationPoint >= elevation || elevation >= MaximumElevationPoint)
        {
            throw new FormatException($"Elevation must be between {LowestElevationPoint} meters and {MaximumElevationPoint} meters.");
        }

        Latitude = latitude;
        Longitude = longitude;
        Elevation = elevation;
    }

    /// <summary>
    /// Parses a coordinate string in the format "latitude|longitude|elevation" and returns a new <c>Coordinate</c> object.
    /// </summary>
    /// <param name="coordinate">The coordinate string to parse.</param>
    /// <returns>A new <c>Coordinate</c> object based on the parsed string.</returns>
    /// <exception cref="FormatException">Thrown if the string format or values are invalid.</exception>
    public static Coordinate Parse(string coordinate)
    {
        var coordinates = coordinate.Split('|');
        if (coordinates.Length != 3)
        {
            throw new FormatException($"Invalid coordinate format. Expected format: '{StringCoordinateFormat}'");
        }

        if (!double.TryParse(coordinates[0], out double latitude))
        {
            throw new FormatException("Invalid latitude value.");
        }
        if (!double.TryParse(coordinates[1], out double longitude))
        {
            throw new FormatException("Invalid longitude value.");
        }
        if (!double.TryParse(coordinates[2], out double elevation))
        {
            throw new FormatException("Invalid elevation value.");
        }

        return new Coordinate(latitude, longitude, elevation);
    }

    /// <summary>
    /// Determines if the current coordinate is within a specified distance of another coordinate.
    /// </summary>
    /// <param name="coordinate">The coordinate to compare to.</param>
    /// <param name="range">The maximum distance in meters.</param>
    /// <returns><c>true</c> if the distance to the other coordinate is within the specified range; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentException">Thrown if the range is less than or equal to zero.</exception>
    public bool IsWithinDistanceTo(Coordinate coordinate, double range)
    {
        if (range <= 0)
        {
            throw new ArgumentException($"{nameof(IsWithinDistanceTo)} '{nameof(range)}' argument needs to be bigger than zero.");
        }

        var distance = CoordinateMath.Haversine(this, coordinate);

        return distance <= range;
    }

    /// <summary>
    /// Calculates the distance between this coordinate and another coordinate using the Haversine formula.
    /// </summary>
    /// <param name="coordinate">The other coordinate to measure the distance to.</param>
    /// <returns>The distance in meters between the two coordinates.</returns>
    public double GetDistanceTo(Coordinate coordinate)
    {
        return CoordinateMath.Haversine(this, coordinate);
    }

    /// <summary>
    /// Returns a human-readable string of the coordinate in a degrees, minutes, and seconds (DMS) format with N/S and E/W prefixes.
    /// </summary>
    /// <returns>A formatted string representing the coordinate and elevation.</returns>
    public string ToPrettyString()
    {
        var latitudePrefix = Latitude > 0 ? "N" : "S";
        var longitudePrefix = Longitude > 0 ? "E" : "W";

        return $"{Math.Abs(Latitude).ToStringDMS()}{latitudePrefix} {Math.Abs(Longitude).ToStringDMS()}{longitudePrefix} Elevation: {Elevation} meters";
    }

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Coordinate coordinate)
        {
            return false;
        }

        if (ReferenceEquals(coordinate, this))
        {
            return true;
        }

        return coordinate.Latitude == Latitude &&
               coordinate.Longitude == Longitude &&
               coordinate.Elevation == Elevation;
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Latitude}|{Longitude}|{Elevation}";
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        return HashCode.Combine(Latitude, Longitude, Elevation);
    }

    /// <inheritdoc/>
    public static bool operator ==(Coordinate c1, Coordinate c2) => c1.Equals(c2);

    /// <inheritdoc/>
    public static bool operator !=(Coordinate c1, Coordinate c2) => !c1.Equals(c2);
}