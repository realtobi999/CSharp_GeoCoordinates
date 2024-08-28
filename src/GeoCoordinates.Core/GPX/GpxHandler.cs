using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

/// <summary>
/// The GpxHandler class is responsible for loading and processing GPX files to extract coordinate data.
/// Implements the IGpxHandler interface.
/// </summary>
public class GpxHandler : IGpxHandler
{
    private readonly IGpxProcessor _gpxProcessor;
    private readonly IGpxLoader _gpxLoader;

    /// <summary>
    /// Constructor for GpxHandler. Initializes the handler with required dependencies.
    /// </summary>
    /// <param name="gpxProcessor">An instance of IGpxProcessor to process GPX data.</param>
    /// <param name="gpxLoader">An instance of IGpxLoader to load GPX files.</param>
    public GpxHandler(IGpxProcessor gpxProcessor, IGpxLoader gpxLoader)
    {
        _gpxProcessor = gpxProcessor;
        _gpxLoader = gpxLoader;
    }

    /// <inheritdoc/>
    public IEnumerable<CoordinatePath> LoadGpxTracks(string filepath)
    {
        var gpx = LoadGpx(filepath);

        var paths = _gpxProcessor.ParseTracks(gpx); 

        return paths;
    }

    /// <inheritdoc/>
    public CoordinatePath LoadGpxWaypoints(string filepath)
    {
        var gpx = LoadGpx(filepath);

        var coordinates = _gpxProcessor.ParseWaypoints(gpx);

        return new CoordinatePath(coordinates);
    }

    private Gpx LoadGpx(string filepath)
    {
        var gpxDocument = _gpxLoader.GetGpxDocument(filepath);
        var gpxNamespace = _gpxLoader.GetGpxDocumentNamespace(gpxDocument);

        var gpx = new Gpx()
        {
            Document = gpxDocument,
            Namespace = gpxNamespace,
        };

        return gpx;
    }
}
