using System;
using System.Threading;

namespace CybersecurityChatbot
{
    public class ConsoleUI
    {
        private const string CYAN = "\u001b[36m";
        private const string GREEN = "\u001b[32m";
        private const string YELLOW = "\u001b[33m";
        private const string RED = "\u001b[31m";
        private const string RESET = "\u001b[0m";
        private const string BOLD = "\u001b[1m";

        public void DisplayHeader()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
    ╔══════════════════════════════════════════════════════════════╗
    ║                                                              ║
    ║     ██████╗██╗   ██╗██████╗ ███████╗██████╗  █████╗ ██╗      ║
    ║    ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██║      ║
    ║    ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████║██║      ║
    ║    ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██║██║      ║
    ║    ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██║  ██║██║      ║
    ║     ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝      ║
    ║                                                              ║
    ║               Cybersecurity Awareness Chatbot                ║
    ║                 Your Guide to Online Safety                  ║
    ║                                                              ║
    ╚══════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine();
            TypeEffect(" Welcome to the Cybersecurity Awareness Chatbot! ", ConsoleColor.Green);
            Console.WriteLine();
        }

        public string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" May I ask your name? ");
            Console.ResetColor();

            string name = Console.ReadLine()?.Trim();
            while (string.IsNullOrEmpty(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" I didn't catch that. Please enter your name: ");
                Console.ResetColor();
                name = Console.ReadLine()?.Trim();
            }
            return name;
        }

        public void DisplayWelcomeMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n Welcome, {userName}! ");
            Console.ResetColor();
            TypeEffect($"Hello {userName}! I'm your Cybersecurity Awareness Bot. I'm here to help you stay safe online.", ConsoleColor.Cyan);
            Console.WriteLine();
            TypeEffect("You can ask me about:", ConsoleColor.Yellow);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  • Password safety");
            Console.WriteLine("  • Phishing attacks");
            Console.WriteLine("  • Safe browsing");
            Console.WriteLine("  • Malware protection");
            Console.WriteLine("  • Social media security");
            Console.ResetColor();
            DisplaySeparator();
        }

        public void DisplaySeparator()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("───════════════════════════════════════════════════════════════════════════════════════════════───");
            Console.ResetColor();
        }

        public string GetUserInput(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($" {userName} > ");
            Console.ResetColor();

            string input = Console.ReadLine()?.Trim();

            // Input validation - empty check
            while (string.IsNullOrEmpty(input))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(" I didn't catch that. Please type your question: ");
                Console.ResetColor();
                input = Console.ReadLine()?.Trim();
            }

            return input;
        }

        public void DisplayResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" Bot > ");
            Console.ResetColor();
            TypeEffect(response, ConsoleColor.White);
            Console.WriteLine();
        }

        public void DisplayExitMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n Stay safe online, {userName}! ");
            Console.ResetColor();
            Console.WriteLine("Remember: Use strong passwords, avoid suspicious links, and keep your software updated.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(" Goodbye and stay secure!");
            Console.ResetColor();
        }

        private void TypeEffect(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(10); // Simulate typing speed
            }
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}