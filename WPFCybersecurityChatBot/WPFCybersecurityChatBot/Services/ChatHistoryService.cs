using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CyberSecurityChatbot.WPF.Models;

namespace CyberSecurityChatbot.WPF.Services
{
    public class ChatHistoryService
    {
        private readonly string _historyFile;

        public ChatHistoryService()
        {
            string folder = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "ChatLogs");

            Directory.CreateDirectory(folder);

            _historyFile = Path.Combine(folder,
                $"chat_{DateTime.Now:yyyy-MM-dd}.txt");

            File.AppendAllText(_historyFile,
                $"\n===== New Session {DateTime.Now:yyyy-MM-dd HH:mm:ss} =====\n");
        }

        public void Save(ChatMessage message)
        {
            try
            {
                File.AppendAllText(_historyFile,
                    message.FormattedLine + Environment.NewLine);
            }
            catch { }
        }

        public void SaveAll(IEnumerable<ChatMessage> messages)
        {
            try
            {
                File.WriteAllLines(_historyFile, messages.Select(m => m.FormattedLine));
            }
            catch { }
        }

        public string HistoryFilePath => _historyFile;
    }
}