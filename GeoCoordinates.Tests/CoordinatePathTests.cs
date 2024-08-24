using GeoCoordinates.Core;

namespace GeoCoordinates.Tests;

public class CoordinatePathTests
{
    [Fact]
    public void Constructor_ShouldInitializeCoordinatesProperly()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)  // Paris
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        Assert.Equal(2, path.Coordinates.Count);
        Assert.Equal(coordinates[0], path.GetFirstCoordinate());
        Assert.Equal(coordinates[1], path.GetLastCoordinate());
    }

    [Fact]
    public void Constructor_ShouldCalculateDistanceCorrectly()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)  // Paris
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        Assert.True(path.Distance > 0);  // Assuming the helper calculates a valid distance.
    }

    [Fact]
    public void Constructor_ShouldCalculateElevationGainAndLossCorrectly()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 100),  // Berlin
            new Coordinate(48.8566, 2.3522, 200),   // Paris
            new Coordinate(40.7128, -74.0060, 50)   // New York
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        Assert.Equal(100, path.ElevationGain);  // 200 - 100
        Assert.Equal(150, path.ElevationLoss);  // 200 - 50
    }

    [Fact]
    public void GetFirstCoordinate_ShouldReturnCorrectCoordinate()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)  // Paris
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var firstCoordinate = path.GetFirstCoordinate();
        Assert.Equal(coordinates[0], firstCoordinate);
    }

    [Fact]
    public void GetLastCoordinate_ShouldReturnCorrectCoordinate()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)  // Paris
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var lastCoordinate = path.GetLastCoordinate();
        Assert.Equal(coordinates[1], lastCoordinate);
    }

    [Fact]
    public void IsAlignedWith_ShouldReturnTrueForAlignedPaths()
    {
        // prepare
        var coordinates1 = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)   // Paris
        };

        var coordinates2 = new List<Coordinate>
        {
            new Coordinate(52.5201, 13.4051, 0), // Close to Berlin
            new Coordinate(48.8565, 2.3523, 0)   // Close to Paris
        };

        var path1 = new CoordinatePath(coordinates1);
        var path2 = new CoordinatePath(coordinates2);

        // act & assert
        var result = path1.IsAlignedWith(path2, 1000);  // 1 km deviation
        Assert.True(result);
    }

    [Fact]
    public void IsAlignedWith_ShouldReturnFalseForNonAlignedPaths()
    {
        // prepare
        var coordinates1 = new List<Coordinate>
        {
            new Coordinate(52.5200, 13.4050, 0), // Berlin
            new Coordinate(48.8566, 2.3522, 0)   // Paris
        };

        var coordinates2 = new List<Coordinate>
        {
            new Coordinate(37.7749, -122.4194, 0),  // San Francisco
            new Coordinate(34.0522, -118.2437, 0)   // Los Angeles
        };

        var path1 = new CoordinatePath(coordinates1);
        var path2 = new CoordinatePath(coordinates2);

        // act & assert
        var result = path1.IsAlignedWith(path2, 1000);  // 1 km deviation
        Assert.False(result);
    }
}

