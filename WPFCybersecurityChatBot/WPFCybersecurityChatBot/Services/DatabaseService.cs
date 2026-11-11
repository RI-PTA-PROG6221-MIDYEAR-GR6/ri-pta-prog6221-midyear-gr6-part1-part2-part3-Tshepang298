using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using CyberSecurityChatbot.WPF.Models;

namespace CyberSecurityChatbot.WPF.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tasks.db");
            _connectionString = $"Data Source={dbPath}";
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Description TEXT,
                    ReminderDate TEXT,
                    IsCompleted INTEGER NOT NULL
                );";
            command.ExecuteNonQuery();
        }

        public void AddTask(CyberTask task)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Tasks (Title, Description, ReminderDate, IsCompleted) VALUES ($title, $desc, $rem, $comp)";
            command.Parameters.AddWithValue("$title", task.Title);
            command.Parameters.AddWithValue("$desc", task.Description);
            command.Parameters.AddWithValue("$rem", task.ReminderDate);
            command.Parameters.AddWithValue("$comp", task.IsCompleted ? 1 : 0);
            command.ExecuteNonQuery();
        }

        public List<CyberTask> GetTasks()
        {
            var tasks = new List<CyberTask>();
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Title, Description, ReminderDate, IsCompleted FROM Tasks";
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add(new CyberTask
                {
                    Id = reader.GetInt32(0),
                    Title = reader.GetString(1),
                    Description = reader.IsDBNull(2) ? "" : reader.GetString(2),
                    ReminderDate = reader.IsDBNull(3) ? "" : reader.GetString(3),
                    IsCompleted = reader.GetInt32(4) == 1
                });
            }
            return tasks;
        }

        public void CompleteTask(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Tasks SET IsCompleted = 1 WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tasks WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }
    }
}