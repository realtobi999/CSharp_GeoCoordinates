namespace GeoCoordinates.Core.Interfaces;

/// <summary>
/// Interface for handling GPX files, defining the contract for loading and processing GPX data.
/// </summary>
public interface IGpxHandler
{
    /// <summary>
    /// Loads a GPX file from the specified file path, processes it's waypoints, and returns a CoordinatePath containing the extracted waypoint coordinates.
    /// </summary>
    /// <param name="filepath">The file path of the GPX file to load.</param>
    /// <returns>A CoordinatePath containing the extracted coordinates from the GPX file.</returns>
    CoordinatePath LoadGpxWaypoints(string filepath);

    /// <summary>
    /// Loads a GPX file from the specified file path, processes it's tracks, and returns a <c>IEnumerable&lt;Coordinate&gt;</c> containing the extracted tracks.
    /// </summary>
    /// <param name="filepath">The file path of the GPX file to load.</param>
    /// <returns>A <c>IEnumerable&lt;Coordinate&gt;</c> containing the extracted tracks from the GPX file.</returns>
    IEnumerable<CoordinatePath> LoadGpxTracks(string filepath);
}
