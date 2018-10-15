using System;

namespace OcpSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console Logger
            ILogger consoleLogger = new ConsoleLogger();
            consoleLogger.Log("Hello");

            // File Logger
            ILogger fileLogger = new FileLogger(DateTime.Now.ToString("yyyyMMdd") + ".log");
            fileLogger.Log("Hello" + Environment.NewLine);

            Console.ReadKey();
        }
    }
}
