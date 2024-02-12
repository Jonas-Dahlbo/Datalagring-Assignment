using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

[Index(nameof(Country), IsUnique = true)]
public class CountryEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required, StringLength(100)]
    public string Country { get; set; } = null!;
    [Required, StringLength(20)]
    public string Continent { get; set; } = null!;

    public virtual ICollection<AddressEntity> Address { get; set; } = new List<AddressEntity>();
}
