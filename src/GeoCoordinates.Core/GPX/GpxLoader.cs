using System.Xml.Linq;
using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

public class GpxLoader : IGpxLoader
{
    public XDocument GetGpxDocument(string filename) => XDocument.Load(filename);
    public XNamespace GetGpxDocumentNamespace(XDocument document) => document.Root!.GetDefaultNamespace();
}
