using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Services;
using System.Net;

namespace Presentation.ConsoleApp.UI;

public class Address_UI
{
    private readonly AddressService _addressService;

    public Address_UI(AddressService addressService)
    {
        _addressService = addressService;
    }



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

        AddressEntity result = _addressService.CreateAdress(addressDto);

        if (result != null)
        {
            Console.WriteLine("Address created");
        }
        else
        {
            Console.WriteLine("Could not create address, would you like to try again? y/n");
            if (Console.ReadLine()!.ToLower() == "y")
            {
                CreateAddress_UI();
            }

            //MENU_UI();
        }
    } //ADD MENU_UI ?

    public void GetAddress_UI()
    {
        Console.Clear();
        var addressDto = new AddressDto();

        Console.WriteLine("------Get One Address------");

        Console.WriteLine("Enter Street Name: ");
        addressDto.StreetName = (Console.ReadLine()!);

        Console.WriteLine("Enter Postal Code: ");
        addressDto.PostalCode = (Console.ReadLine()!);

        var address = _addressService.GetOneAddress(addressDto.StreetName, addressDto.PostalCode);

        Console.Clear();

        if (address.Item1 != null)
        {
            Console.WriteLine("Continent     Country     City     Postal Code     Street Name \n\n");
            Console.WriteLine($"{address.Item1.Continent}     {address.Item1.Country}     {address.Item1.City}     {address.Item1.PostalCode}     {address.Item1.StreetName}");
        }
        else
        {
            Console.WriteLine("Something went wrong, would you like to try again? y/n");
            
            if (Console.ReadLine()!.ToLower() == "y")
            {
                GetAddress_UI();
            }

            //MENU_UI
        }


    }// ADD MENU_UI ?

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
        Console.WriteLine("-----Enter New Address Information-----");

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

        addressToUpdate.Item2 = _addressService.CreateAdress(addressDto);
        
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
                CreateAddress_UI();
            }
        }
    }// ADD MENU_UI ?


}
