using System.Collections.Generic;
using System.Linq;
using CyberSecurityChatbot.WPF.Models;

namespace CyberSecurityChatbot.WPF.Services
{
    public class ActivityLogService
    {
        private readonly List<ActivityRecord> _log = new();

        public void Log(string action)
        {
            _log.Add(new ActivityRecord { Action = action });
        }

        public List<ActivityRecord> GetRecentActions(int count = 5)
        {
            return _log.OrderByDescending(a => a.Timestamp).Take(count).ToList();
        }
    }
}