using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities;

public class AddressEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required, StringLength(100)]
    public string City { get; set; } = null!;
    [Required, StringLength(100)]
    public string PostalCode { get; set; } = null!;
    [Required, StringLength(100)]
    public string StreetName { get; set; } = null!;

    [ForeignKey(nameof(CountryEntity))]
    [Required]
    public string CountryId { get; set; } = null!;

    public virtual CountryEntity Country { get; set; } = null!;
}
