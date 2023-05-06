namespace WebApi.Services
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(string message) : base(message, loggerName: "ConsoleLogger")
        {

        }
    }
}
