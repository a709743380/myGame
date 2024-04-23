using ConsoleApp2.Common;
using ConsoleApp2.enums;
using ConsoleApp2.Extensions;
using ConsoleApp2.GameAttributes;
using ConsoleApp2.IGame;
using System.Text;

namespace ConsoleApp2.Games
{
    [GameAttribute(GameList.OneAOneB)]
    public class OneAOneBGame : IGameTask
    {
        private static CommonGear _commonGear = new();
        public bool Regame { get; set; } = false;
        private void Play1A1B()
        {
            string guessNumber = InitGuess();

            Console.WriteLine("ANS:" + guessNumber);
            Console.WriteLine("1A1B");
            Console.ForegroundColor = ConsoleColor.White;
            bool run = true;
            bool isAnsIt;
            //開始遊戲
            while (run)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;//設置Console字體顏色
                    Console.Write("請數入4位數字:");
                    string? inputGuess = Console.ReadLine();
                    if (inputGuess?.Length == 4)
                    {
                        //檢查輸入的字是否為數字
                        if (inputGuess.CheckInt(out _))
                        {
                            //與答案比較位置與數字的正確性 輸出 xAxB out是否結束本局
                            string show = ToGuessInput(inputGuess, guessNumber, out isAnsIt);
                            //顯示判斷結果
                            Console.WriteLine(show);
                            if (isAnsIt)
                            {
                                //猜中檢查是否要重新開始
                                var result = _commonGear.DoReSrart();
                                if (result.restart)
                                {
                                    guessNumber = InitGuess();
                                    Console.WriteLine("已經重新開始");
                                }
                                else
                                {
                                    run = false;
                                    Regame = result.regame;
                                }
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("請數入4位數字");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("請數入4位數字");
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("請數入4位數字");
                }
            }
        }
        /// <summary>
        /// 產生一個隨機答案
        /// </summary>
        /// <returns></returns>
        private string InitGuess()
        {
            string guessbase = "0123456789";
            string guessString = "";
            StringBuilder baseBuilder = new StringBuilder(guessbase);
            //總共4位數字
            for (int i = 0; i < 4; i++)
            {
                Random rnd = new Random();
                //隨機取得不重複數字
                int randomIndex = rnd.Next(0, baseBuilder.Length);
                char basechar = baseBuilder[randomIndex];
                guessString += basechar;
                baseBuilder.Remove(randomIndex, 1);
            }
            return guessString;
        }

        /// <summary>
        /// 與答案比較位置與數字的正確性
        /// </summary>
        /// <param name="inputGuess"></param>
        /// <param name="ans"></param>
        /// <param name="isAnsIt"></param>
        /// <returns></returns>
        private string ToGuessInput(string? inputGuess, string ans, out bool isAnsIt)
        {
            isAnsIt = false;
            int showA = 0;
            int showB = 0;
            //A 位置對數字對  B位置不對數字對
            for (int i = 0; i < ans.Length; i++)
            {
                if (ans.Contains(inputGuess![i]))
                {
                    //數字對 B+1
                    showB++;
                    if (ans[i] == inputGuess[i])
                    {
                        //位置也對 B-1 A+1
                        showA++;
                        showB--;
                    }
                }
            }
            //四個數字位置都對 
            if (showA == 4)
            {
                isAnsIt = true;
            }

            return $"{showA}A{showB}B";
        }

        public void Execute()
        {
            Play1A1B();
        }
    }
}
