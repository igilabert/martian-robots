using MartianRobots.Models;
using Xunit;

namespace MartianRobots.Tests.Models
{
    public class SurfaceTests
    {
        [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new Surface(5, 5));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 3)]
        [InlineData(5, 5)]
        public void IsPointInSurface_ShouldReturnTrue_WhenPointIsInsideBounds(int x, int y)
        {
            var surface = new Surface(5, 5);
            Assert.True(surface.IsPointInSurface(new Point(x, y)));
        }

        [Fact]
        public void IsPointInSurface_ShouldReturnFalse_WhenPointIsOutsideBounds()
        {
            var surface = new Surface(5, 5);
            Assert.False(surface.IsPointInSurface(new Point(5, 6)));
        }
    }
}
