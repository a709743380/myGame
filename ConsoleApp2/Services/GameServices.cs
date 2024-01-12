using Autofac.Features.Metadata;
using ConsoleApp2.IGame;
using ConsoleApp2.enums;
using Autofac;
using ConsoleApp2.GameAttributes;

namespace ConsoleApp2.Services
{
    public class GameServices
    {
        public void PlayGame(GameList GameType)
        {
            // 1. 建立容器建構器
            var builder = new ContainerBuilder();

            // 2. 註冊服務
            builder.RegisterModule<GameModule>();

            // 3. 建立容器
            var container = builder.Build();

            // 4. 解析依賴項
            using (var scope = container.BeginLifetimeScope())
            {
                ////檢查是否注冊成功
                //bool isRegistered = container.IsRegistered<IGameTask>(); 
                var gameTasks = scope.Resolve<IEnumerable<Meta<IGameTask>>>();
                //取得實際游戲
                var lazy = gameTasks
                    ?.FirstOrDefault(t => GameType.Equals(t.Metadata[nameof(GameAttribute.Type)]))
                    ?.Value;
                lazy.Execute();
            }
        }

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
}
