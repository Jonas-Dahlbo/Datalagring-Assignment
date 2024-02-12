using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class ContactRepository(DataContext context) : BaseRepository<ContactEntity>(context)
{
    private readonly DataContext _context = context;

    public override IEnumerable<ContactEntity> GetAll()
    {
        try
        {
            return _context.Contacts
                .Include(i => i.Address.Country)
                .Include(i => i.Address.City)
                .Include (i => i.Occupation.Occupation)
                .ToList();
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }

    public override ContactEntity GetOne(Expression<Func<ContactEntity, bool>> predicate)
    {
        try
        {
            return _context.Contacts
                .Include(i => i.Address.Country)
                .Include(i => i.Address.City)
                .Include(i => i.Occupation.Occupation)
                .FirstOrDefault(predicate)!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }
}
