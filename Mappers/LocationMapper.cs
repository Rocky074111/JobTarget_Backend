using api.Models;

public static class LocationMapper
{
    public static Location ToLocation(LocationDTO locationDTO)
    {
        return new Location
        {
            City = locationDTO.City ?? "",
            State = locationDTO.State ?? "",
            Country = locationDTO.Country ?? ""
        };
    }

    public static LocationDTO ToLocationDTO(Location location)
    {
        return new LocationDTO
        {
            City = location.City,
            State = location.State,
            Country = location.Country
        };
    }
}