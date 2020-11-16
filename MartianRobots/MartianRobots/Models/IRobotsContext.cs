using System.Collections.Generic;

namespace MartianRobots.Models
{
    public interface IRobotsContext
    {
        /// <summary>
        /// Gets the current surface
        /// </summary>
        ISurface Surface { get; }
        /// <summary>
        /// Gets the current robot to move
        /// </summary>
        IRobot ActiveRobot { get; }
        /// <summary>
        /// List of points where robots got lost
        /// </summary>
        IList<Point> Scents { get; }
        /// <summary>
        /// Sets the surface where the robots move
        /// </summary>
        /// <param name="surface">Target surface</param>
        void SetSurface(ISurface surface);
        /// <summary>
        /// Adds a robot to the list
        /// </summary>
        /// <param name="robot">Robot to add to the list</param>
        void AddRobot(IRobot robot);
        /// <summary>
        /// Sets the robot to move
        /// </summary>
        /// <param name="robot">Robot to set as active</param>
        void SetActiveRobot(IRobot robot);
        /// <summary>
        /// Update the list of points where robots got lost
        /// </summary>
        /// <param name="scents">New value for scents</param>
        void UpdateScents(IList<Point> scents);
        /// <summary>
        /// Formats the position of robots
        /// </summary>
        /// <returns>Formatted string with robots position</returns>
        string GetResults();
    }
}
