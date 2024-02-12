using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Contexts;

public partial class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public virtual DbSet<AddressEntity> Addresses { get; set; }
    public virtual DbSet<ContactEntity> Contacts { get; set; }
    public virtual DbSet<CountryEntity> Countries { get; set; }
    public virtual DbSet<OccupationEntity> Occupations { get; set; }
    public virtual DbSet<SalaryEntity> SalaryEntities { get; set; }
}
