
namespace ConsoleApp2.Extensions
{
    public static class CheckExtension
    {
        public static bool CheckInt(this string? inputString, out int stringToint)
        {
            stringToint = 0;
            bool check = int.TryParse(inputString, out stringToint);
            if (!check)
            {
                Console.WriteLine("請輸入數字:");
                string temp = Console.ReadLine();
                check = temp.CheckInt(out stringToint);
            }



            return check;
        }

    }
}
