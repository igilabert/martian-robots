using MartianRobots.Enums;
using MartianRobots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MartianRobots.CommandHandlers
{
    public class MoveRobotCommandHandler : ICommandHandler
    {
        public Regex CommandRegexPattern => new Regex(@"^[LRF]+$");

        public void HandleCommand(IRobotsContext robotsContext, string command)
        {
            var movementSequence = GetMovementSequence(command);
            robotsContext.UpdateScents(robotsContext.ActiveRobot.Move(movementSequence, robotsContext.Scents));
        }

        private IList<Movement> GetMovementSequence(string command)
        {
            char[] commandSplit = command.ToCharArray();
            return commandSplit.Select(arg => Enum.Parse<Movement>(new string(arg, 1))).ToList();
        }
    }
}
