using MartianRobots.Enums;
using MartianRobots.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MartianRobots.Tests.Models
{
    public class RobotsContextTests
    {
        [Fact]
        public void CanBeCreated()
        {
            Assert.NotNull(new RobotsContext());
        }

        [Fact]
        public void InitializesScentsToEmptyList()
        {
            var robotsContext = new RobotsContext();

            Assert.Empty(robotsContext.Scents);
        }

        [Fact]
        public void InitializesRobotsToEmptyList()
        {
            var robotsContext = new RobotsContext();

            Assert.Empty(robotsContext._robots);
        }

        [Fact]
        public void SetSurface_SetsSurfaceToProvidedOne()
        {
            var expectedSurface = new Surface(1, 1);
            var robotsContext = new RobotsContext();

            robotsContext.SetSurface(expectedSurface);

            Assert.Equal(expectedSurface, robotsContext.Surface);
        }

        [Fact]
        public void AddRobot_AddsARobotToTheList()
        {
            var robot = new Robot(new Point(1, 2), Orientation.E);
            var robotsContext = new RobotsContext();

            robotsContext.AddRobot(robot);

            Assert.NotEmpty(robotsContext._robots);
            Assert.Equal(robot, robotsContext._robots.First());
        }

        [Fact]
        public void SetActiveRobot_SetsActiveRobotToProvidedOne()
        {
            var robot = new Robot(new Point(1, 2), Orientation.E);
            var robotsContext = new RobotsContext();

            robotsContext.SetActiveRobot(robot);

            Assert.Equal(robot, robotsContext.ActiveRobot);
        }

        [Fact]
        public void UpdateScents_SetsScentsToProvidedOne()
        {
            var scents = new List<Point> { new Point(1, 1) };
            var robotsContext = new RobotsContext();

            robotsContext.UpdateScents(scents);

            Assert.Equal(scents.Count(), robotsContext.Scents.Count());
        }

        [Fact]
        public void GetResults_ReturnsCurrentStatusOfAllRobots()
        {
            var robot = new Robot(new Point(1, 1), Orientation.N);
            var robot2 = new Robot(new Point(2, 2), Orientation.E);

            var robotContext = new RobotsContext();
            robotContext.AddRobot(robot);
            robotContext.AddRobot(robot2);

            var results = robotContext.GetResults();

            Assert.Equal($"1 1 N {Environment.NewLine}2 2 E {Environment.NewLine}", results);
        }

        [Fact]
        public void GetResults_AppendsLost_WhenRobotIsLost()
        {
            var robot = new Robot(new Point(1, 1), Orientation.N);
            robot.IsLost = true;

            var robotContext = new RobotsContext();
            robotContext.AddRobot(robot);

            var results = robotContext.GetResults();

            Assert.Equal($"1 1 N LOST{Environment.NewLine}", results);
        }
    }
}
