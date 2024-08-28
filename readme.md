# GeoCoordinates C# Library

## Summary

This lightweight and straightforward library is ideal for anyone working with coordinate data or projects involving geographical information.

## Library Features

- Coordinate: A class representing a single point in space, containing latitude, longitude, elevation, and other relevant properties.
- CoordinatePath: A class that holds a collection of coordinates along with additional attributes such as total distance, elevation gain and loss, and more.
- GpxHandler: A utility class for parsing and extracting data from GPX files (versions 1.0 and 1.1).

## Example Use Case

Consider a scenario where you need to calculate the distance between two mountain summits.

Using this library you would do as such:

``` C#
static double GetDistanceBetweenSummits(Coordinate summit1, Coordinate summit2)
{
    return summit1.GetDistanceTo(summit2);
}

var summit1 = new Coordinate(53.023, 54.024, 6565);
var summit2 = new Coordinate(54.325, 55.023, 7972);

var distance = GetDistanceBetweenSummits(summit1, summit2);

Console.WriteLine($"The distance between the summits is {distance} meters."); 
```

Or you need to figure out the distance of your run which u have saved as a gpx file.

``` C#
var handler = new GpxHandler(new GpxProcessor() ,new GpxLoader());
var filepath = "./runs/my_fav_run.gpx";

var run = handler.LoadGpxWaypoints(filepath);

Console.WriteLine($"The distance of the run is {path.Distance} meters");
```

Or you need to figure out average elevation gain of your saved tracks from gpx file.

``` C#
var handler = new GpxHandler(new GpxProcessor(), new GpxLoader());
var filepath = "./runs/my_fav_run.gpx";

var runs = handler.LoadGpxTracks(filepath);

var total = 0.0;

foreach (var run in runs)
{
    total += run.ElevationGain;
}

Console.WriteLine($"The average elevation gain of all your runs is {total / runs.Count()})
```

## Contribution

If anyone reading this wants to contribute just create a pull request. I will be happy to extend this library beyond, I've implemented only the basics for my own use. I'm sure there is a'lot of that can be done!
