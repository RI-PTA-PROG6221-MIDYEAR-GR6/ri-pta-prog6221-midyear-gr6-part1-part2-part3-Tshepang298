using System;

namespace CyberSecurityChatbot.WPF.Models
{
    public class ChatMessage
    {
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public string Sender { get; set; } = "";
        public string Text { get; set; } = "";
        public bool IsUser { get; set; }

        public string FormattedLine =>
            $"[{Timestamp:yyyy-MM-dd HH:mm:ss}] {Sender}: {Text}";
    }
}