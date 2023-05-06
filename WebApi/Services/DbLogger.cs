namespace WebApi.Services
{
    public class DbLogger : Logger
    {
        public DbLogger(string message) : base(message, loggerName: "DbLogger")
        {

        }
    }
}
