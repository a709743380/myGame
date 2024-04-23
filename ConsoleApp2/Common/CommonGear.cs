
namespace ConsoleApp2.Common
{
    public class CommonGear
    {
        public (bool restart, bool regame) DoReSrart()
        {
            Console.Write("是否重新開始（Y/N）");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == 'Y' || key.KeyChar == 'y')
            {
                Console.WriteLine();
                return (true, false);
            }
            else
            {
                Console.WriteLine();
                Console.Write("是否重新切換遊戲？（Y/N）");
                key = Console.ReadKey();
                if (key.KeyChar == 'Y' || key.KeyChar == 'y')
                {
                    Console.WriteLine();
                    return (false, true);
                }
            }
            return (false, false);
        }
    }
}
