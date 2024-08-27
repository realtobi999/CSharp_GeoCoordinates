namespace GeoCoordinates.Core.Interfaces;

/// <summary>
/// Interface for handling GPX files, defining the contract for loading and processing GPX data.
/// </summary>
public interface IGpxHandler
{
    /// <summary>
    /// Loads a GPX file from the specified file path, processes it, and returns a CoordinatePath containing the extracted coordinates.
    /// </summary>
    /// <param name="filepath">The file path of the GPX file to load.</param>
    /// <returns>A CoordinatePath containing the extracted coordinates from the GPX file.</returns>
    CoordinatePath LoadGpx(string filepath);
}
