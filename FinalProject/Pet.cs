using System;
using System.Threading;
using System.Threading.Tasks;
using empty.Enums;

namespace empty;

public class Pet
{
    public string Name { get; }
    public PetType Type { get; }

    public int Hunger { get; set; } = 50;
    public int Sleep { get; set; } = 50;
    public int Fun { get; set; } = 50;

    public bool IsAlive { get; private set; } = true;

    public event Action<Pet> OnPetDied = delegate { };

    private CancellationTokenSource tokenSource;
    
    private bool isDead = false;

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

            if (!isDead&(Hunger <= 0 || Sleep <= 0 || Fun <= 0))
            {
                isDead = true;
                IsAlive = false;
                Console.WriteLine($"{Name} has died due to poor care");
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

    public void UpdateStatsOverTime()
    {
        Hunger += 1;
        Sleep += 1;
        Fun = Math.Max(Fun - 1, 0);
        
        //Console.WriteLine($"{Name}'s stats updated -> Hunger: {Hunger}, Sleep: {Sleep}, Fun: {Fun}");
    }
}