using System;

namespace CyberSecurityChatbot.WPF.Models
{
    public class ActivityRecord
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Action { get; set; } = "";

        public string DisplayText => $"[{Timestamp:HH:mm}] {Action}";
    }
}