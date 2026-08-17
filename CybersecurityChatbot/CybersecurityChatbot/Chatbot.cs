using System;
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
            //// Play voice greeting
            //PlayVoiceGreeting();

            // Display ASCII art header
            _consoleUI.DisplayHeader();

            // Get user name
            _userName = _consoleUI.GetUserName();

            // Welcome message
            _consoleUI.DisplayWelcomeMessage(_userName);

            // Main conversation loop
            RunConversationLoop();
        }

        //private void PlayVoiceGreeting()
        //{
        //    try
        //    {
        //        string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "greeting.wav");
        //        if (File.Exists(audioPath))
        //        {
        //            using (SoundPlayer player = new SoundPlayer(audioPath))
        //            {
        //                player.PlaySync();
        //            }
        //        }
        //        else
        //        {
        //            // Fallback to text-only greeting if audio file not found
        //            Console.WriteLine("[Audio greeting not found. Welcome to the Cybersecurity Awareness Bot!]");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Note: Could not play audio greeting. {ex.Message}");
        //    }
        //}

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