using MartianRobots.Enums;
using System;

namespace MartianRobots.Services
{
    public class TurnService : ITurnService
    {
        public Orientation TurnLeftFrom(Orientation orientation)
        {
            return orientation switch
            {
                Orientation.N => Orientation.W,
                Orientation.S => Orientation.E,
                Orientation.E => Orientation.N,
                Orientation.W => Orientation.S,
                _ => throw new InvalidOperationException()
            };
        }

        public Orientation TurnRightFrom(Orientation orientation)
        {
            return orientation switch
            {
                Orientation.N => Orientation.E,
                Orientation.S => Orientation.W,
                Orientation.E => Orientation.S,
                Orientation.W => Orientation.N,
                _ => throw new InvalidOperationException()
            };
        }
    }
}
