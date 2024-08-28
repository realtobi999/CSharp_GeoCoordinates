using FluentAssertions;
using GeoCoordinates.Core.GPX;

namespace GeoCoordinates.Tests;

public class GpxTests
{
    [Fact]
    public void GpxHandler_LoadGpxWaypoints_Works()
    {
        // prepare
        var handler = new GpxHandler(new GpxProcessor(), new GpxLoader());
        var filepath = $"./../../../assets/gpx_test_file_1.gpx";

        // act & assert
        var path = handler.LoadGpxWaypoints(filepath);

        path.Should().NotBeNull();
        path.Coordinates.Count.Should().BeGreaterThanOrEqualTo(1);
        path.Distance.Should().BeGreaterThanOrEqualTo(1);
    }

    [Fact]
    public void GpxHandler_LoadGpxTracks_Works()
    {
        // prepare
        var handler = new GpxHandler(new GpxProcessor(), new GpxLoader());
        var filepath = $"./../../../assets/gpx_test_file_1.gpx";

        // act & assert
        var paths = handler.LoadGpxTracks(filepath);

        paths.Count().Should().BeGreaterThanOrEqualTo(1);
        paths.ElementAt(0).Coordinates.Count.Should().BeGreaterThanOrEqualTo(1);
    }
}
