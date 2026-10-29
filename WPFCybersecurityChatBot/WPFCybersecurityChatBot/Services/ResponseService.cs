using System;
using System.Collections.Generic;
using System.Linq;
using CyberSecurityChatbot.WPF.Models;

namespace CyberSecurityChatbot.WPF.Services
{
    public class ResponseService
    {
        private readonly Random _random = new();
        private readonly SentimentService _sentiment = new();
        public MemoryService Memory { get; } = new();
        public DatabaseService Db { get; } = new();
        public ActivityLogService Logger { get; } = new();
        public QuizService Quiz { get; } = new();

        private string? _lastTopic;

        // NLP State: What is the bot waiting for?
        private bool _awaitingTaskDesc = false;
        private bool _awaitingReminder = false;
        private string _pendingTaskTitle = "";

        private readonly Dictionary<string, string[]> _responses = new()
        {
            ["password"] = new[] { "Use at least 12 characters with a mix of letters, numbers and symbols.", "Never reuse passwords.", "Enable 2FA." },
            ["phishing"] = new[] { "Be cautious of emails asking for personal information.", "Check the sender's email address carefully.", "Never click suspicious links." },
            ["privacy"] = new[] { "Review your social media privacy settings regularly.", "Avoid sharing your location publicly.", "Use a VPN on public Wi-Fi." },
            ["scam"] = new[] { "If an offer sounds too good to be true, it usually is.", "Never send money to someone you met online." },
            ["malware"] = new[] { "Keep your antivirus updated.", "Only download software from official sources." }
        };

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "Please type something.";
            string lower = input.ToLower().Trim();

            // 1. Handle pending Task/Reminder questions (NLP State)
            if (_awaitingTaskDesc)
            {
                var task = new CyberTask { Title = _pendingTaskTitle, Description = input, ReminderDate = "" };
                Db.AddTask(task);
                Logger.Log($"Task added: '{_pendingTaskTitle}'");
                _awaitingTaskDesc = false;
                _awaitingReminder = true;
                return $"Task added with description '{input}'. Would you like a reminder? (Type 'yes' or 'no')";
            }

            if (_awaitingReminder)
            {
                _awaitingReminder = false;
                if (lower.Contains("yes") || lower.Contains("remind"))
                {
                    return "Got it! I'll remind you in 3 days. (Reminder set).";
                }
                return "No reminder set. Task saved.";
            }

            // 2. NLP: Task Creation
            if (lower.Contains("add task") || lower.Contains("add a task") || lower.Contains("remind me"))
            {
                // Extract title from quotes or after the keyword
                string title = input.Replace("add task", "", StringComparison.OrdinalIgnoreCase)
                                    .Replace("add a task", "", StringComparison.OrdinalIgnoreCase)
                                    .Replace("remind me to", "", StringComparison.OrdinalIgnoreCase)
                                    .Trim().Trim('"', '\'');

                if (string.IsNullOrEmpty(title))
                {
                    return "What task would you like to add?";
                }

                _pendingTaskTitle = title;
                _awaitingTaskDesc = true;
                return $"Task started for '{title}'. Please provide a short description for this task.";
            }

            // 3. NLP: View Tasks
            if (lower.Contains("view tasks") || lower.Contains("my tasks") || lower.Contains("show tasks"))
            {
                var tasks = Db.GetTasks();
                if (tasks.Count == 0) return "You have no tasks.";
                string taskList = "Here are your tasks:\n";
                foreach (var t in tasks)
                    taskList += $"ID {t.Id}: {t.DisplayText}\n";
                return taskList;
            }

            // 4. NLP: Activity Log
            if (lower.Contains("activity log") || lower.Contains("what have you done"))
            {
                var logs = Logger.GetRecentActions();
                if (logs.Count == 0) return "No recent activity.";
                string logStr = "Here's a summary of recent actions:\n";
                int i = 1;
                foreach (var log in logs)
                    logStr += $"{i++}. {log.Action}\n";
                return logStr;
            }

            // 5. NLP: Start Quiz
            if (lower.Contains("start quiz") || lower.Contains("play game") || lower.Contains("quiz"))
            {
                Quiz.StartQuiz();
                Logger.Log("Quiz started");
                var q = Quiz.GetCurrentQuestion();
                return $"Quiz started! Question 1: {q.Question}\nOptions: {string.Join(" | ", q.Options)}";
            }

            // 6. Follow-up / Sentiment / General fallback
            if (lower.Contains("tell me more") && _lastTopic != null)
                return PickRandom(_lastTopic, "Sure — here's another tip. ");

            var sentiment = _sentiment.Detect(lower);
            string prefix = _sentiment.GetEmpathyPrefix(sentiment);

            foreach (var kv in _responses)
            {
                if (lower.Contains(kv.Key))
                {
                    _lastTopic = kv.Key;
                    Memory.RememberTopic(kv.Key);
                    return prefix + PickRandom(kv.Key);
                }
            }

            return "I didn't quite understand that. Could you rephrase? (Try 'Add task', 'Start quiz', or ask about passwords).";
        }

        private string PickRandom(string topic, string prefix = "")
        {
            var arr = _responses[topic];
            return prefix + arr[_random.Next(arr.Length)];
        }
    }
}