using System;
using System.IO;
using System.Media;

namespace CybersecurityChatbot
{
    public class Chatbot
    {
        private ResponseSystem _responseSystem;
        private ConsoleUI _consoleUI;
        private string _userName;

        public Chatbot()
        {
            _responseSystem = new ResponseSystem();
            _consoleUI = new ConsoleUI();
        }

        public void Start()
        {
            // Play voice greeting
            PlayVoiceGreeting();

            // Display ASCII art header
            _consoleUI.DisplayHeader();

            // Get user name
            _userName = _consoleUI.GetUserName();

            // Welcome message
            _consoleUI.DisplayWelcomeMessage(_userName);

            // Main conversation loop
            RunConversationLoop();
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                // Try multiple possible paths for the audio file
                string[] possiblePaths = new string[]
                {
                    // Path where Audio folder is in project root (relative to executable)
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "greeting.wav"),
                    // If running from bin/debug/net9.0, go up to project root
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Audio", "greeting.wav"),
                    // Current directory
                    Path.Combine(Directory.GetCurrentDirectory(), "Audio", "greeting.wav"),
                    // Simple relative path
                    "Audio/greeting.wav",
                    // If WAV is directly in project root
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "greeting.wav")
                };

                string audioPath = null;
                foreach (string path in possiblePaths)
                {
                    try
                    {
                        string fullPath = Path.GetFullPath(path);
                        if (File.Exists(fullPath))
                        {
                            audioPath = fullPath;
                            break;
                        }
                    }
                    catch { /* Skip invalid paths */ }
                }

                if (audioPath != null && File.Exists(audioPath))
                {
                    Console.WriteLine($"🔊 Playing audio greeting...");
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync(); // Play and wait for completion
                    }
                }
                else
                {
                    // Fallback to text-only greeting if audio file not found
                    Console.WriteLine("[Audio greeting not found. Welcome to the Cybersecurity Awareness Bot!]");
                    Console.WriteLine("💡 Tip: Place greeting.wav in the Audio folder.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Note: Could not play audio greeting. {ex.Message}");
            }
        }

        private void RunConversationLoop()
        {
            bool running = true;

            while (running)
            {
                _consoleUI.DisplaySeparator();
                string userInput = _consoleUI.GetUserInput(_userName);

                // Check for exit command
                if (userInput.ToLower() == "exit" || userInput.ToLower() == "quit")
                {
                    _consoleUI.DisplayExitMessage(_userName);
                    running = false;
                    continue;
                }

                // Process the input and get response
                string response = _responseSystem.GetResponse(userInput);

                // Add typing effect for conversational feel
                _consoleUI.DisplayResponse(response);
            }
        }
    }
}