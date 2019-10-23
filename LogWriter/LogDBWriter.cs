using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace LogWriter
{
    public class LogDBWriter : ILogWriter
    {
        private string _connectionString;
        private string _tableName;
        private DateTime DateTimestamp
        {
            get
            {
                return DateTime.Now;
            }
        }

        public LogDBWriter(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sqlInit = $"IF EXISTS({_tableName} )";
                SqlCommand cmd = new SqlCommand(sqlInit, conn);
                cmd.ExecuteNonQuery();
                
            }
        }

        public string GetAllEntries()
        {
            string result = "";
            string sqlSelect = "SELECT timestamp, title, message " +
                                $"FROM {_tableName};";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlSelect, conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result += reader["timestamp"] + " ";
                    result += reader["title"] + " : ";
                    result += reader["message"] + "\n";
                }
            }
            return result;
        }

        public int NewLogEntry(string message)
        {
            string sqlInsert = $"INSERT INTO {_tableName} (timestamp, title, message) " +
                                "VALUES (@timestamp, @title, @message); " +
                                "SELECT CAST(SCOPE_IDENTITY AS int);";
            int index = -1;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);
                cmd.Parameters.AddWithValue("@timestamp", DateTimestamp);
                cmd.Parameters.AddWithValue("@title", message.Substring(0, message.IndexOf(" ")));
                cmd.Parameters.AddWithValue("@message", message.Substring(message.IndexOf(" ") + 2));
                index = (int)cmd.ExecuteScalar();
            }
            return index;
        }

        public string SearchAllEntries(string searchWord)
        {
            string [] entries = GetAllEntries().Split("\n");
            string matchedEntries = "";

            foreach(string line in entries)
            {
                if (line.Contains(searchWord))
                {
                    matchedEntries += line + "\n";
                }
            }
            return matchedEntries;
        }
    }
}
