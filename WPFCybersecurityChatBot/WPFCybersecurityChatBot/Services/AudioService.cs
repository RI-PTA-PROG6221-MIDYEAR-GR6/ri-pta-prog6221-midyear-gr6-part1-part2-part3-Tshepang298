using System;
using System.IO;
using System.Media;

namespace CyberSecurityChatbot.WPF.Services
{
    public class AudioService
    {
        public void PlayGreeting()
        {
            try
            {
                string path = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Audio",
                    "greeting.wav");

                if (File.Exists(path))
                {
                    using var player = new SoundPlayer(path);
                    player.Play();
                }
            }
            catch
            {
                // Silent fail — voice is optional
            }
        }
    }
}