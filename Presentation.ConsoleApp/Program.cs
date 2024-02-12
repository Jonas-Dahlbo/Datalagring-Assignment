using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Jonas\source\repos\New folder (2)\DataLagringAssignment\DatalagringAssignment\Infrastructure\Data\database.mdf"";Integrated Security=True;Connect Timeout=30"));
}).Build();