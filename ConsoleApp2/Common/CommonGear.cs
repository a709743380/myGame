
namespace ConsoleApp2.Common
{
    public class CommonGear
    {
        public bool DoReSrart()
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
        public string[] InitGame()
        {
            var games = new[] { "1A1B", "猜數字" };
            return games;
        }
    }
}
