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
        private static  IContainer? _container;

        // 在建構子中建立容器並註冊服務
        //test1 修改
        public static void Register()
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
        /// <summary>
        /// 取得遊戲內容
        /// </summary>
        /// <param name="GameType"></param>
        /// <returns></returns>
        public bool PlayGame(GameList GameType)
        {
            // 使用先前建立的容器
            using (var scope = _container?.BeginLifetimeScope())
            {
                var gameTasks = scope?.Resolve<IEnumerable<Meta<IGameTask>>>();
                var lazy = gameTasks
                    ?.FirstOrDefault(t => GameType.Equals(t.Metadata[nameof(GameAttribute.Type)]))
                    ?.Value;
                lazy?.Execute();

                return lazy.Regame;
            }
        }
    }
}
