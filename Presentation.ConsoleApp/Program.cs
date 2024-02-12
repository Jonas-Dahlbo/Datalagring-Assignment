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

Console.Clear();
Console.ReadKey();
var contactService = builder.Services.GetRequiredService<ContactService>();
var result = contactService.CreateContact(new ContactDto
{
    FirstName = "Per",
    LastName = "Eriksson",
    Email = "Per@domain.com",
    City = "Mora",
    PostalCode = "12345",
    StreetName = "Gågatan 12",
    Country = "Norge",
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

result = contactService.RemoveContact("my@domain.com");
if (result)
    Console.WriteLine("Contact Removed");
else
    Console.WriteLine("Failed");

var result2 = contactService.GetOneContact("new@domain.com");

if(result2 == null)
{
    Console.WriteLine("GetOne Contact Failed!");
}

Console.WriteLine($"{result2.FirstName} {result2.LastName} {result2.Email} {result2.Continent} {result2.Country} {result2.City} {result2.PostalCode} {result2.StreetName} {result2.Occupation} {result2.Salary}");

Console.ReadKey();