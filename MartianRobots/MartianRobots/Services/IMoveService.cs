using MartianRobots.Enums;
using MartianRobots.Models;

namespace MartianRobots.Services
{
    public interface IMoveService
    {
        /// <summary>
        /// Gets the next point after moving forward from a given point and orientation
        /// </summary>
        /// <param name="point">Current position</param>
        /// <param name="orientation">Current orientation</param>
        /// <returns>Next point after moving forward from a given point and orientation</returns>
        Point MoveForwardFrom(Point point, Orientation orientation);
    }
}
