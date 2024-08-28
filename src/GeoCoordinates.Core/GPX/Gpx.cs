using System.Xml.Linq;

namespace GeoCoordinates.Core.GPX;

/// <summary>
/// Represents a GPX (GPS Exchange Format) structure containing the XML namespace and the GPX document itself.
/// </summary>
public struct Gpx
{
    /// <summary>
    /// XML namespace of the GPX document.
    /// </summary>
    public XNamespace Namespace { get; set; }

    /// <summary>
    /// XDocument representing the GPX file.
    /// </summary>
    public XDocument Document { get; set; }
}

