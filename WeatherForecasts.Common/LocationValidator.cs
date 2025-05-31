namespace WeatherForecasts.Common;

public static class LocationValidator
{
    public static bool IsValidLatitude(float latitude) =>
        latitude is >= -90 and <= 90;

    public static bool IsValidLongitude(float longitude) =>
        longitude is >= -180 and <= 180;

    public static bool IsValidCoordinates(float latitude, float longitude) =>
        IsValidLatitude(latitude) && IsValidLongitude(longitude);

    public static void ValidateCoordinates(float latitude, float longitude)
    {
        if (!IsValidLatitude(latitude))
            throw new ArgumentOutOfRangeException(nameof(latitude), "Latitude must be between -90 and 90 degrees.");
        if (!IsValidLongitude(longitude))
            throw new ArgumentOutOfRangeException(nameof(longitude), "Longitude must be between -180 and 180 degrees.");
    }
}
