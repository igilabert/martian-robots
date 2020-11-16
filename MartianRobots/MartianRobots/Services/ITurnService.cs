using MartianRobots.Enums;

namespace MartianRobots.Services
{
    public interface ITurnService
    {
        Orientation TurnLeftFrom(Orientation orientation);

        Orientation TurnRightFrom(Orientation orientation);
    }
}
