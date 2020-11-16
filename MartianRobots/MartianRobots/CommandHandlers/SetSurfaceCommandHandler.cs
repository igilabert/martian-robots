using MartianRobots.Models;
using System.Text.RegularExpressions;

namespace MartianRobots.CommandHandlers
{
    public class SetSurfaceCommandHandler : ICommandHandler
    {
        public Regex CommandRegexPattern => new Regex(@"^\d+ \d+$");

        public void HandleCommand(IRobotsContext robotsContext, string command)
        {
            var surface = GetSurface(command);
            robotsContext.SetSurface(surface);
        }

        private Surface GetSurface(string command)
        {
            string[] splitCommand = command.Split(" ");
            return new Surface(int.Parse(splitCommand[0]), int.Parse(splitCommand[1]));
        }
    }
}
