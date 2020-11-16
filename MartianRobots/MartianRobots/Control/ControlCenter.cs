using Dawn;
using MartianRobots.CommandHandlers;
using MartianRobots.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Control
{
    public class ControlCenter : IControlCenter
    {
        private readonly IRobotsContext _robotsContext;
        private readonly IEnumerable<ICommandHandler> _commandHandlers;

        public ControlCenter(IRobotsContext robotsContext, IEnumerable<ICommandHandler> commandHandlers)
        {
            _robotsContext = robotsContext;
            _commandHandlers = commandHandlers;
        }
        public string GetResulsts() => _robotsContext.GetResults();

        public void SendCommand(string command)
        {
            Guard.Argument(command).MaxLength(100);

            var commandHandler = _commandHandlers?.FirstOrDefault(handler => handler.MatchesCommandPattern(command)) ?? throw new InvalidOperationException();

            commandHandler.HandleCommand(_robotsContext, command);
        }
    }
}
