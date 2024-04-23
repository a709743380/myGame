using ConsoleApp2.enums;
using ConsoleApp2.Extensions;
using ConsoleApp2.Services;
namespace ConsoleApp2
{
    internal class Program
    {
        private  static GameAutoFac _gameServices = new GameAutoFac();
        // 程式啟動時的靜態建構子
        static Program()
        {
            GameAutoFac.Register();
        }


        static void Main(string[] args)
        {
            GameList[] allGame = (GameList[])Enum.GetValues(typeof(GameList));
        start:
            foreach (var game in allGame)
            {
                Console.WriteLine((int)game + " . " + game);
            }

            Console.WriteLine("請輸入遊戲的編號:");

            string orderstring = Console.ReadLine();
            if (orderstring.CheckInt(out int order))
            {
                if (Enum.IsDefined(typeof(GameList), order))
                {
                   bool regame = _gameServices.PlayGame((GameList)order);
                    if(regame)
                        goto start;
                }
                else
                {
                    Console.WriteLine("沒有這個遊戲機掰");
                }
            }
            else
            {
                Console.WriteLine("請輸入有效的數字！");
                goto start;
            }
        }
    }
}