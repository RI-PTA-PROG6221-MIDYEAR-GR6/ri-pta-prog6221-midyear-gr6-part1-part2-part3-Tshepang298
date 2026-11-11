using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbot.WPF.Services
{
    public class MemoryService
    {
        public string? UserName { get; set; }
        public string? FavouriteTopic { get; private set; }

        private readonly List<string> _topicsDiscussed = new();

        public void RememberTopic(string topic)
        {
            _topicsDiscussed.Add(topic);

            if (_topicsDiscussed.Count(t => t == topic) >= 2 || FavouriteTopic == null)
                FavouriteTopic = topic;
        }

        public bool HasFavouriteTopic => !string.IsNullOrEmpty(FavouriteTopic);
    }
}