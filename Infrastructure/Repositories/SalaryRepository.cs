using Infrastructure.Contexts;
using Infrastructure.Entities;

namespace Infrastructure.Repositories
{
    public class SalaryRepository(DataContext context) : BaseRepository<SalaryEntity>(context)
    {
        private readonly DataContext _context = context;
    }
}
