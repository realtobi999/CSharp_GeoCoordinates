using System.Drawing;

namespace GeoCoordinates.Core.Helpers;

internal class Algorithms
{
    public static bool ArePathsOverlapping(IEnumerable<Coordinate> coordinate1, IEnumerable<Coordinate> coordinates2, double deviation)
    {
        for (int i = 0; i < coordinate1.Count(); i++)
        {
            bool found = false;
            for (int j = 0; j < coordinates2.Count(); j++)
            {
                if (coordinate1.ElementAt(i).IsWithinDistanceTo(coordinates2.ElementAt(j), deviation))
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return true;
    }
    
    public static List<Coordinate> RamerDouglasPeucker(IEnumerable<Coordinate> coordinates, double epsilon)
    {
        if (coordinates.Count() < 2)
        {
            return [.. coordinates];
        }
        if (epsilon <= 0)
        {
            return [.. coordinates];
        }

        // find the coordinate with the maximum distance from the line between the start and end
        var maxDistance = 0;
        var index = 0;
        for (var i = 1; i < coordinates.Count() - 1; i++)
        {
            var distance = CoordinateMath.PerpendicularDistance(coordinates.ElementAt(i), coordinates.First(), coordinates.Last());
            if (distance > maxDistance)
            {
                index = i;
                maxDistance = (int)distance;
            }
        }

        // if max distance is greater than epsilon, recursively simplify
        if (maxDistance > epsilon)
        {
            // recursively simplify the two parts
            var firstHalf = RamerDouglasPeucker(coordinates.Take(index + 1), epsilon);
            var secondHalf = RamerDouglasPeucker(coordinates.Skip(index), epsilon);

            // merge results, avoiding duplicate at split coordinate
            return firstHalf.Take(firstHalf.Count - 1).Concat(secondHalf).ToList();
        }
        else
        {
            // if no coordinate is beyond the threshold, return start and end coordinate 
            return [coordinates.First(), coordinates.Last()];
        }
    }
}
