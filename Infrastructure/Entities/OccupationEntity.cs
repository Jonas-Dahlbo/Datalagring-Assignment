using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class OccupationEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required, StringLength(100)]
    public string Occupation { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    [ForeignKey(nameof(SalaryEntity))]
    public string SalaryId { get; set; } = null!;

    public virtual SalaryEntity Salary { get; set; } = null!;
}
