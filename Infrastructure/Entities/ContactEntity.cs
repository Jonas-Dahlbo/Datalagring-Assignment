using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class ContactEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required, StringLength(100)]
    public string FirstName { get; set; } = null!;
    [Required, StringLength(100)]
    public string LastName { get; set; } = null!;
    [Required, StringLength(100)] 
    public string Email { get; set; } = null!;
    
    [Required]
    [ForeignKey(nameof(AddressEntity))]
    public string AddressId { get; set; } = null!;
    public virtual AddressEntity Address { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(OccupationEntity))]
    public string OccupationId { get; set; } = null!;
    public virtual OccupationEntity Occupation { get; set; } = null!;
}
