using System;

namespace CyberSecurityChatbot.WPF.Models
{
    public class CyberTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string ReminderDate { get; set; } = "";
        public bool IsCompleted { get; set; }

        public string DisplayText => IsCompleted
            ? $"[✔] {Title} (Completed)"
            : $"[ ] {Title} - Reminder: {(string.IsNullOrEmpty(ReminderDate) ? "None" : ReminderDate)}";
    }
}