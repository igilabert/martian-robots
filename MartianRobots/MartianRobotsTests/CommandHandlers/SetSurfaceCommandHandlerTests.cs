using MartianRobots.CommandHandlers;
using MartianRobots.Models;
using Moq;
using Xunit;

namespace MartianRobots.Tests.CommandHandlers
{
    public class SetSurfaceCommandHandlerTests
    {
        private const string ValidCommand = "1 1";
        private const string InvalidCommand = "1 1 a";

        [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new SetSurfaceCommandHandler());
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnFalse_WhenCommandIsInvalid()
        {
            ICommandHandler commandHandler = new SetSurfaceCommandHandler();

            Assert.False(commandHandler.MatchesCommandPattern(InvalidCommand));
        }

        [Fact]
        public void MatchesCommandPattern_ShouldReturnTrue_WhenCommandIsValid()
        {
            ICommandHandler commandHandler = new SetSurfaceCommandHandler();

            Assert.True(commandHandler.MatchesCommandPattern(ValidCommand));
        }

        [Fact]
        public void HandleCommand_ShouldSetSurface()
        {
            var robotsContextMock = new Mock<IRobotsContext>();
            var commandHandler = new SetSurfaceCommandHandler();

            commandHandler.HandleCommand(robotsContextMock.Object, "5 5");

            robotsContextMock.Verify(x => x.SetSurface(It.IsAny<ISurface>()));
        }
    }
}
