using System;
using System.Threading.Tasks;
using empty.Enums;

namespace empty;

public class Game
{
    private bool _isRunning = true;
    private readonly PetManager _petManager = new PetManager();

    
    public async Task StartAsync()
    {
        _petManager.PetDied += (_, name) =>
        {
            Console.WriteLine($" {name} has died...");
        };
        _ = Task.Run(async () =>
        {
            while (_isRunning)
            {
                await Task.Delay(5000);
            }
        });
        
        while (_isRunning)
        {
            ShowMainMenu();
            
            Console.Write("Choose an option: ");
            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    await AdoptPetMenu();
                    break;
                case "2":
                    _petManager.ShowAllPetStats();
                    break;
                case "3":
                    UseItem();
                    break;
                case "4":
                    ShowCreatorInfo();
                    break;
                case "0":
                    _isRunning = false;
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
    
    private async Task AdoptPetMenu()
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
            string? name = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Pet name cannot be empty. Adoption cancelled.");
                return;
            }

            _petManager.AdoptPet((PetType)selectedType, name);
            await Task.Delay(1000);
        }
        else
        {
            Console.WriteLine("Invalid pet type selected.");
        }
    }
    
    private void UseItem()
    {
        _petManager.UseItemOnPet();
    }
    
    private List<Item> items = new List<Item>
    {
        new Item(ItemType.Food, "Apple"),
        new Item(ItemType.Bed, "Soft Bed"),
        new Item(ItemType.Toy, "Chew Toy")
    };
    
}