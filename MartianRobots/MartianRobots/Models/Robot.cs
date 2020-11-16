using MartianRobots.Enums;
using MartianRobots.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MartianRobots.Tests")]

namespace MartianRobots.Models
{
    public class Robot : IRobot
    {
        public Point CurrentPosition { get; set; }
        public ISurface Surface { get; set; }
        public Orientation CurrentOrientation { get; set; }
        public bool IsLost { get; set; }

        private readonly IReadOnlyDictionary<Movement, Action> _movementActionMappings;
        private readonly IMoveService _moveService;
        private readonly ITurnService _turnService;
        private IList<Point> _scents;

        public Robot(Point startPosition, Orientation startOrientation)
        {
            CurrentPosition = startPosition;
            CurrentOrientation = startOrientation;
            IsLost = false;
            _movementActionMappings = new Dictionary<Movement, Action>()
            {
                { Movement.L, TurnLeft},
                { Movement.R, TurnRight},
                { Movement.F, MoveForward }
            };
            _moveService = new MoveService();
            _turnService = new TurnService();
        }

        /// <summary>
        /// Internal constructor for testing purposes
        /// </summary>        
        internal Robot(Point startPosition, Orientation startOrientation, ISurface surface, IMoveService moveService, ITurnService turnService)
        {
            CurrentPosition = startPosition;
            CurrentOrientation = startOrientation;
            IsLost = false;
            Surface = surface;

            _movementActionMappings = new Dictionary<Movement, Action>()
            {
                { Movement.L, TurnLeft},
                { Movement.R, TurnRight},
                { Movement.F, MoveForward }
            };
            _moveService = moveService;
            _turnService = turnService;
        }

        public IList<Point> Move(IList<Movement> movements, IList<Point> scents)
        {
            _scents = scents;

            foreach (var movement in movements)
            {
                _movementActionMappings[movement].Invoke();
            }

            return _scents;
        }

        private void TurnLeft()
        {
            CurrentOrientation = _turnService.TurnLeftFrom(CurrentOrientation);
        }

        private void TurnRight()
        {
            CurrentOrientation = _turnService.TurnRightFrom(CurrentOrientation);
        }

        private void MoveForward()
        {
            var nextPoint = _moveService.MoveForwardFrom(CurrentPosition, CurrentOrientation);

            if (!Surface.IsPointInSurface(nextPoint))
            {
                if (!_scents.Any(point => point.Equals(CurrentPosition)))
                {
                    _scents.Add(CurrentPosition);
                    IsLost = true;
                    CurrentPosition = nextPoint;
                }
            }
            else
            {
                CurrentPosition = nextPoint;
            }
        }
    }
}
