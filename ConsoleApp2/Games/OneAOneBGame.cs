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
        public void Play1A1B()
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

                                if (_commonGear.DoReSrart())
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
        public string InitGuess()
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
        private string ToGuessInput(string? inputGuess, string ans, out bool isAnsIt)
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

        public void Execute()
        {
            Play1A1B();
        }
    }
}
