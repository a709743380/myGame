using Autofac;
using ConsoleApp2.GameAttributes;
using ConsoleApp2.IGame;

namespace ConsoleApp2.Modules
{
    public class GameModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //注冊所有游戲
            var attrType = typeof(GameAttribute);
            builder.RegisterAssemblyTypes(ThisAssembly)
                .AssignableTo<IGameTask>()
                .Where(type => type.IsDefined(attrType, true))
                .As<IGameTask>()
                .WithMetadataFrom<GameAttribute>()
                .InstancePerLifetimeScope();
        }
    }
}
