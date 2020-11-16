namespace MartianRobots.Models
{
    public class Surface : ISurface
    {
        public Point UpperRight { get; private set; }

        public Surface(int x, int y)
        {
            UpperRight = new Point(x, y);
        }

        public bool IsPointInSurface(Point point)
        {
            var isXInSurface = point.X >= 0 && point.X <= UpperRight.X;
            var isYInSurface = point.Y >= 0 && point.Y <= UpperRight.Y;

            return isXInSurface && isYInSurface;
        }
    }
}
