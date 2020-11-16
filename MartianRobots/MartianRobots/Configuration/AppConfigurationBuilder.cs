using MartianRobots.CommandHandlers;
using MartianRobots.Control;
using MartianRobots.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MartianRobots.Configuration
{
    public class AppConfigurationBuilder
    {
        private readonly IServiceCollection _services;

        public AppConfigurationBuilder()
        {
            _services = new ServiceCollection();
        }

        public IServiceProvider Build()
        {
            _services.AddSingleton<IControlCenter, ControlCenter>();
            _services.AddSingleton<IRobotsContext, RobotsContext>();
            _services.AddSingleton<ICommandHandler, SetSurfaceCommandHandler>();
            _services.AddSingleton<ICommandHandler, SetStartPossitionCommandHandler>();
            _services.AddSingleton<ICommandHandler, MoveRobotCommandHandler>();

            return _services.BuildServiceProvider();
        }
    }
}
