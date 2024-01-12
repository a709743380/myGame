using Autofac.Core;
using Autofac;
using ConsoleApp2.Common;
using ConsoleApp2.enums;
using ConsoleApp2.Games;
using ConsoleApp2.GameAttr;
using ConsoleApp2.IGame;
using ConsoleApp2.Services;
using Autofac.Features.Metadata;

namespace ConsoleApp2
{
    internal class Program
    {
        delegate void GameMethod();

        static void Main(string[] args)
        {
            GameList[] allGame = (GameList[])Enum.GetValues(typeof(GameList));
            foreach (var game in allGame)
            {
                Console.WriteLine((int)game + " . " + game);
            }

            Console.WriteLine("請輸入遊戲的編號:");
            if (int.TryParse(Console.ReadLine(), out int order))
            {
                if (Enum.IsDefined(typeof(GameList), order))
                {
                    GameServices game = new GameServices();
                    game.PlayGame((GameList)order);
                }
                else
                {
                    Console.WriteLine("沒有這個遊戲機掰");
                }
            }
            else
            {
                Console.WriteLine("請輸入有效的數字！");
            }
        }
    }
}