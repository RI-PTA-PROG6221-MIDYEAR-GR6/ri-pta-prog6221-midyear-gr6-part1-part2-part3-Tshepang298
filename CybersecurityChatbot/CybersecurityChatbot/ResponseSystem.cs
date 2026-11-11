using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CybersecurityChatbot
{
    public class ResponseSystem
    {
        private Dictionary<string, string[]> _keywordResponses;
        private string[] _fallbackResponses;

        public ResponseSystem()
        {
            InitializeResponses();
        }

        private void InitializeResponses()
        {
            _keywordResponses = new Dictionary<string, string[]>
            {
                // General queries
                { "how are you", new[] { "I'm functioning optimally and ready to help you stay safe online!", "All systems secure! How can I assist you today?", "I'm great, thanks for asking! Let's talk about cybersecurity." } },
                { "purpose", new[] { "My purpose is to help you stay safe online by providing cybersecurity awareness tips and advice.", "I'm here to educate and empower you about online safety practices." } },
                { "help", new[] { "I can help you with: password safety, phishing prevention, secure browsing, and general cybersecurity tips." } },
                
                // Password safety
                { "password", new[] { "Strong passwords: Use at least 12 characters with uppercase, lowercase, numbers, and symbols. Never reuse passwords!", "Enable 2FA (Two-Factor Authentication) whenever possible for an extra layer of security." } },
                { "password safety", new[] { "Never share your passwords with anyone. Use a password manager like Bitwarden or LastPass." } },
                
                // Phishing
                { "phishing", new[] { "Phishing attacks try to trick you into revealing sensitive information. Always verify the sender's email address.", "Look for misspellings, suspicious links, and urgent language in emails. When in doubt, don't click!" } },
                { "phishing email", new[] { "If you receive a suspicious email: Don't click any links, don't download attachments, and report it to your IT department." } },
                
                // Safe browsing
                { "safe browsing", new[] { "Use HTTPS websites, keep your browser updated, and consider using ad-blockers for extra security.", "Avoid public Wi-Fi for sensitive transactions unless using a VPN." } },
                { "browsing", new[] { "Clear your browser cache regularly and use private/incognito mode for sensitive browsing." } },
                
                // Malware
                { "malware", new[] { "Protect yourself from malware: Keep your antivirus updated, don't download suspicious files, and be cautious of email attachments." } },
                { "virus", new[] { "Protect against viruses: Use reputable antivirus software, avoid suspicious downloads, and keep your system updated." } },
                
                // Social media
                { "social media", new[] { "Be careful what you share on social media. Oversharing can put you at risk of identity theft and social engineering attacks." } }
            };

            _fallbackResponses = new[]
            {
                "I didn't quite understand that. Could you rephrase? I can help with: passwords, phishing, safe browsing, malware, and social media security.",
                "I'm not sure I follow. Try asking me about password safety, phishing, or safe browsing.",
                "That's an interesting question! Could you be more specific? I'm best at helping with cybersecurity topics.",
                "I don't have a response for that. Try asking me about online safety tips."
            };
        }

        public string GetResponse(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return "I noticed you didn't type anything. Feel free to ask me about cybersecurity topics!";
            }

            // Clean the input
            string cleanedInput = userInput.ToLower().Trim();

            // Check for exact matches first
            foreach (var kvp in _keywordResponses)
            {
                if (cleanedInput.Contains(kvp.Key))
                {
                    string[] responses = kvp.Value;
                    Random Random = new Random();
                    int index = Random.Next(responses.Length);
                    return responses[index];
                }
            }

            // Check for greetings
            if (IsGreeting(cleanedInput))
            {
                return $"Hello there! I'm your Cybersecurity Awareness Bot. What would you like to know about?";
            }

            // Check for farewells
            if (IsFarewell(cleanedInput))
            {
                return "Stay safe online! Remember to use strong passwords and be cautious of phishing attempts. Goodbye!";
            }

            // Return a fallback response
            Random random = new Random();
            int fallbackIndex = random.Next(_fallbackResponses.Length);
            return _fallbackResponses[fallbackIndex];
        }

        private bool IsGreeting(string input)
        {
            string[] greetings = { "hello", "hi", "hey", "greetings", "good morning", "good afternoon", "good evening" };
            foreach (string greeting in greetings)
            {
                if (input.Contains(greeting))
                    return true;
            }
            return false;
        }

        private bool IsFarewell(string input)
        {
            string[] farewells = { "goodbye", "bye", "see you", "later", "talk to you later" };
            foreach (string farewell in farewells)
            {
                if (input.Contains(farewell))
                    return true;
            }
            return false;
        }
    }
}