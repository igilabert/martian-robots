using MartianRobots.CommandHandlers;
using MartianRobots.Models;
using Moq;
using System;
using Xunit;

namespace MartianRobots.Tests.CommandHandlers
{
    public class SetStartPossitionCommandHandlerTests
    {
        private readonly Mock<IRobotsContext> _robotsContextMock = new Mock<IRobotsContext>();
        private const string ValidCommand = "1 1 N";
        private const string InvalidCommand = "1 1";

        [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new SetStartPossitionCommandHandler());
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnFalse_WhenCommandIsInvalid()
        {
            ICommandHandler commandHandler = new SetStartPossitionCommandHandler();

            Assert.False(commandHandler.MatchesCommandPattern(InvalidCommand));
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnTrue_WhenCommandIsValid()
        {
            ICommandHandler commandHandler = new SetStartPossitionCommandHandler();

            Assert.True(commandHandler.MatchesCommandPattern(ValidCommand));
        }

        [Fact]
        public void HandleCommand_ThrowsInvalidOperationException_WhenRobotStartPositionIsNotInSurface()
        {
            _robotsContextMock.Setup(x => x.Surface.IsPointInSurface(It.IsAny<Point>())).Returns(false);
            var commandHandler = new SetStartPossitionCommandHandler();

            Assert.Throws<InvalidOperationException>(() => commandHandler.HandleCommand(_robotsContextMock.Object, ValidCommand));
        }

        [Fact]
        public void HandleCommand_ShouldAddRobot_WhenRobotStartPositionIsInSurface()
        {
            _robotsContextMock.Setup(x => x.Surface.IsPointInSurface(It.IsAny<Point>())).Returns(true);
            var commandHandler = new SetStartPossitionCommandHandler();

            commandHandler.HandleCommand(_robotsContextMock.Object, ValidCommand);

            _robotsContextMock.Verify(x => x.AddRobot(It.IsAny<IRobot>()));
        }

        [Fact]
        public void HandleCommand_ShouldSetActiveRobot_WhenRobotStartPositionIsInSurface()
        {
            _robotsContextMock.Setup(x => x.Surface.IsPointInSurface(It.IsAny<Point>())).Returns(true);
            var commandHandler = new SetStartPossitionCommandHandler();

            commandHandler.HandleCommand(_robotsContextMock.Object, ValidCommand);

            _robotsContextMock.Verify(x => x.SetActiveRobot(It.IsAny<IRobot>()));
        }
    }
}
