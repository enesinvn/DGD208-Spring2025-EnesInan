using System;
using System.Threading.Tasks;

namespace empty 
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Game game = new Game();
            await game.StartAsync();
        }
    }
}