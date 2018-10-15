using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OcpSample
{
    public class FileLogger : ILogger
    {
        private readonly Dictionary<long, long> lockDictionary;
        private readonly string fileName;
        public string FileName => fileName;

        public FileLogger(string fileName)
        {
            this.fileName = fileName ?? throw new ArgumentNullException(nameof(fileName));
            this.lockDictionary = new Dictionary<long, long>();
        }

        public void Log(string message)
        {
            WriteLogToFile(message);
        }

        private void WriteLogToFile(string message)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 1024, true))
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                int length = data.Length;
                long loc = 0;
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        if (loc >= fs.Length)
                        {
                            fs.Lock(loc, length);
                            lockDictionary.Add(loc, length);
                            flag = false;
                        }
                        else
                        {
                            loc = fs.Length;
                        }
                    }
                    catch
                    {
                        while (!lockDictionary.ContainsKey(loc))
                        {
                            loc += lockDictionary[loc];
                        }
                    }
                }
                fs.Seek(loc, SeekOrigin.Begin);
                fs.Write(data, 0, length);
                fs.Close();
            }
        }
    }
}
