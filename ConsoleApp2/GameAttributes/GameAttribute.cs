using ConsoleApp2.enums;

namespace ConsoleApp2.GameAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class GameAttribute : Attribute
    {
        public GameList Type { get; }

        public GameAttribute(GameList type)
        {
            Type = type;
        }
    }
}
