using MartianRobots.Models;
using System;
using Xunit;

namespace MartianRobots.Tests.Models
{
    public class PointTests
    {
        [Theory]
        [InlineData(-1, -1)]
        [InlineData(-1, 0)]
        [InlineData(0, -1)]
        [InlineData(51, 0)]
        [InlineData(0, 51)]
        public void ShouldThrowArgumentOutOfRangeException_WhenAnyCoordinateIsOutOfBounds(int x, int y)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Point(x, y));
        }

        [Fact]
        public void CanBeCreated_WhenCoordinatesAreInBounds()
        {
            Assert.NotNull(new Point(1, 1));
        }

        [Fact]
        public void CoordinatesAreSet_WhenPointIsCreated()
        {
            var point = new Point(1, 2);
            
            Assert.Equal(1, point.X);
            Assert.Equal(2, point.Y);
        }

        [Fact]
        public void Equals_ShouldReturnsTrue_WhenTwoPointsHaveEqualCoordinates()
        {
            var pointA = new Point(1, 1);
            var pointB = new Point(1, 1);

            Assert.True(pointA.Equals(pointB));
        }

        [Theory]
        [InlineData(1, 2, 1, 1)]
        [InlineData(1, 2, 2, 2)]
        [InlineData(1, 2, 3, 4)]
        public void Equals_ShouldReturnFalse_WhenTwoPointsHaveDifferentCoordinates(int x1, int y1, int x2, int y2)
        {
            var pointA = new Point(x1, y1);
            var pointB = new Point(x2, y2);

            Assert.False(pointA.Equals(pointB));
        }
    }
}
