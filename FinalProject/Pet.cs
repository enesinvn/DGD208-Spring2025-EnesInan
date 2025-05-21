using System;
using System.Threading;
using System.Threading.Tasks;
using empty.Enums;

namespace empty;

public class Pet
{
    public string Name { get; }
    public PetType Type { get; }

    public int Hunger { get; private set; } = 50;
    public int Sleep { get; private set; } = 50;
    public int Fun { get; private set; } = 50;

    public bool IsAlive { get; private set; } = true;

    public event Action<Pet> OnPetDied = delegate { };

    private CancellationTokenSource tokenSource;

    public Pet(PetType type, string name)
    {
        Type = type;
        Name = name;
        tokenSource = new CancellationTokenSource();
        _ = DecreaseStatsAsync(tokenSource.Token);
    }

    public async Task DecreaseStatsAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            await Task.Delay(3000); // Her 3 saniyede bir stat azalır

            Hunger--;
            Sleep--;
            Fun--;

            if (Hunger <= 0 || Sleep <= 0 || Fun <= 0)
            {
                IsAlive = false;
                OnPetDied?.Invoke(this);
                break;
            }
        }
    }
    
    public void Stop()
    {
        tokenSource.Cancel();
    }

    public void Feed(int amount)
    {
        Hunger = Math.Min(100, Hunger + amount);
    }

    public void Play(int amount)
    {
        Fun = Math.Min(100, Fun + amount);
    }

    public void SleepPet(int amount)
    {
        Sleep = Math.Min(100, Sleep + amount);
    }

    public void ShowStats()
    {
        Console.WriteLine($"[{Type}] {Name} | Hunger: {Hunger} | Sleep: {Sleep} | Fun: {Fun}");
    }
}