using MartianRobots.Enums;
using MartianRobots.Services;
using System;
using Xunit;

namespace MartianRobots.Tests.Services
{
    public class TurnServiceTests
    {
        private readonly ITurnService _turnService = new TurnService();

        [Fact]
        public void TurnLeftFrom_ShouldThrowException_WhenFromOrientationIsInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => _turnService.TurnLeftFrom((Orientation)(-10)));
        }

        [Fact]
        public void TurnRightFrom_ShouldThrowException_WhenFromOrientationIsInvalid()
        {
            Assert.Throws<InvalidOperationException>(() => _turnService.TurnLeftFrom((Orientation)(-10)));
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.W)]
        [InlineData(Orientation.E, Orientation.N)]
        [InlineData(Orientation.S, Orientation.E)]
        [InlineData(Orientation.W, Orientation.S)]
        public void TurnLeftFrom_ShouldReturnExpectedDirection_WhenFromOrientationIsValid(Orientation fromOrientation, Orientation expectedOrientation)
        {
            var actualOrientation = _turnService.TurnLeftFrom(fromOrientation);

            Assert.Equal(expectedOrientation, actualOrientation);
        }

        [Theory]
        [InlineData(Orientation.N, Orientation.E)]
        [InlineData(Orientation.E, Orientation.S)]
        [InlineData(Orientation.S, Orientation.W)]
        [InlineData(Orientation.W, Orientation.N)]
        public void TurnRightFrom_ShouldReturnExpectedDirection_WhenFromOrientationIsValid(Orientation fromOrientation, Orientation expectedOrientation)
        {
            var actualOrientation = _turnService.TurnRightFrom(fromOrientation);

            Assert.Equal(expectedOrientation, actualOrientation);
        }
    }
}
