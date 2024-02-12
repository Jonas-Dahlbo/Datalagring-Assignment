using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories;

public class CountryRepository(DataContext context) : BaseRepository<CountryEntity>(context)
{
    private readonly DataContext _context = context;
}
