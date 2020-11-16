using Dawn;

namespace MartianRobots.Models
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = Guard.Argument(x).InRange(0, 50);
            Y = Guard.Argument(y).InRange(0, 50);
        }

        public override bool Equals(object obj)
        {
            var point = obj as Point;
            return X == point.X && Y == point.Y;
        }
    }
}
