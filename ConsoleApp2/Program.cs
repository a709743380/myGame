using System.Text;

namespace ConsoleApp2
{
    internal class Program
    {
        delegate void GameMethod();
        static void Main(string[] args)
        {
            foreach (var game in InitGame())
            {
                Console.WriteLine(game);
            }
            Console.WriteLine("請輸入遊戲的編號:");
            if (int.TryParse(Console.ReadLine(), out int order))
            {
                // 使用字典將 GameList 列舉值映射到對應的委派
                Dictionary<GameList, GameMethod> gameDictionary = new Dictionary<GameList, GameMethod>
                {
                    { GameList.OneAOneB, Play1A1B },
                    { GameList.Guess, GuessNumber }
                    // 添加其他遊戲和方法的映射
                };

                // 檢查字典中是否存在對應的方法，存在則執行
                if (gameDictionary.TryGetValue((GameList)order, out GameMethod? selectedGame))
                {
                    selectedGame();
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

        #region 1A1B
        private static void Play1A1B()
        {
            string guessNumber = InitGuess();

            Console.WriteLine("ANS:" + guessNumber);
            Console.WriteLine("1A1B");
            Console.ForegroundColor = ConsoleColor.White;
            bool run = true;
            bool isAnsIt;
            while (run)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("請數入4位數字:");
                    string? inputGuess = Console.ReadLine();
                    if (inputGuess?.Length == 4)
                    {
                        if (inputGuess.CheckInt(out _))
                        {
                            string show = ToGuessInput(inputGuess, guessNumber, out isAnsIt);

                            Console.WriteLine(show);
                            if (isAnsIt)
                            {

                                if (DoReSrart())
                                {
                                    guessNumber = InitGuess();
                                    Console.WriteLine("已經重新開始");
                                }
                                else
                                {
                                    run = false;
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
        private static string InitGuess()
        {
            string guessbase = "0123456789";
            string guessString = "";
            StringBuilder baseBuilder = new StringBuilder(guessbase);
            for (int i = 0; i < 4; i++)
            {
                Random rnd = new Random();
                int randomIndex = rnd.Next(0, baseBuilder.Length);
                char basechar = baseBuilder[randomIndex];
                guessString += basechar;
                baseBuilder.Remove(randomIndex, 1);
            }
            return guessString;
        }
        #endregion

        #region 終極密碼
        private static (int ansNumber, int num_start, int num_end) InitNumber()
        {
            Random rad = new Random();
            int ansNumber = rad.Next(0, 101);
            int numStart = 0;
            int numEnd = 100;
            return (ansNumber, numStart, numEnd);
        }
        private static string ToGuessInput(string? inputGuess, string ans, out bool isAnsIt)
        {
            isAnsIt = false;
            int showA = 0;
            int showB = 0;
            for (int i = 0; i < ans.Length; i++)
            {
                if (ans.Contains(inputGuess![i]))
                {
                    showB++;
                    if (ans[i] == inputGuess[i])
                    {
                        showA++;
                        showB--;
                    }
                }
            }

            if (showA == 4)
            {
                isAnsIt = true;
            }
            return $"{showA}A{showB}B";
        }
        private static void GuessNumber()
        {
            Console.WriteLine("0~100猜數字");
            (int ansNumber, int numStart, int numEnd) = InitNumber();
            Console.WriteLine($"已經產生最終答案{ansNumber}");
            bool running = true;
            while (running)
            {
                string? guessNumber = Console.ReadLine();

                if (int.TryParse(guessNumber, out int guessInt))
                {
                    if (!(guessInt.CompareTo(numStart) > 0 && numEnd.CompareTo(guessInt) > 0))
                    {
                        Console.WriteLine($"請輸入範圍[{numStart}]到[{numEnd}]的數字");
                        continue;
                    }
                    int checkGuess = guessInt.CompareTo(ansNumber);
                    switch (checkGuess)
                    {
                        case -1:
                            numStart = guessInt;
                            Console.WriteLine($"{guessInt}比答案小[{numStart}]到[{numEnd}]");
                            break;
                        case 0:
                            Console.WriteLine($"猜中了答案是{guessNumber}");

                            if (DoReSrart())
                            {
                                (ansNumber, numStart, numEnd) = InitNumber();

                                Console.WriteLine($"已經產生最終答案");
                            }
                            else
                            {
                                running = false;
                            }
                            break;
                        case 1:
                            numEnd = guessInt;
                            Console.WriteLine($"{guessInt}比答案大[{numStart}]到[{numEnd}]");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("請輸入數字");
                }
            }

        }
        #endregion

        #region Common
        private static bool DoReSrart()
        {
            Console.WriteLine("是否重新開始（Y/N）");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'Y' || key.KeyChar == 'y')
            {
                Console.WriteLine();
                return true;
            }

            return false;
        }
        private static string[] InitGame()
        {
            var games = new[] { "1A1B", "猜數字" };
            return games;
        }
        #endregion

    }
    enum GameList
    {
        OneAOneB = 1,
        Guess = 2,
    }



    static class Extension
    {
        public static bool CheckInt(this string? inputString, out int stringToint)
        {
            stringToint = 0;
            bool check = int.TryParse(inputString, out stringToint);
            return check;
        }

    }
}