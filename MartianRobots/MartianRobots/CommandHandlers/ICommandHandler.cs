using MartianRobots.Models;
using System.Text.RegularExpressions;

namespace MartianRobots.CommandHandlers
{
    public interface ICommandHandler
    {
        /// <summary>
        /// Regular expression with the pattern of the command
        /// </summary>
        Regex CommandRegexPattern { get; }

        /// <summary>
        /// Returns wheter the command matches the pattern or not.
        /// </summary>
        /// <param name="command">Command to handle</param>
        /// <returns>True if the handler can handle the command</returns>
        bool MatchesCommandPattern(string command) => CommandRegexPattern.IsMatch(command);

        /// <summary>
        /// Handles the command
        /// </summary>
        /// <param name="robotsContext">Robots context needed to keep trace of every robot</param>
        /// <param name="command">Command to handle</param>
        void HandleCommand(IRobotsContext robotsContext, string command);
    }
}
