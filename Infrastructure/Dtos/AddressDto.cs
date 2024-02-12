namespace Infrastructure.Dtos;

public class AddressDto
{
    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string StreetName { get; set; } = null!;

    public string Country { get; set; } = null!;
    public string Continent { get; set; } = null!;
}
