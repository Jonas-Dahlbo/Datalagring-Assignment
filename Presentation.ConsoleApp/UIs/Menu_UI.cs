using Infrastructure.Services;

namespace Presentation.ConsoleApp.UIs;

public class Menu_UI(ContactService contactService, AddressService addressService, OccupationService occupationService)
{
    private readonly ContactService _contactService = contactService;
    private readonly AddressService _addressService = addressService;
    private readonly OccupationService _occupationService = occupationService;

    
    public void MainMenu_UI()
    {
        Console.Clear();
        Console.WriteLine("------Database Management Interface------\n");
        Console.WriteLine("Contact Menu: \n");
        Console.WriteLine("1: Create a new Contact.");
        Console.WriteLine("2: See all Contacts: ");
        Console.WriteLine("3: Update a Contact.");
        Console.WriteLine("4: Remove a Contact");
        Console.WriteLine("5: Address Menu");
        Console.WriteLine("6: Occupation Menu");
        Console.WriteLine("7: EXIT");

        var ContacUI = new Contact_UI(_contactService);

        switch (Console.ReadLine()!.Trim())
        {
            case "1":
                Console.Clear();
                ContacUI.CreateContact_UI();
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                MainMenu_UI();
                break;
             case "2":
                Console.Clear();
                ContacUI.GetContacts_UI();
                Console.WriteLine("\n------Do you wish to get detailed information about a Contact? y/n------");
                if (Console.ReadLine()!.ToLower().Trim() == "y")
                    ContacUI.GetOneContact_UI();
               
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                MainMenu_UI();
                break;
            case "3":
                Console.Clear();
                ContacUI.UpdateContact_UI();
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                MainMenu_UI();
                break;
            case "4":
                Console.Clear();
                ContacUI.RemoveContact_UI();
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                MainMenu_UI();
                break;
            case "5":
                AddressMenu_UI();
                break;
            case "6":
                OccupationMenu_UI();
                break;
            case "7":
                break;
            default:
                Console.Clear();
                Console.WriteLine("--------Invalid input, press any key to return to Menu.--------");
                Console.ReadKey();
                MainMenu_UI();
                break;


        }
    }
    
    public void AddressMenu_UI()
    {
        Console.Clear();
        Console.WriteLine("Address Menu: \n");
        Console.WriteLine("1: Create a new Address.");
        Console.WriteLine("2: See all Addresses.");
        Console.WriteLine("3: Update an Address.");
        Console.WriteLine("4: Return to Main Menu.");

        var AddressUI = new Address_UI(_addressService);

        switch (Console.ReadLine()!.Trim())
        {
            case "1":
                Console.Clear();
                AddressUI.CreateAddress_UI();
                Console.WriteLine("\nPress any key to return to the Menu");
                Console.ReadKey();
                AddressMenu_UI();
                break; 
            case "2":
                Console.Clear();
                AddressUI.GetAddresses_UI();
                Console.WriteLine("\nPress any key to return to the Menu");
                Console.ReadKey();
                AddressMenu_UI();
                break; 
            case "3":
                Console.Clear();
                AddressUI.UpdateAddress_UI();
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                AddressMenu_UI();
                break;
            case "4":
                MainMenu_UI();
                break;
            default:
                Console.Clear();
                Console.WriteLine("--------Invalid input, press any key to return to Menu.--------");
                Console.ReadKey();
                AddressMenu_UI();
                break;
        }
    }

    public void OccupationMenu_UI()
    {
        Console.Clear();
        Console.WriteLine("Occupation Menu: \n");
        Console.WriteLine("1: Create a new Occupation.");
        Console.WriteLine("2: See all Occupations.");
        Console.WriteLine("3: Update an Occupation.");
        Console.WriteLine("4: Return to Main Menu.");

        var OccupationUI = new Occupation_UI(_occupationService);

        switch (Console.ReadLine()!.Trim())
        {
            case "1":
                Console.Clear();
                OccupationUI.CreateOccupation_UI();
                Console.WriteLine("\nPress any key to return to the Menu");
                Console.ReadKey();
                OccupationMenu_UI();
                break;
            case "2":
                Console.Clear();
                OccupationUI.GetOccupations_UI();
                Console.WriteLine("\nPress any key to return to the Menu");
                Console.ReadKey();
                OccupationMenu_UI();
                break;
            case "3":
                Console.Clear();
                OccupationUI.UpdateOccupation_UI();
                Console.WriteLine("Press any key to return to the main Menu");
                Console.ReadKey();
                OccupationMenu_UI();
                break;
            case "4":
                MainMenu_UI();
                break;
            default:
                Console.Clear();
                Console.WriteLine("--------Invalid input, press any key to return to Menu.--------");
                Console.ReadKey();
                OccupationMenu_UI();
                break;
        }
    }
}
