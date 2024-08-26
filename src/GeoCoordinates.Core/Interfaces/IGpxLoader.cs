using System.Xml.Linq;

namespace GeoCoordinates.Core.Interfaces;

public interface IGpxLoader
{
    XDocument GetGpxDocument(string filename);
    XNamespace GetGpxDocumentNamespace(XDocument document);
}
