using System;
using System.IO;

namespace LogWriter
{
    public class LogFileWriter : ILogWriter
    {
        private string _fileName;
        private StreamWriter _sw;
        private StreamReader _sr;

        private DateTime DateTimestamp
        {
            get
            {
                return DateTime.Now;
            }
        }
        /// <summary>
        /// Creates an object that can be used to write timestamped log entries
        /// to be stored in a single file on the local machine
        /// </summary>
        /// <param name="filePath">Full absolute or relative path to the location of log file (file extension ".log" will be added automatically)</param>
        public LogFileWriter(string filePath)
        {
            if (filePath.Contains("\\") && !Directory.Exists(filePath.Substring(0, filePath.LastIndexOf("\\"))))
            {
                Directory.CreateDirectory(filePath.Substring(0, filePath.LastIndexOf("\\")));
            }
            _fileName = filePath + ".log";
        }
        
        public int NewLogEntry(string message)
        {
            using (_sw = new StreamWriter(_fileName, true))
            {
                _sw.WriteLine(DateTimestamp + " -- " + message);
            }
            return 0;
        }

        public string GetAllEntries()
        {
            string allEntries = "";
            using (_sr = new StreamReader(_fileName))
            {
                while (!_sr.EndOfStream)
                {
                    allEntries += _sr.ReadLine() + "\n";
                }
            }
            return allEntries;
        }

        public string SearchAllEntries(string searchWord)
        {
            string foundEntries = "";
            using (_sr = new StreamReader(_fileName))
            {
                while (!_sr.EndOfStream)
                {
                    string line = _sr.ReadLine();
                    if (line.Contains(searchWord))
                    {
                        foundEntries += line + "\n";
                    }
                }
            }
            return foundEntries;
        }
    }
}
