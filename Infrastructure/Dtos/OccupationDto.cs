namespace Infrastructure.Dtos;

public class OccupationDto
{
    public string Occupation { get; set; } = null!;

    public string? Description { get; set; } = null!;
    public decimal Salary { get; set; }
}
