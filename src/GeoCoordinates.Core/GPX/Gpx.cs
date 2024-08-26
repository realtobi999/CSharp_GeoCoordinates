using System.Xml.Linq;

namespace GeoCoordinates.Core.GPX;

public struct Gpx
{
    public XNamespace Namespace { get; set; }
    public XDocument Document { get; set; }
}
