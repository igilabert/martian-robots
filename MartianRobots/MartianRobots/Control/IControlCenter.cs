namespace MartianRobots.Control
{
    public interface IControlCenter
    {
        /// <summary>
        /// Sends the command to the handler which can handle  it
        /// </summary>
        /// <param name="command">Command to execute</param>
        void SendCommand(string command);

        /// <summary>
        /// Gets the position of all robots
        /// </summary>
        /// <returns>Formatted string with robots position</returns>
        string GetResulsts();
    }
}
