using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<DataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""C:\Users\Jonas\source\repos\New folder (2)\DataLagringAssignment\DatalagringAssignment\Infrastructure\Data\database.mdf"";Integrated Security=True;Connect Timeout=30"));

    services.AddScoped<AddressRepository>();
    services.AddScoped<ContactRepository>();
    services.AddScoped<CountryRepository>();
    services.AddScoped<OccupationRepository>();
    services.AddScoped<SalaryRepository>();

    services.AddScoped<AddressService>();
    services.AddScoped<ContactService>();
    services.AddScoped<OccupationService>();

}).Build();

builder.Start();

Console.ReadKey();
var contactService = builder.Services.GetRequiredService<ContactService>();
var result = contactService.CreateContact(new ContactDto
{
    FirstName = "Hank",
    LastName = "Mohawk",
    Email = "my@domain.com",
    City = "Mora",
    PostalCode = "12345",
    StreetName = "Gågatan 12",
    Country = "Sverige",
    Continent = "Europa",
    Occupation = "Student",
    Description = "Learn things",
    Salary = 100
});

if (result)
    Console.WriteLine("Lyckades");
else
    Console.WriteLine("Misslyckades");

Console.ReadKey();