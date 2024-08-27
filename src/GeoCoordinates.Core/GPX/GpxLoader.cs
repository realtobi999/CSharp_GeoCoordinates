using System.Xml.Linq;
using GeoCoordinates.Core.Interfaces;

namespace GeoCoordinates.Core.GPX;

/// <summary>
/// The GpxLoader class implements the IGpxLoader interface and provides functionality for loading GPX files and retrieving their associated XML namespace.
/// </summary>
public class GpxLoader : IGpxLoader
{
    public XDocument GetGpxDocument(string filename) => XDocument.Load(filename);
    public XNamespace GetGpxDocumentNamespace(XDocument document) => document.Root!.GetDefaultNamespace();
}
