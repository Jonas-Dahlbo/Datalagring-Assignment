using Infrastructure.Dtos;
using Infrastructure.Services;

namespace Presentation.ConsoleApp.UIs;

public class Occupation_UI(OccupationService occupationService)
{
    private readonly OccupationService _occupationService = occupationService;

    public void CreateOccupation_UI()
    {
        OccupationDto occupationDto = new OccupationDto();

        Console.Clear();
        Console.WriteLine("------CREATE Occupation------\n");

        Console.WriteLine("Enter Occupation: ");
        occupationDto.Occupation = Console.ReadLine()!;
       
        Console.WriteLine("Do you wish to enter a description of the Occupation? y/n");
        if (Console.ReadLine()!.ToLower() == "y")
        {
            Console.WriteLine("Enter Description: ");
            occupationDto.Description = Console.ReadLine()!;
        }

        Console.WriteLine("Enter Salary: ");
        occupationDto.Salary = decimal.Parse(Console.ReadLine()!);


        bool result = _occupationService.CreateOccupation(occupationDto);

        if (result)
        {
            Console.WriteLine("Occupation created");
        }
        else
        {
            Console.WriteLine("Could not create Occupation, would you like to try again? y/n");
            if (Console.ReadLine()!.ToLower() == "y")
            {
                CreateOccupation_UI();
            }

            //MENU_UI();
        }
    } //ADD MENU_UI ?

    public void GetOccupations_UI()
    {

        var occupations = _occupationService.GetAllOccupations();

        Console.WriteLine("Occupation     Salary      Description\n");
        foreach (var occupation in occupations)
        {
            Console.WriteLine($"{occupation.Occupation}     {occupation.Salary}      {occupation.Description}");
        }
    }

    public void UpdateOccupation_UI()
    {
        Console.Clear();

        OccupationDto occupationDto = new OccupationDto();

        Console.WriteLine("-----Choose which Occupation to Update-----\n");
        GetOccupations_UI();

        Console.WriteLine("\nEnter Occupation: ");
        occupationDto.Occupation = Console.ReadLine()!;

        var occupationToUpdate = _occupationService.GetOneOccupation(occupationDto.Occupation);

        Console.Clear();
        Console.WriteLine("-----Enter Updated Occupation Information-----");

        Console.WriteLine("Enter Occupation: ");
        occupationToUpdate.Item2.Occupation = Console.ReadLine()!;
        Console.WriteLine("Enter Country: ");
        occupationToUpdate.Item2.Description = Console.ReadLine()!;
        Console.WriteLine("Enter Salary: ");
        occupationToUpdate.Item2.Salary.Salary = decimal.Parse(Console.ReadLine()!);
        

        if (occupationToUpdate.Item2 != null)
        {
            var newOccupation = _occupationService.UpdateOccupation(occupationToUpdate.Item2);
            Console.WriteLine($"{newOccupation.Occupation}     {newOccupation.Salary}     {newOccupation.Description}\n");
        }
        else
        {
            Console.WriteLine("Something went wrong, would you like to try again? y/n");

            if (Console.ReadLine()!.ToLower() == "y")
            {
                UpdateOccupation_UI();
            }
        }
    }// ADD MENU_UI ?
}
