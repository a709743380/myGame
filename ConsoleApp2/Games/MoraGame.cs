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
        //基本參數設定
        private static CommonGear _commonGear = new();
        private int countYouWin = 0;
        private int countCWin = 0;
        private int TotalCount = 1;
        private int totalWin = 0;
        private const string Rock = "✊";
        private const string Scissors = "✌️";
        private const string Paper = "✋";
        public bool Regame { get; set; } = false;

        /// <summary>
        /// 取得猜拳內容
        /// </summary>
        private Array values = Enum.GetValues(typeof(Mora));
        //遊戲開始
        private void PlayMora()
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
                    string winOrlose = countYouWin - countCWin > 0 ? "贏" : "輸";
                    Console.Write($"遊戲結束你{winOrlose}了");
                    var result = _commonGear.DoReSrart();
                    running = result.restart;
                    Regame = result.regame;
                    if (running)
                        init();

                }

            } while (running);

        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void init()
        {
            countYouWin = 0;
            countCWin = 0;
            TotalCount = 1;
            Console.Write("請輸入贏幾局為勝利：");
            Console.ReadLine().CheckInt(out this.totalWin);
        }
        /// <summary>
        /// 電腦出拳
        /// </summary>
        /// <returns></returns>
        private Mora getCMora()
        {
            Random rnd = new Random();
            int random = rnd.Next(values.Length);
            var randomMora = (Mora)values.GetValue(random);
            return randomMora;
        }
        /// <summary>
        /// 檢查你和電腦猜拳誰贏了
        /// </summary>
        /// <param name="diff"></param>
        private void CheckWin(int diff)
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
        /// <summary>
        /// 檢查你輸入的是不是 已知方法
        /// </summary>
        /// <param name="mora"></param>
        /// <returns></returns>
        private string GetMoraSymbol(Mora mora)
        {
            switch (mora)
            {
                case Mora.Rock:
                    return Rock;
                case Mora.Scissors:
                    return Scissors;
                case Mora.Paper:
                    return Paper;
                default:
                    Console.WriteLine("請輸入正確的數字！！");
                    return null;
            }
        }

        private enum Mora
        {
            Rock = 0,
            Scissors = 2,
            Paper = 5
        }
        public void Execute()
        {
            PlayMora();
        }
    }
}
