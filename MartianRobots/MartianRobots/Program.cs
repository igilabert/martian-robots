using Dawn;
using MartianRobots.Configuration;
using MartianRobots.Control;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace MartianRobots
{
    class Program
    {
        private static IServiceProvider _serviceProvider;
        private static IControlCenter _controlCenter;

        static void Main(string[] args)
        {
            var commands = LoadCommands(args);

            ConfigureApp();
            InitializeControlCenter();

            foreach (var command in commands)
            {
                _controlCenter.SendCommand(command.ToUpper());
            }

            Console.Write(_controlCenter.GetResulsts());
        }

        private static string[] LoadCommands(string[] args)
        {
            Guard.Argument(args).NotNull().Count(1);
            return File.ReadAllLines(args[0]);
        }

        private static void ConfigureApp()
        {
            _serviceProvider = new AppConfigurationBuilder().Build();
        }

        private static void InitializeControlCenter() => _controlCenter = _serviceProvider.GetService<IControlCenter>();
    }
}
