using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
                .Include(i => i.Address)
                .Include(i => i.Address).ThenInclude(i => i.Country)
                .Include(i => i.Occupation)
                .Include(i => i.Occupation).ThenInclude(i => i.Salary)
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
                .Include(i => i.Address)
                .Include(i => i.Address).ThenInclude(i => i.Country)
                .Include(i => i.Occupation)
                .Include(i => i.Occupation).ThenInclude(i => i.Salary)
                .FirstOrDefault(predicate)!;
        }
        catch (Exception ex) { Debug.WriteLine("ERROR ::" + ex.Message); }
        return null!;
    }
}
