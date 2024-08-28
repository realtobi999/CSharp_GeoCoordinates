using FluentAssertions;
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
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)  // paris
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        path.Coordinates.Should().HaveCount(2);
        path.GetFirstCoordinate().Should().Be(coordinates[0]);
        path.GetLastCoordinate().Should().Be(coordinates[1]);
    }

    [Fact]
    public void Constructor_ShouldCalculateDistanceCorrectly()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)  // paris
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        path.Distance.Should().BeGreaterThan(0);  // assuming the helper calculates a valid distance
    }

    [Fact]
    public void Constructor_ShouldCalculateElevationGainAndLossCorrectly()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 100),  // berlin
            new(48.8566, 2.3522, 200),   // paris
            new(40.7128, -74.0060, 50)   // new york
        };

        // act & assert
        var path = new CoordinatePath(coordinates);
        path.ElevationGain.Should().Be(100);  // 200 - 100
        path.ElevationLoss.Should().Be(150);  // 200 - 50
    }

    [Fact]
    public void GetFirstCoordinate_ShouldReturnCorrectCoordinate()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)  // paris
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var firstCoordinate = path.GetFirstCoordinate();
        firstCoordinate.Should().Be(coordinates[0]);
    }

    [Fact]
    public void GetLastCoordinate_ShouldReturnCorrectCoordinate()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)  // paris
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var lastCoordinate = path.GetLastCoordinate();
        lastCoordinate.Should().Be(coordinates[1]);
    }

    [Fact]
    public void IsAlignedWith_ShouldReturnTrueForAlignedPaths()
    {
        // prepare
        var coordinates1 = new List<Coordinate>
        {
            new(48.8566, 2.3522, 0),  // paris
            new(52.5200, 13.4050, 0), // berlin
        };

        var coordinates2 = new List<Coordinate>
        {
            new(52.5201, 13.4051, 0), // close to berlin
            new(48.8565, 2.3523, 0)   // close to paris
        };

        var path1 = new CoordinatePath(coordinates1);
        var path2 = new CoordinatePath(coordinates2);

        // act & assert
        var result = path1.IsAlignedWith(path2, 1000);  // 1 km deviation
        result.Should().BeTrue();
    }

    [Fact]
    public void IsAlignedWith_ShouldReturnFalseForNonAlignedPaths()
    {
        // prepare
        var coordinates1 = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)   // paris
        };

        var coordinates2 = new List<Coordinate>
        {
            new(37.7749, -122.4194, 0),  // san francisco
            new(34.0522, -118.2437, 0)   // los angeles
        };

        var path1 = new CoordinatePath(coordinates1);
        var path2 = new CoordinatePath(coordinates2);

        // act & assert
        var result = path1.IsAlignedWith(path2, 1000);  // 1 km deviation
        result.Should().BeFalse();
    }

    [Fact]
    public void Simplify_ShouldReturnSameCoordinates_WhenEpsilonIsZero()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(51.1657, 10.4515, 0), // somewhere in germany
            new(48.8566, 2.3522, 0)   // paris
        };

        var path = new CoordinatePath(coordinates);

        // act
        var simplifiedPath = path.Simplify(0);

        // assert
        simplifiedPath.Coordinates.Should().Equal(coordinates);
    }

    [Fact]
    public void Simplify_ShouldRemoveUnnecessaryPoints_WhenEpsilonIsGreaterThanZero()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(0, 0, 0),     // start
            new(0.5, 0.5, 0), // near start (should be removed)
            new(1, 1, 0),     // end
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var simplifiedPath = path.Simplify(0.1);

        var expectedCoordinates = new List<Coordinate>
        {
            new(0, 0, 0),  // start
            new(1, 1, 0)   // end
        };

        simplifiedPath.Coordinates.Should().Equal(expectedCoordinates);
    }

    [Fact]
    public void Clip_ShouldReturnCorrectPath_WhenStartAndEndExistInPath()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(51.1657, 10.4515, 0), // somewhere in germany
            new(48.8566, 2.3522, 0)   // paris
        };

        var path = new CoordinatePath(coordinates);

        // act
        var clippedPath = path.Clip(coordinates[0], coordinates[2]);

        // assert
        clippedPath.Coordinates.Should().Equal(coordinates);
    }

    [Fact]
    public void Clip_ShouldThrowException_WhenStartOrEndNotInPath()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)   // paris
        };

        var path = new CoordinatePath(coordinates);
        var start = new Coordinate(0, 0, 0); // non-existent start coordinate
        var end = new Coordinate(1, 1, 0);   // non-existent end coordinate

        // act & assert
        var action1 = () => path.Clip(start, coordinates[1]);
        action1.Should().Throw<ArgumentException>().WithMessage("Start coordinate not found in the path.");
        var action2 = () => path.Clip(coordinates[0], end);
        action2.Should().Throw<ArgumentException>().WithMessage("End coordinate not found in the path.");
    }

    [Fact]
    public void TestName()
    {
        // prepare
        var coordinates = new List<Coordinate>
        {
            new(52.5200, 13.4050, 0), // berlin
            new(48.8566, 2.3522, 0)   // paris
        };

        var path = new CoordinatePath(coordinates);

        // act & assert
        var result = path.ToString();
        result.Should().Be("52.52|13.405|0;48.8566|2.3522|0");
    }
}
