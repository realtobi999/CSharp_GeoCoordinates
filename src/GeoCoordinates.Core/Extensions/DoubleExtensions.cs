namespace GeoCoordinates.Core.Extensions;

internal static class DoubleExtensions
{
    public static string ToStringDMS(this double value, int decimalPlaces = 2)
    {
        // Get the absolute value of the decimal degree
        double absoluteDegree = Math.Abs(value);

        // Extract the degrees, minutes, and seconds
        int degrees = (int)absoluteDegree;
        double minutesDecimal = (absoluteDegree - degrees) * 60;
        int minutes = (int)minutesDecimal;
        double seconds = (minutesDecimal - minutes) * 60;

        // Return formatted string as D°M'S"
        return $"{degrees}°{minutes}'{seconds:F1}\"";
    }

    public static double ToRadians(this double value)
    {
        return Math.PI * value / 180;
    }
}