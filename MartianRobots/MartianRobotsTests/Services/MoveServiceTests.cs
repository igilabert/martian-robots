using MartianRobots.Enums;
using MartianRobots.Models;
using MartianRobots.Services;
using System;
using Xunit;

namespace MartianRobots.Tests.Services
{
    public class MoveServiceTests
    {
        private readonly IMoveService _moveService = new MoveService();

        [Theory]
        [InlineData(1, 1, Orientation.N, 1, 2)]
        [InlineData(1, 1, Orientation.E, 2, 1)]
        [InlineData(1, 1, Orientation.S, 1, 0)]
        [InlineData(1, 1, Orientation.W, 0, 1)]
        public void MoveForwardFrom_ShouldReturnNextPoint_AccordingToStartPositionAndStartOrientation(
            int x, int y, Orientation initialOrientation,
            int expectedX,
            int expectedY
        )
        {
            var initialPoint = new Point(x, y);

            var nextPoint = _moveService.MoveForwardFrom(initialPoint, initialOrientation);

            Assert.Equal(expectedX, nextPoint.X);
            Assert.Equal(expectedY, nextPoint.Y);
        }

        [Fact]
        public void MoveForwardFrom_ShouldThrowInvalidOperationException_WhenOrientationIsInvalid()
        {
            var position = new Point(1, 1);

            Assert.Throws<InvalidOperationException>(() => _moveService.MoveForwardFrom(position, (Orientation)(-10)));
        }
    }
}
