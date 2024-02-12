namespace Infrastructure.Dtos;

public class ContactDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;

    public string City { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string StreetName { get; set; } = null!;

    public string Country { get; set; } = null!;
    public string Continent { get; set; } = null!;
    public string Occupation { get; set; } = null!;

    public string? Description { get; set; } = null!;
    public decimal Salary { get; set; }
}
