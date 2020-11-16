namespace MartianRobots.Models
{
    public interface ISurface
    {
        /// <summary>
        /// Checks if the given point is contained in the surface.
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <returns>Whether the point is in the surface or not</returns>
        bool IsPointInSurface(Point point);
    }
}