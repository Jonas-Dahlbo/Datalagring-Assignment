using Infrastructure.Dtos;
using Infrastructure.Services;

namespace Presentation.ConsoleApp.UIs;

public class Contact_UI(ContactService contactService)
{
    private readonly ContactService _contactService = contactService;

    public void CreateContact_UI()
    {
        ContactDto contactDto = new ContactDto();

        Console.Clear();
        Console.WriteLine("------CREATE Contact------\n");

        Console.WriteLine("Enter First Name: ");
        contactDto.FirstName = Console.ReadLine()!;
        Console.WriteLine("Enter Last Name: ");
        contactDto.LastName = Console.ReadLine()!;
        Console.WriteLine("Enter Email Address: ");
        contactDto.Email = Console.ReadLine()!;
        Console.WriteLine("\nEnter Continent: ");
        contactDto.Continent = Console.ReadLine()!;
        Console.WriteLine("Enter Country: ");
        contactDto.Country = Console.ReadLine()!;
        Console.WriteLine("Enter City: ");
        contactDto.City = Console.ReadLine()!;
        Console.WriteLine("Enter Postal Code: ");
        contactDto.PostalCode = Console.ReadLine()!;
        Console.WriteLine("Enter Street Name: ");
        contactDto.StreetName = Console.ReadLine()!;
        Console.WriteLine("\nEnter Occupation: ");
        contactDto.Occupation = Console.ReadLine()!;
        Console.WriteLine("Do you wish to enter a description of the Occupation? y/n");
        if (Console.ReadLine()!.ToLower() == "y")
        {
            Console.WriteLine("Enter Description: ");
            contactDto.Description = Console.ReadLine()!;
        }
        Console.WriteLine("\nEnter Salary: ");
        contactDto.Salary = decimal.Parse(Console.ReadLine()!);


        bool result = _contactService.CreateContact(contactDto);

        if (result)
        {
            Console.WriteLine("Contact created");
        }
        else
        {
            Console.WriteLine("Could not create Contact, would you like to try again? y/n");
            if (Console.ReadLine()!.ToLower() == "y")
            {
                CreateContact_UI();
            }
        }
    } //ADD MENU_UI ?

    public void GetContacts_UI()
    {
        Console.WriteLine("------Contact List------\n");
        var contacts = _contactService.GetAllContacts();

        Console.WriteLine("First name     Last Name     Email     Country     City     Occupation\n\n");
        foreach (var contact in contacts)
        {
            Console.WriteLine($"{contact.FirstName}     {contact.LastName}     {contact.Email}     {contact.Country}     {contact.City}     {contact.Occupation}");
        }
    } 

    public void GetOneContact_UI()
    {
        Console.Clear();
        var contactDto = new ContactDto();

        Console.WriteLine("------Get One Contact------");

        Console.WriteLine("Enter Email: ");
        contactDto.Email = (Console.ReadLine()!);

        var contact = _contactService.GetOneContact(contactDto.Email);

        Console.Clear();

        if (contact.Item1 != null)
        {
            Console.WriteLine("First name     Last Name     Email\n");
            Console.WriteLine($"{contact.Item1.FirstName}     {contact.Item1.LastName}     {contact.Item1.Email}");
            Console.WriteLine("Continent     Country     City     Postal Code     Street Name\n");
            Console.WriteLine($"{contact.Item1.Continent}     {contact.Item1.Country}     {contact.Item1.City}     {contact.Item1.PostalCode}     {contact.Item1.StreetName}");
            Console.WriteLine("Occupation     Salary     Description\n");
            Console.WriteLine($"{contact.Item1.Occupation}     {contact.Item1.Salary}     {contact.Item1.Description}");
        }
        else
        {
            Console.WriteLine("Something went wrong, would you like to try again? y/n");

            if (Console.ReadLine()!.ToLower() == "y")
            {
                GetOneContact_UI();
            }
        }
    }

    public void UpdateContact_UI()
    {
        Console.Clear();

        ContactDto contactDto = new ContactDto();

        Console.WriteLine("-----Choose which Address to Update-----\n");
        GetContacts_UI();

        Console.WriteLine("\nEnter Email: ");
        contactDto.Email = Console.ReadLine()!;

        var contactToUpdate = _contactService.GetOneContact(contactDto.Email);

        Console.Clear();
        Console.WriteLine("-----Enter Updated Contact Information-----");

        Console.WriteLine("Enter First Name: ");
        contactDto.FirstName = Console.ReadLine()!;
        Console.WriteLine("Enter Last Name: ");
        contactDto.LastName = Console.ReadLine()!;
        Console.WriteLine("Enter Email Address: ");
        contactDto.Email = Console.ReadLine()!;
        Console.WriteLine("\nEnter Continent: ");
        contactDto.Continent = Console.ReadLine()!;
        Console.WriteLine("Enter Country: ");
        contactDto.Country = Console.ReadLine()!;
        Console.WriteLine("Enter City: ");
        contactDto.City = Console.ReadLine()!;
        Console.WriteLine("Enter Postal Code: ");
        contactDto.PostalCode = Console.ReadLine()!;
        Console.WriteLine("Enter Street Name: ");
        contactDto.StreetName = Console.ReadLine()!;
        Console.WriteLine("\nEnter Occupation: ");
        contactDto.Occupation = Console.ReadLine()!;
        Console.WriteLine("Do you wish to enter a description of the Occupation? y/n");
        if (Console.ReadLine()!.ToLower() == "y")
        {
            Console.WriteLine("Enter Description: ");
            contactDto.Description = Console.ReadLine()!;
        }
        Console.WriteLine("\nEnter Salary: ");
        contactDto.Salary = decimal.Parse(Console.ReadLine()!);

        if (contactToUpdate.Item2 != null)
        {
            var newContact = _contactService.UpdateContact(contactToUpdate.Item2);
            Console.WriteLine("First name     Last Name     Email\n");
            Console.WriteLine($"{newContact.FirstName}     {newContact.LastName}     {newContact.Email}");
            Console.WriteLine("Continent     Country     City     Postal Code     Street Name\n");
            Console.WriteLine($"{newContact.Continent}     {newContact.Country}     {newContact.City}     {newContact.PostalCode}     {newContact.StreetName}");
            Console.WriteLine("Occupation     Salary     Description\n");
            Console.WriteLine($"{newContact.Occupation}     {newContact.Salary}     {newContact.Description}");
        }
        else
        {
            Console.WriteLine("Something went wrong, would you like to try again? y/n");

            if (Console.ReadLine()!.ToLower() == "y")
            {
                UpdateContact_UI();
            }
        }
    }// ADD MENU_UI ?

    public void RemoveContact_UI()
    {
        Console.WriteLine("------Remove Contact------\n");

        Console.WriteLine("------Enter the Email Adress of the Contact to Remove------\n");
        GetContacts_UI();
        
        var contactEmail = Console.ReadLine()!;

        var result = _contactService.RemoveContact(contactEmail);

        if (result)
            Console.WriteLine("Contact Removed");
        else 
            Console.WriteLine("Something Went Wrong");
        Console.WriteLine("Press any key to return to menu");
        Console.ReadKey();
        Console.Clear();
    }
}
