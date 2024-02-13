using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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
    services.AddScoped<Contact_UI>();
    services.AddScoped<Menu_UI>();
}).Build();

builder.Start();

var MenuUI = builder.Services.GetRequiredService<Menu_UI>();
MenuUI.MainMenu_UI();

