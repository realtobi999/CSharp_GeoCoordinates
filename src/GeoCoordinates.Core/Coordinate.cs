using GeoCoordinates.Core.Extensions;
using GeoCoordinates.Core.Helpers;

namespace GeoCoordinates.Core;

public class Coordinate
{
    public const string StringCoordinateFormat = "latitude|longitude|elevation";
    public const double MaximumElevationPoint = 8848;
    public const double LowestElevationPoint = -420;

    public double Latitude { get; }
    public double Longitude { get; }
    public double Elevation { get; } // in meters

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

    public bool IsWithinDistanceTo(Coordinate coordinate, double range)
    {
        if (range <= 0)
        {
            throw new ArgumentException($"{nameof(IsWithinDistanceTo)} '{nameof(range)}' argument needs to be bigger than zero.");
        }

        var distance = CoordinateMath.Haversine(this, coordinate);

        return distance <= range;
    }

    public override string ToString()
    {
        return $"{Latitude}|{Longitude}|{Elevation}";
    }

    public string ToPrettyString()
    {
        var latitudePrefix = Latitude > 0 ? "N" : "S";
        var longitudePrefix = Longitude > 0 ? "E" : "W";

        return $"{Math.Abs(Latitude).ToStringDMS()}{latitudePrefix} {Math.Abs(Longitude).ToStringDMS()}{longitudePrefix} Elevation: {Elevation} meters";
    }

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

    public double GetDistanceTo(Coordinate coordinate) => CoordinateMath.Haversine(this, coordinate);

    public override int GetHashCode() => HashCode.Combine(Latitude, Longitude, Elevation);

    public static bool operator ==(Coordinate c1, Coordinate c2) => c1.Equals(c2);

    public static bool operator !=(Coordinate c1, Coordinate c2) => !c1.Equals(c2);

}
