using MartianRobots.CommandHandlers;
using MartianRobots.Enums;
using MartianRobots.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace MartianRobots.Tests.CommandHandlers
{
    public class MoveRobotCommandHandlerTests
    {
        private readonly Mock<IRobotsContext> _robotsContextMock = new Mock<IRobotsContext>();
        private readonly Mock<IRobot> _robotMock = new Mock<IRobot>();
        private const string ValidCommand = "LFRLLFLFRRF";
        private const string InvalidCommand = "MFLDR";

        [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new MoveRobotCommandHandler());
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnFalse_WhenCommandIsInvalid()
        {
            ICommandHandler commandHandler = new MoveRobotCommandHandler();

            Assert.False(commandHandler.MatchesCommandPattern(InvalidCommand));
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnTrue_WhenCommandIsValid()
        {
            ICommandHandler commandHandler = new MoveRobotCommandHandler();

            Assert.True(commandHandler.MatchesCommandPattern(ValidCommand));
        }

        [Fact]
        public void HandleCommand_ShouldCallMoveOnActiveRobot()
        {
            _robotsContextMock.Setup(x => x.ActiveRobot).Returns(_robotMock.Object);
            ICommandHandler commandHandler = new MoveRobotCommandHandler();

            commandHandler.HandleCommand(_robotsContextMock.Object, ValidCommand);

            _robotMock.Verify(x => x.Move(It.IsAny<IList<Movement>>(), It.IsAny<IList<Point>>()));
        }

        [Fact]
        public void HandleCommand_ShouldCallUpdateScents()
        {
            _robotsContextMock.Setup(x => x.ActiveRobot).Returns(_robotMock.Object);
            ICommandHandler commandHandler = new MoveRobotCommandHandler();

            commandHandler.HandleCommand(_robotsContextMock.Object, ValidCommand);

            _robotsContextMock.Verify(x => x.UpdateScents(It.IsAny<IList<Point>>()));
        }
    }
}
