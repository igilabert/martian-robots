using MartianRobots.Enums;
using MartianRobots.Models;
using System;
using System.Text.RegularExpressions;

namespace MartianRobots.CommandHandlers
{
    public class SetStartPossitionCommandHandler : ICommandHandler
    {
        public Regex CommandRegexPattern => new Regex(@"^\d+ \d+ [NSEW]$");

        public void HandleCommand(IRobotsContext robotsContext, string command)
        {
            (var startPosition, var startOrientation) = GetStartPosition(command);

            var robot = robotsContext.Surface.IsPointInSurface(startPosition) ? new Robot(startPosition, startOrientation) { Surface = robotsContext.Surface } : throw new InvalidOperationException();

            robotsContext.AddRobot(robot);
            robotsContext.SetActiveRobot(robot);
        }

        private (Point, Orientation) GetStartPosition(string command)
        {
            string[] splitCommands = command.Split(' ');
            var startPosition = new Point(int.Parse(splitCommands[0]), int.Parse(splitCommands[1]));
            var startOrientation = Enum.Parse<Orientation>(new string(splitCommands[2][0], 1));

            return (startPosition, startOrientation);
        }
    }
}
