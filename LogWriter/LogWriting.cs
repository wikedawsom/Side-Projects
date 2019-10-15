using System;
using System.IO;

namespace LogWriting
{
    public class LogWriter
    {
        private string _fileName;
        private StreamWriter _sw;

        private DateTime Timestamp
        {
            get
            {
                return DateTime.Now;
            }
        }

        public LogWriter(string filePath)
        {
            if (filePath.Contains("\\") && !Directory.Exists(filePath.Substring(0, filePath.LastIndexOf("\\"))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf("\\")));
            }
            _fileName = filePath + ".log";
        }
        
        public void NewLogEntry(string message)
        {
            using (_sw = new StreamWriter(_fileName, true))
            {
                _sw.WriteLine(Timestamp + " -- " + message);
            }
        }
    }
}
