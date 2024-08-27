using System.Xml.Linq;

namespace GeoCoordinates.Core.Interfaces;

/// <summary>
/// The IGpxLoader interface provides methods for loading GPX files and retrieving their associated XML namespace.
/// </summary>
public interface IGpxLoader
{
    /// <summary>
    /// Loads a GPX document from the specified file path and returns it as an XDocument.
    /// </summary>
    /// <param name="filename">The file path of the GPX file to load.</param>
    /// <returns>An XDocument representing the loaded GPX document.</returns>
    XDocument GetGpxDocument(string filename);

    /// <summary>
    /// Retrieves the XML namespace from the root of a GPX document.
    /// </summary>
    /// <param name="document">The XDocument representing the GPX document.</param>
    /// <returns>The XNamespace associated with the GPX document.</returns>
    XNamespace GetGpxDocumentNamespace(XDocument document);
}
