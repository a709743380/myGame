using Autofac.Features.Metadata;
using ConsoleApp2.IGame;
using ConsoleApp2.enums;
using Autofac;
using ConsoleApp2.GameAttributes;
using ConsoleApp2.Modules;
using System;

namespace ConsoleApp2.Services
{
    public class GameAutoFac
    {
        private static IContainer _container;

        // 在建構子中建立容器並註冊服務
        public  static void Register()
        {
            // 1. 建立容器建構器
            var builder = new ContainerBuilder();
            // 2. 註冊服務
            builder.RegisterModule<GameModule>();
            // 建立容器
            _container = builder.Build();
            // 取得已註冊的服務
            var registrations = _container.ComponentRegistry.Registrations;
        }

        public bool PlayGame(GameList GameType)
        {
            // 使用先前建立的容器
            using (var scope = _container.BeginLifetimeScope())
            {
                var gameTasks = scope.Resolve<IEnumerable<Meta<IGameTask>>>();
                var lazy = gameTasks
                    ?.FirstOrDefault(t => GameType.Equals(t.Metadata[nameof(GameAttribute.Type)]))
                    ?.Value;
                lazy?.Execute();

                return lazy.Regame;
            }
        }

        //這樣每次都要new一個太奇怪了
        //public void PlayGame(GameList GameType)
        //{
        //    // 1. 建立容器建構器
        //    var builder = new ContainerBuilder();
        //    // 2. 註冊服務
        //    builder.RegisterModule<GameModule>();
        //    // 建立容器
        //    IContainer container = builder.Build();
        //    // 4. 解析依賴項
        //    using (var scope = container.BeginLifetimeScope())
        //    {
        //        ////檢查是否注冊成功
        //        //bool isRegistered = container.IsRegistered<IGameTask>(); 
        //        var gameTasks = scope.Resolve<IEnumerable<Meta<IGameTask>>>();
        //        //取得實際游戲
        //        var lazy = gameTasks
        //            ?.FirstOrDefault(t => GameType.Equals(t.Metadata[nameof(GameAttribute.Type)]))
        //            ?.Value;
        //        lazy.Execute();
        //    }
        //}
    }
}
