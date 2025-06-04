using empty.Enums;

namespace empty;

public class Item
{
    public ItemType Type { get; }
    public string Name { get; }

    public Item(ItemType type, string name)
    {
        Type = type;
        Name = name;
    }

    public void ApplyTo(Pet pet)
    {
        switch (Type)
        {
            case ItemType.Food:
                pet.Hunger += 20;
                break;
            case ItemType.Bed:
                pet.Sleep += 20;
                break;
            case ItemType.Toy:
                pet.Fun += 20;
                break;
        }

        Console.WriteLine($"{Name} used on {pet.Name}. {Type} increased!");
    }
}