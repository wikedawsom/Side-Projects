using System;
using System.Collections.Generic;
using System.Text;

namespace LogWriter
{
    public interface ILogWriter
    {
        string GetAllEntries();

        string SearchAllEntries(string searchWord);

        int NewLogEntry(string message);
    }
}
