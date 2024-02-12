using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class SalaryEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    [Column(TypeName ="money")]
    public decimal Salary { get; set; }

    public virtual ICollection<OccupationEntity> Address { get; set; } = new List<OccupationEntity>();
}
