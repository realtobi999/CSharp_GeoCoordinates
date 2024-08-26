using FluentAssertions;
using GeoCoordinates.Core.GPX;

namespace GeoCoordinates.Tests;

public class GpxTests
{
    [Fact]
    public void GpxHandler_LoadGpx_Works()
    {
        // prepare
        var handler = new GpxHandler(new GpxProcessor() ,new GpxLoader());
        var filepath = $"./../../../assets/{nameof(GpxTests)}_{nameof(GpxHandler_LoadGpx_Works)}_TestAsset.gpx";

        // act & assert
        var path = handler.LoadGpx(filepath);

        path.Should().NotBeNull();
        path.Coordinates.Count.Should().BeGreaterThan(1);
        path.Distance.Should().BeGreaterThan(1);
        path.ElevationGain.Should().BeGreaterThan(1);
        path.ElevationLoss.Should().BeGreaterThan(1);

        foreach (var coordinate in path.Coordinates)
        {
            Console.WriteLine(coordinate.ToString());
        }
    }
}
