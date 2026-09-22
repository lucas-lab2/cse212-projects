using System.Text.Json.Serialization;

/// <summary>
/// Represents the top-level GeoJSON FeatureCollection returned by the USGS earthquake API.
/// See: https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
/// </summary>
public class FeatureCollection
{
    /// <summary>
    /// Array of earthquake feature objects.
    /// </summary>
    public Feature[] Features { get; set; }
}

/// <summary>
/// Represents a single earthquake feature in the GeoJSON feed.
/// </summary>
public class Feature
{
    /// <summary>
    /// Properties associated with the earthquake event.
    /// </summary>
    public EarthquakeProperties Properties { get; set; }
}

/// <summary>
/// Contains the relevant earthquake properties: location and magnitude.
/// </summary>
public class EarthquakeProperties
{
    /// <summary>
    /// Earthquake magnitude (can be null if not yet determined).
    /// </summary>
    public double? Mag { get; set; }

    /// <summary>
    /// Human-readable description of the earthquake location.
    /// </summary>
    public string Place { get; set; }
}