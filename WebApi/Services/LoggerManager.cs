using System;
using System.Diagnostics;

namespace WebApi.Services
{
    public class LoggerManager : ILoggerService
    {
       public void Write(Logger logger)
        {
            Debug.WriteLine($"{logger.LoggerName} - {logger.Message}");
        }
    }
}
