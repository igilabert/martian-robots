using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("MartianRobots.Tests")]

namespace MartianRobots.Models
{
    public class RobotsContext : IRobotsContext
    {
        private const string Lost = "LOST";

        public IList<Point> Scents { get; private set; }
        public IRobot ActiveRobot { get; private set; }
        public ISurface Surface { get; private set; }

        internal readonly List<IRobot> _robots;

        public RobotsContext()
        {
            _robots = new List<IRobot>();
            Scents = new List<Point>();
        }

        public void SetSurface(ISurface surface)
        {
            Surface = surface;
        }

        public void AddRobot(IRobot robot)
        {
            _robots.Add(robot);
        }

        public void SetActiveRobot(IRobot robot)
        {
            ActiveRobot = robot;
        }

        public void UpdateScents(IList<Point> scents)
        {
            Scents = scents;
        }

        public string GetResults()
        {
            var stringBuilder = new StringBuilder();
            foreach (var robot in _robots)
            {
                stringBuilder.Append($"{robot.CurrentPosition.X} {robot.CurrentPosition.Y} {robot.CurrentOrientation} {(robot.IsLost ? Lost : string.Empty)}{Environment.NewLine}");
            }
            return stringBuilder.ToString();
        }
    }
}
