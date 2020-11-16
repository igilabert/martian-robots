using MartianRobots.Enums;
using System.Collections.Generic;

namespace MartianRobots.Models
{
    public interface IRobot
    {
        /// <summary>
        /// Robot's current orientation
        /// </summary>
        Orientation CurrentOrientation { get; set; }
        /// <summary>
        /// Robot's current position
        /// </summary>
        Point CurrentPosition { get; set; }
        /// <summary>
        /// Returns true when the robot gets lost
        /// </summary>
        bool IsLost { get; set; }
        /// <summary>
        /// Surface where the robot is moving
        /// </summary>
        ISurface Surface { get; set; }
        /// <summary>
        /// Moves the robot through the surface
        /// </summary>
        /// <param name="movements">The list of movements to execute</param>
        /// <param name="scents">The list of points where previous robots got lost</param>
        /// <returns></returns>
        IList<Point> Move(IList<Movement> movements, IList<Point> scents);
    }
}