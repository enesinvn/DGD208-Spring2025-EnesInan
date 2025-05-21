using System;
using System.Collections.Generic;
using empty.Enums;

namespace empty;
public class PetManager
{
    private readonly List<Pet> pets = new();

    public void AdoptPet(PetType type, string name)
    {
        Pet newPet = new(type, name);
        pets.Add(newPet);
        Console.WriteLine($"{name} the {type} was adopted!");
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
}