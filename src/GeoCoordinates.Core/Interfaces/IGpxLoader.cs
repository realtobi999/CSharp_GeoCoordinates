using System;
using System.Xml.Linq;

namespace GeoCoordinates.Core.Interfaces;

public interface IGpxLoader
{
    XDocument GetGpxDocument(string filename);
    XNamespace GetGpxNamespace(string namespaceName);
}
