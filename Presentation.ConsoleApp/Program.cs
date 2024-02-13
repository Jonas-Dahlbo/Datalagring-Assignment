using Infrastructure.Contexts;
using Infrastructure.Dtos;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp.UI;
using Presentation.ConsoleApp.UIs;

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

    services.AddScoped<Address_UI>();
    services.AddScoped<Occupation_UI>();
}).Build();

builder.Start();

Console.Clear();
Console.ReadKey();

var AddresUI = builder.Services.GetRequiredService<Address_UI>();
AddresUI.GetAddresses_UI();
Console.ReadKey();
var contactService = builder.Services.GetRequiredService<ContactService>();
var addressService = builder.Services.GetRequiredService<AddressService>();
var Occupation_UI = builder.Services.GetRequiredService<Occupation_UI>();

/*var result = addressService.RemoveAdress("edcb96d1-eea5-4724-a925-0ba41b59644f");

if (result)
    Console.WriteLine("Lyckades");
else
    Console.WriteLine("Misslyckades");
Console.ReadKey();*/
/*
var result2 = contactService.CreateContact(new ContactDto
{
    FirstName = "Per",
    LastName = "Eriksson",
    Email = "Test4@domain.com",
    City = "Mora",
    PostalCode = "12345",
    StreetName = "TestGatan",
    Country = "Tyskland",
    Continent = "Europa",
    Occupation = "Unemployed",
    Salary = 1002
}); */
Occupation_UI.GetOccupations_UI();
Console.ReadKey();
/* var result2 = contactService.GetOneContact("new@domain.com");

if(result2 == null)
{
    Console.WriteLine("GetOne Contact Failed!");
}

Console.WriteLine($"{result2.FirstName} {result2.LastName} {result2.Email} {result2.Continent} {result2.Country} {result2.City} {result2.PostalCode} {result2.StreetName} {result2.Occupation} {result2.Salary}");
*/