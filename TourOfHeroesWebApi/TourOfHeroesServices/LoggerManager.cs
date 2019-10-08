namespace TourOfHeroesServices
{
    using NLog;
    using Contracts;

    public class LoggerManager : ILoggerManager
    {
        // ReSharper disable once InconsistentNaming
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public LoggerManager()
        {
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}