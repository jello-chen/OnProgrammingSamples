using System.Collections.Generic;

namespace OcpSample
{
    public class GroupLogger: ILogger
    {
        private readonly List<ILogger> _loggers = new List<ILogger>();

        public GroupLogger(IEnumerable<ILogger> loggers)
        {
            _loggers.AddRange(loggers);
        }

        public GroupLogger(params ILogger[] loggers)
        {
            _loggers.AddRange(loggers);
        }

        public void Log(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(message);
            }
        }
    }
}
