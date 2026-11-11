using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbot.WPF.Services
{
    public enum Sentiment { Neutral, Worried, Curious, Frustrated, Happy }

    public class SentimentService
    {
        private readonly Dictionary<Sentiment, string[]> _keywords = new()
        {
            [Sentiment.Worried] = new[] { "worried", "scared", "afraid", "anxious", "nervous", "unsure", "overwhelmed" },
            [Sentiment.Curious] = new[] { "curious", "interested", "wondering", "want to know", "how does", "why" },
            [Sentiment.Frustrated] = new[] { "frustrated", "annoyed", "angry", "confused", "hate", "stuck", "ugh" },
            [Sentiment.Happy] = new[] { "happy", "great", "awesome", "thanks", "thank you", "love" }
        };

        public Sentiment Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return Sentiment.Neutral;

            string lower = input.ToLower();
            foreach (var kv in _keywords)
            {
                if (kv.Value.Any(k => lower.Contains(k)))
                    return kv.Key;
            }
            return Sentiment.Neutral;
        }

        public string GetEmpathyPrefix(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Worried => "It's completely understandable to feel that way. ",
            Sentiment.Frustrated => "I hear you — that sounds frustrating. ",
            Sentiment.Curious => "Great question! ",
            Sentiment.Happy => "Glad you're enjoying this! ",
            _ => ""
        };
    }
}