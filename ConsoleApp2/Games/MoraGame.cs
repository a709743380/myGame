using ConsoleApp2.Common;
using ConsoleApp2.enums;
using ConsoleApp2.Extensions;
using ConsoleApp2.GameAttributes;
using ConsoleApp2.IGame;
using System.Text;

namespace ConsoleApp2.Games
{
    [GameAttribute(GameList.MoraGame)]
    public class MoraGame : IGameTask
    {
        private static CommonGear _commonGear = new();
        private int countYouWin = 0;
        private int countCWin = 0;
        private int TotalCount = 1;
        private int totalWin = 0;
        private Array values = Enum.GetValues(typeof(Mora));
        public void PlayMora()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;
            init();
            do
            {
                Console.Write($"【{totalWin}勝局】【目前勝利{countYouWin}局】請出拳第{TotalCount}局 1：\u270A  2：\u270C  3：\u270B:  ");

                string myGuess = Console.ReadLine();
                myGuess.CheckInt(out int guess);
                if (guess > values.Length)
                {
                    Console.WriteLine("請輸入正確的代號！！");
                    continue;
                }

                var cNumber = getCMora();
                var youGuess = (Mora)values.GetValue(guess - 1);

                string myshow = GetMoraSymbol(youGuess);
                string cshow = GetMoraSymbol(cNumber);

                if (string.IsNullOrEmpty(myshow) || string.IsNullOrEmpty(cshow))
                    continue;
                
                Console.WriteLine($"你:{myshow}      VS     {cshow}: 電腦");
                CheckWin((int)youGuess - (int)cNumber);
                if (countYouWin == totalWin || countCWin == totalWin)
                {
                    init();
                    string winOrlose = countYouWin - countCWin > 0 ? "贏" : "輸";
                    Console.Write($"遊戲結束你{winOrlose}了");
                    running = _commonGear.DoReSrart();
                }

            } while (running);

        }

        private void init()
        {
            countYouWin = 0;
            countCWin = 0;
            TotalCount = 1;
            Console.Write("請輸入贏幾局為勝利：");
            Console.ReadLine().CheckInt(out this.totalWin);
        }

        private Mora getCMora()
        {
            Random rnd = new Random();
            int random = rnd.Next(values.Length);
            var randomMora = (Mora)values.GetValue(random);
            return randomMora;
        }
        public void CheckWin(int diff)
        {
            if (diff == -2 || diff == -3 || diff == 5)
            {
                Console.WriteLine($"你贏了");
                countYouWin++;
            }
            else if (diff == 2 || diff == 3 || diff == -5)
            {
                Console.WriteLine($"你輸了");
                countCWin++;
            }
            else
            {
                Console.WriteLine($"平局");
            }

            TotalCount++;
        }
        public string GetMoraSymbol(Mora mora)
        {
            switch (mora)
            {
                case Mora.Rock:
                    return MoraSymbol.Rock;
                case Mora.Scissors:
                    return MoraSymbol.Scissors;
                case Mora.Paper:
                    return MoraSymbol.Paper;
                default:
                    Console.WriteLine("請輸入正確的數字！！");
                    return null;
            }
        }

        public enum Mora
        {
            Rock = 0,
            Scissors = 2,
            Paper = 5
        }

        public class MoraSymbol
        {
            public const string Rock = "✊";
            public const string Scissors = "✌️";
            public const string Paper = "✋";
        }
        public void Execute()
        {
            PlayMora();
        }
    }
}
