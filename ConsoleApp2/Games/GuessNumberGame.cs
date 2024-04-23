using ConsoleApp2.Common;
using ConsoleApp2.enums;
using ConsoleApp2.GameAttributes;
using ConsoleApp2.IGame;

namespace ConsoleApp2.Games
{
    [Game(GameList.GuessNumber)]
    public class GuessNumberGame : IGameTask
    {
        private CommonGear _commonGear = new();

        public bool Regame { get; set; } = false;

        private (int ansNumber, int num_start, int num_end) InitNumber()
        {
            Random rad = new();
            int ansNumber = rad.Next(0, 101);
            int numStart = 0;
            int numEnd = 100;
            return (ansNumber, numStart, numEnd);
        }

        private void GuessNumber()
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

                            var result = _commonGear.DoReSrart();
                            if (result.restart)
                            {
                                (ansNumber, numStart, numEnd) = InitNumber();

                                Console.WriteLine($"已經產生最終答案");
                            }
                            else
                            {
                                running = false;
                                Regame = result.regame;
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

        public void Execute()
        {
            GuessNumber();
        }
    }
}
