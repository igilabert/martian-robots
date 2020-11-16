using MartianRobots.CommandHandlers;
using MartianRobots.Control;
using MartianRobots.Models;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace MartianRobots.Tests.Control
{
    public class ControlCenterTests
    {
        private readonly Mock<ICommandHandler> _commandHandlerMock = new Mock<ICommandHandler>();
        private readonly Mock<IRobotsContext> _robotsContextMock = new Mock<IRobotsContext>();

        [Fact]
        public void SendCommand_ShouldThrowArgumentOutOfRangeException_WhenCommandLengthIsGreaterThan100()
        {            
            var controlCenter = CreateControlCenterForTests();            

            Assert.Throws<ArgumentException>(() => controlCenter.SendCommand(new string('a', 101)));
        }

        [Fact]
        public void SendCommand_ShouldThrowInvalidOperationException_WhenCommandCanNotBeHandled()
        {
            _commandHandlerMock.Setup(x => x.MatchesCommandPattern(It.IsAny<string>())).Returns(false);
            var controlCenter = CreateControlCenterForTests();

            Assert.Throws<InvalidOperationException>(() => controlCenter.SendCommand(string.Empty));
        }

        [Fact]
        public void SendCommand_MustCallHandleCommandOfSelectedCommandHandler_WhenCommandCanBeHandled()
        {
            _commandHandlerMock.Setup(x => x.MatchesCommandPattern(It.IsAny<string>())).Returns(true);
            var controlCenter = CreateControlCenterForTests();

            controlCenter.SendCommand(string.Empty);

            _commandHandlerMock.Verify(x => x.HandleCommand(It.IsAny<IRobotsContext>(), It.IsAny<string>()));
        }

        [Fact]
        public void GetResulsts_MustCallRobotsContextGetResults()
        {
            var controlCenter = CreateControlCenterForTests();

            controlCenter.GetResulsts();

            _robotsContextMock.Verify(x => x.GetResults());
        }

        private ControlCenter CreateControlCenterForTests() => new ControlCenter(_robotsContextMock.Object, new List<ICommandHandler> { _commandHandlerMock.Object });
    }
}
