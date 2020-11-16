using MartianRobots.Models;
using MartianRobots.Enums;
using Xunit;
using Moq;
using MartianRobots.Services;
using System.Collections.Generic;
using System.Linq;

namespace MartianRobots.Tests.Models
{
    public class RobotTests
    {
        private readonly Mock<ISurface> _surfaceMock = new Mock<ISurface>();
        private readonly Mock<ITurnService> _turnServiceMock = new Mock<ITurnService>();
        private readonly Mock<IMoveService> _moveServiceMock = new Mock<IMoveService>();

        private readonly IList<Point> _scents = new List<Point>();
        private readonly Point StartPoint = new Point(1, 1);
        private readonly Orientation StartOrientation = Orientation.N;
        private readonly IList<Movement> _moveForward = new List<Movement> { Movement.F };

    [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new Robot(StartPoint, StartOrientation));
        }

        [Fact]
        public void Robot_SetCurrentPosition_WhenCreated()
        {
            var robot = new Robot(StartPoint, StartOrientation);

            Assert.True(StartPoint.Equals(robot.CurrentPosition));
        }

        [Fact]
        public void Robot_SetCurrentOrientation_WhenCreated()
        {
            var robot = new Robot(StartPoint, StartOrientation);

            Assert.Equal(StartOrientation, robot.CurrentOrientation);
        }

        [Fact]
        public void IsLost_ShouldBeDefaultedAsFalse_WhenRobotIsCreated()
        {
            var robot = new Robot(StartPoint, StartOrientation);

            Assert.False(robot.IsLost);
        }

        [Fact]
        public void Move_ShouldUpdateCurrentOrientation_WhenCommandIsMoveTurnLeft()
        {
            var expectedOrientation = Orientation.S;
            _turnServiceMock.Setup(x => x.TurnLeftFrom(It.IsAny<Orientation>())).Returns(expectedOrientation);
            Robot robot = CreateRobotForTest();

            _ = robot.Move(new List<Movement> { Movement.L }, _scents);

            Assert.Equal(expectedOrientation, robot.CurrentOrientation);
        }

        [Fact]
        public void Move_ShouldUpdateCurrentOrientation_WhenCommandIsMoveTurnRight()
        {
            var expectedOrientation = Orientation.E;
            _turnServiceMock.Setup(x => x.TurnRightFrom(It.IsAny<Orientation>())).Returns(expectedOrientation);
            var robot = CreateRobotForTest();

            _ = robot.Move(new List<Movement> { Movement.R }, _scents);

            Assert.Equal(expectedOrientation, robot.CurrentOrientation);
        }

        [Fact]
        public void Move_ShouldUpdateCurrentPosition_WhenCommandIsMoveForward()
        {
            var finalPosition = new Point(3, 3);
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(true);
            _moveServiceMock.Setup(x => x.MoveForwardFrom(It.IsAny<Point>(), It.IsAny<Orientation>())).Returns(finalPosition);
            var robot = CreateRobotForTest();

            _ = robot.Move(_moveForward, _scents);

            Assert.True(finalPosition.Equals(robot.CurrentPosition));
        }

        [Fact]
        public void Move_ShouldReturnSameScents_IfRobotDoesNotGetLost()
        {
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(true);
            var robot = CreateRobotForTest();

            var scents = robot.Move(_moveForward, _scents);

            Assert.False(scents.Any());
        }

        [Fact]
        public void Move_ShouldSetIsLostToTrue_WhenRobotGetLost()
        {
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(false);
            _moveServiceMock.Setup(x => x.MoveForwardFrom(It.IsAny<Point>(), It.IsAny<Orientation>())).Returns(new Point(1, 2));
            var robot = CreateRobotForTest();

            var scents = robot.Move(_moveForward, _scents);

            Assert.True(robot.IsLost);
        }

        [Fact]
        public void Move_ShouldUpdateScentsWithCurrentPosition_WhenRobotGetLost()
        {
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(false);
            _moveServiceMock.Setup(x => x.MoveForwardFrom(It.IsAny<Point>(), It.IsAny<Orientation>())).Returns(new Point(1, 2));

            var robot = CreateRobotForTest();

            var scents = robot.Move(_moveForward, _scents);

            Assert.True(robot.IsLost);
            Assert.True(scents.Any());
            Assert.True(StartPoint.Equals(scents.First()));
        }

        [Fact]
        public void Move_ShouldUpdateCurrentPosition_WhenRobotGetLost()
        {
            var expectedPosition = new Point(1, 2);
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(false);
            _moveServiceMock.Setup(x => x.MoveForwardFrom(It.IsAny<Point>(), It.IsAny<Orientation>())).Returns(expectedPosition);

            var robot = CreateRobotForTest();

            _ = robot.Move(_moveForward, _scents);

            Assert.True(robot.IsLost);
            Assert.True(robot.CurrentPosition.Equals(expectedPosition));
        }

        [Fact]
        public void Move_ShouldNotExecuteMoveForwardCommand_WhenThereIsAScentInCurrentPositionAndRobotWillGetLost()
        {
            _scents.Add(StartPoint);
            _surfaceMock.Setup(x => x.IsPointInSurface(It.IsAny<Point>())).Returns(false);
            var robot = CreateRobotForTest();

            var scents = robot.Move(_moveForward, _scents);

            Assert.False(robot.IsLost);
            Assert.True(robot.CurrentPosition.Equals(StartPoint));
        }

        private Robot CreateRobotForTest() => new Robot(StartPoint, StartOrientation, _surfaceMock.Object, _moveServiceMock.Object, _turnServiceMock.Object);
    }
}
