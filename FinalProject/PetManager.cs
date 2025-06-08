using System;
using System.Collections.Generic;
using empty.Enums;

namespace empty;
public class PetManager
{
    private readonly List<Pet> pets = new();
    public event EventHandler<string>? PetDied;

    public void AdoptPet(PetType type, string name)
    {
        Pet newPet = new(type, name);
        pets.Add(newPet);
        Console.WriteLine($"{name} the {type} was adopted!");
        newPet.OnPetDied += (pet) =>
        {
            PetDied?.Invoke(this, pet.Name);
            RemovePet(pet);
        };
    }

    public void ShowAllPetStats()
    {
        if (pets.Count == 0)
        {
            Console.WriteLine("No pets adopted yet.");
            return;
        }

        foreach (var pet in pets)
        {
            Console.WriteLine($"Name: {pet.Name} | Type: {pet.Type} | Hunger: {pet.Hunger} | Sleep: {pet.Sleep} | Fun: {pet.Fun}");
        }
    }
    public void UpdateAllPetStats()
    {
        foreach (var pet in pets.ToList())
        {
            if (pet.IsAlive)
            {
                pet.ShowStats();
            }
        }
    }
    public void UseItemOnPet()
    {
        if (pets.Count == 0)
        {
            Console.WriteLine("No pets to use items on.");
            return;
        }
        
        Console.WriteLine("Choose a pet:");
        for (int i = 0; i < pets.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {pets[i].Name} ({pets[i].Type})");
        }
        
        Console.Write("Enter pet number: ");
        if (!int.TryParse(Console.ReadLine(), out int petChoice) || petChoice < 1 || petChoice > pets.Count)
        {
            Console.WriteLine("Invalid pet selection.");
            return;
        }
        
        Pet selectedPet = pets[petChoice - 1];

        Console.WriteLine("Choose an item to use:");
        foreach (var itemType in Enum.GetValues(typeof(ItemType)))
        {
            Console.WriteLine($"{(int)itemType} - {itemType}");
        }

        Console.Write("Enter item type number: ");
        if (!int.TryParse(Console.ReadLine(), out int itemTypeChoice) || !Enum.IsDefined(typeof(ItemType), itemTypeChoice))
        {
            Console.WriteLine("Invalid item type.");
            return;
        }

        ItemType chosenType = (ItemType)itemTypeChoice;
        Item item = new(chosenType, chosenType.ToString());

        item.ApplyTo(selectedPet);
    }
    public void RemovePet(Pet pet)
    {
        if (pets.Contains(pet))
        {
            pets.Remove(pet);
        }
    }
    private void HandlePetDeath(Pet pet)
    {
        RemovePet(pet);
    }
}