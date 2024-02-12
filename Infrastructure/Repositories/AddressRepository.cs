using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class AddressRepository(DataContext context) : BaseRepository<AddressEntity>(context)
{
    private readonly DataContext _context = context;

    public override IEnumerable<AddressEntity> GetAll()
    {
        try
        {
            return _context.Addresses.Include(i => i.Country).ToList();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }

    public override AddressEntity GetOne(Expression<Func<AddressEntity, bool>> predicate)
    {
        try
        {
            return _context.Addresses.Include(i => i.Country).FirstOrDefault(predicate)!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }
}
