using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class OccupationRepository(DataContext context) : BaseRepository<OccupationEntity>(context)
{
    private readonly DataContext _context = context;

    public override IEnumerable<OccupationEntity> GetAll()
    {
        try
        {
            return _context.Occupations.Include(i => i.Salary).ToList();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }

    public override OccupationEntity GetOne(Expression<Func<OccupationEntity, bool>> predicate)
    {
        try
        {
            return _context.Occupations.Include(i => i.Salary).FirstOrDefault(predicate, null!);
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }
}
