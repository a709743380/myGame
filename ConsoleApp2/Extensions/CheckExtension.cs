
namespace ConsoleApp2.Extensions
{
    public static class CheckExtension
    {
        public static bool CheckInt(this string? inputString, out int stringToint)
        {
            stringToint = 0;
            bool check = int.TryParse(inputString, out stringToint);
            return check;
        }

    }
}
