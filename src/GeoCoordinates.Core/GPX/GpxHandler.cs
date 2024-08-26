using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

public class GpxHandler : IGpxHandler
{
    private readonly IGpxProcessor _gpxProcessor;
    private readonly IGpxLoader _gpxLoader;

    public GpxHandler(IGpxProcessor gpxProcessor, IGpxLoader gpxLoader)
    {
        _gpxProcessor = gpxProcessor;
        _gpxLoader = gpxLoader;
    }

    public CoordinatePath LoadGpx(string filepath)
    {
        var gpxDocument = _gpxLoader.GetGpxDocument(filepath);
        var gpxNamespace = _gpxLoader.GetGpxDocumentNamespace(gpxDocument);

        var gpx = new Gpx()
        {
            Document = gpxDocument,
            Namespace = gpxNamespace,
        };

        var coordinates = _gpxProcessor.GetCoordinates(gpx);

        return new CoordinatePath(coordinates);
    }
}
