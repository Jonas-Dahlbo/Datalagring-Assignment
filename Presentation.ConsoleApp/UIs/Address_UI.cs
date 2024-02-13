using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;

namespace Presentation.ConsoleApp.UI;

public class Address_UI(AddressService addressService)
{
    private readonly AddressService _addressService = addressService;

    public void CreateAddress_UI()
    {
        AddressDto addressDto = new AddressDto();

        Console.Clear();
        Console.WriteLine("------CREATE ADDRESS------\n");

        Console.WriteLine("Enter Continent: ");
        addressDto.Continent = Console.ReadLine()!;
        Console.WriteLine("Enter Country: ");
        addressDto.Country = Console.ReadLine()!;
        Console.WriteLine("Enter City: ");
        addressDto.City = Console.ReadLine()!;
        Console.WriteLine("Enter Postal Code: ");
        addressDto.PostalCode = Console.ReadLine()!;
        Console.WriteLine("Enter Street Name: ");
        addressDto.StreetName = Console.ReadLine()!;

        bool result = _addressService.CreateAdress(addressDto);

        if (result)
        {
            Console.WriteLine("Address created");
        }
        else
        {
            Console.WriteLine("Could not create Address, would you like to try again? y/n");
            if (Console.ReadLine()!.ToLower() == "y")
            {
                CreateAddress_UI();
            }
        }
    } //ADD MENU_UI ?

    public void GetAddresses_UI()
    {

        var addresses = _addressService.GetAllAddresses();
        
        Console.WriteLine("Continent     Country     City     Postal Code     Street Name \n\n");
        foreach (var address in addresses)
        {
            Console.WriteLine($"{address.Continent}     {address.Country}     {address.City}     {address.PostalCode}     {address.StreetName}");
        }
    }

    public void UpdateAddress_UI()
    {
        Console.Clear();
        
        AddressDto addressDto = new AddressDto();

        Console.WriteLine("-----Choose which Address to Update-----\n");
        GetAddresses_UI();

        Console.WriteLine("\nEnter Street Name: ");
        addressDto.StreetName = Console.ReadLine()!;
        Console.WriteLine("Enter Postal Code: ");
        addressDto.PostalCode= Console.ReadLine()!;

        var addressToUpdate = _addressService.GetOneAddress(addressDto.StreetName, addressDto.PostalCode);

        Console.Clear() ;
        Console.WriteLine("-----Enter Updated Address Information-----");

        Console.WriteLine("Enter Continent: ");
        addressToUpdate.Item2.Country.Continent = Console.ReadLine()!;
        Console.WriteLine("Enter Country: ");
        addressToUpdate.Item2.Country.Country = Console.ReadLine()!;
        Console.WriteLine("Enter City: ");
        addressToUpdate.Item2.City = Console.ReadLine()!;
        Console.WriteLine("Enter Postal Code: ");
        addressToUpdate.Item2.PostalCode = Console.ReadLine()!;
        Console.WriteLine("Enter Street Name: ");
        addressToUpdate.Item2.StreetName = Console.ReadLine()!;
        
        if (addressToUpdate.Item2 != null)
        {
            var newAddress = _addressService.UpdateAddress(addressToUpdate.Item2);
            Console.WriteLine($"{newAddress.Continent}     {newAddress.Country}     {newAddress.City}     {newAddress.PostalCode}     {newAddress.StreetName}\n");
        }
        else
        {
            Console.WriteLine("Something went wrong, would you like to try again? y/n");

            if (Console.ReadLine()!.ToLower() == "y")
            {
                UpdateAddress_UI();
            }
        }
    }// ADD MENU_UI ?


}
