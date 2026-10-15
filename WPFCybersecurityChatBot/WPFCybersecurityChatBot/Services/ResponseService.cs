using System;
using System.Collections.Generic;

namespace CyberSecurityChatbot.WPF.Services
{
    public class ResponseService
    {
        private readonly Random _random = new();
        private readonly SentimentService _sentiment = new();

        public MemoryService Memory { get; } = new();

        private string? _lastTopic;

        private readonly Dictionary<string, string[]> _responses = new()
        {
            ["password"] = new[]
            {
                "Use at least 12 characters with a mix of letters, numbers and symbols.",
                "Never reuse passwords across accounts — each one should be unique.",
                "Consider a password manager like Bitwarden or 1Password.",
                "Enable two-factor authentication wherever possible."
            },
            ["phishing"] = new[]
            {
                "Be cautious of emails asking for personal information — scammers often impersonate trusted organisations.",
                "Check the sender's email address carefully; small typos are a red flag.",
                "Never click suspicious links. Hover first to see the real URL.",
                "If an email creates urgency (\"act now!\"), treat it as suspicious."
            },
            ["privacy"] = new[]
            {
                "Review your social media privacy settings regularly.",
                "Avoid sharing your location, birth date, or ID numbers publicly.",
                "Use a VPN on public Wi-Fi to protect your traffic.",
                "Consider using a privacy-focused browser like Firefox or Brave."
            },
            ["scam"] = new[]
            {
                "If an offer sounds too good to be true, it usually is.",
                "Never send money or gift cards to someone you met online.",
                "Verify the identity of anyone requesting sensitive information."
            },
            ["malware"] = new[]
            {
                "Keep your antivirus and OS updated at all times.",
                "Only download software from official sources.",
                "Be wary of email attachments from unknown senders."
            }
        };

        private readonly string[] _fallbacks =
        {
            "I'm not sure I understand. Can you try rephrasing?",
            "Hmm, I didn't quite catch that. Could you ask about passwords, phishing, privacy, scams, or malware?",
            "I don't have an answer for that yet, but I can help with online safety topics like phishing and privacy."
        };

        public string GetResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "Please type a message so I can help you.";

            string lower = input.ToLower().Trim();

            // 1. Follow-up
            if (lower.Contains("tell me more") || lower.Contains("another") ||
                lower.Contains("explain more") || lower.Contains("more tip"))
            {
                if (_lastTopic != null)
                    return PickRandom(_lastTopic, prefix: "Sure — here's another tip. ");
                return "What topic would you like me to expand on?";
            }

            // 2. Sentiment
            var sentiment = _sentiment.Detect(lower);
            string prefix = _sentiment.GetEmpathyPrefix(sentiment);

            // 3. Keyword
            foreach (var kv in _responses)
            {
                if (lower.Contains(kv.Key))
                {
                    _lastTopic = kv.Key;
                    Memory.RememberTopic(kv.Key);
                    return prefix + PickRandom(kv.Key);
                }
            }

            // 4. Fallback
            return PickRandomFrom(_fallbacks);
        }

        private string PickRandom(string topic, string prefix = "")
        {
            var arr = _responses[topic];
            return prefix + arr[_random.Next(arr.Length)];
        }

        private string PickRandomFrom(string[] arr) => arr[_random.Next(arr.Length)];
    }
}