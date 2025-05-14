using System;
using System.Threading.Tasks;

namespace empty;

public class Game
{
    private bool İsRunning = true;
    private readonly PetManager petManager = new PetManager();

    public async Task startAsync()
    {
        Console.Clear();
        Console.WriteLine("Welcome to DGD 208 Final Project!");

        while (İsRunning)
        {
            ShowMainMenu();
            
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    AdoptPet();
                    break;
                case "2":
                    petManager.ShowAllPetStats();
                    break;
                case "3":
                    UseItem();
                    break;
                case "4":
                    ShowCreatorInfo();
                    break;
                case "0":
                    İsRunning = false;
                    Console.WriteLine("Exiting game...");
                    break;
                default:
                    Console.WriteLine("Invalid input. Try again...");
                    break;
            }
            
            await Task.Delay(1000);
            Console.Clear();
        }
    }

    private void ShowMainMenu()
    {
        Console.WriteLine("===== MAIN MENU =====");
        Console.WriteLine("1. Adopt a Pet");
        Console.WriteLine("2. View All Pet Stats");
        Console.WriteLine("3. Use an Item");
        Console.WriteLine("4. Show Creator Info");
        Console.WriteLine("0. Exit Game");
        Console.WriteLine("=====================");
    }
    
    private void ShowCreatorInfo()
    {
        Console.WriteLine("Created by Enes Inan - 2299112756");
    }
    
    private void AdoptPet()
    {
        Console.WriteLine("Choose a pet type:");
        foreach (var type in Enum.GetValues(typeof(PetType)))
        {
            Console.WriteLine($"{(int)type} - {type}");
        }

        Console.Write("Enter pet type number: ");
        if (int.TryParse(Console.ReadLine(), out int selectedType) &&
            Enum.IsDefined(typeof(PetType), selectedType))
        {
            Console.Write("Enter a name for your pet: ");
            string name = Console.ReadLine();

            petManager.AdoptPet((PetType)selectedType, name);
        }
        else
        {
            Console.WriteLine("Invalid pet type selected.");
        }
    }
    
    private void UseItem()
    {
        Console.WriteLine("UseItem functionality will be added later.");
        // Bu kısım ileride geliştirilecek
    }
}