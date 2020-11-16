using MartianRobots.Enums;
using MartianRobots.Models;
using System;

namespace MartianRobots.Services
{
    public class MoveService : IMoveService
    {
        public Point MoveForwardFrom(Point point, Orientation orientation)
        {
            return orientation switch
            {
                Orientation.N => new Point(point.X, point.Y + 1),
                Orientation.E => new Point(point.X + 1, point.Y),
                Orientation.S => new Point(point.X, point.Y - 1),
                Orientation.W => new Point(point.X - 1, point.Y),
                _ => throw new InvalidOperationException()
            };
        }

    }
}
