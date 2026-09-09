# Cybersecurity Awareness Chatbot - Part 1

## 📋 Overview
A command-line cybersecurity awareness chatbot with voice greeting, ASCII art, and interactive conversation about online safety practices. This application helps users learn about cybersecurity through an engaging conversational interface.

---

## ✨ Features
- 🎤 **Voice Greeting** - Plays a recorded WAV greeting when the application launches
- 🎨 **ASCII Art Display** - Shows a "Cybersecurity Awareness Bot" logo header
- 💬 **Personalized Interaction** - Asks for and uses the user's name throughout the conversation
- 🔒 **Cybersecurity Responses** - Provides information about:
  - Password safety
  - Phishing attacks
  - Safe browsing
  - Malware protection
  - Social media security
- ✅ **Input Validation** - Handles empty entries and unsupported queries gracefully
- 🎨 **Enhanced Console UI** - Colored text, spacing, borders, and typing effects
- 🐙 **Version Control** - GitHub with CI workflow (minimum 6 commits)

---

## 📁 Project Structure

CybersecurityChatbot/
├── .github/
│ └── workflows/
│ └── dotnet.yml # CI workflow
├── Audio/
│ └── greeting.wav # Voice greeting audio file
├── Chatbot.cs # Main bot logic and flow
├── ConsoleUI.cs # User interface and formatting
├── ResponseSystem.cs # Response handling and keyword matching
├── Program.cs # Application entry point
├── CybersecurityChatbot.csproj
├── .gitignore
└── README.md


---

## 🚀 Installation and Running

### Prerequisites
- .NET 9.0 SDK or higher
- Windows OS (for audio playback with System.Media)
- Git (for version control)

### Installation Steps

1. **Clone the repository**
```bash
git clone https://github.com/RI-PTA-PROG6221-MIDYEAR-GR6/ri-pta-prog6221-midyear-gr6-part1-part2-part3-Tshepang298/tree/main
cd CybersecurityChatbot

2.Restore dependencies

dotnet restore

3.Build the project

dotnet build

4.Run the application

dotnet run

USAGE EXAMPLES

Starting The Application

🔊 Playing audio greeting...

    ╔══════════════════════════════════════════════════════════════╗
    ║                                                              ║
    ║     ██████╗██╗   ██╗██████╗ ███████╗██████╗  █████╗ ██╗      ║
    ║    ██╔════╝╚██╗ ██╔╝██╔══██╗██╔════╝██╔══██╗██╔══██╗██║      ║
    ║    ██║      ╚████╔╝ ██████╔╝█████╗  ██████╔╝███████║██║      ║
    ║    ██║       ╚██╔╝  ██╔══██╗██╔══╝  ██╔══██╗██╔══██║██║      ║
    ║    ╚██████╗   ██║   ██████╔╝███████╗██║  ██║██║  ██║██║      ║
    ║     ╚═════╝   ╚═╝   ╚═════╝ ╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝╚═╝      ║
    ║                                                              ║
    ║           Cybersecurity Awareness Chatbot                    ║
    ║                 Your Guide to Online Safety                  ║
    ║                                                              ║
    ╚══════════════════════════════════════════════════════════════╝

 Welcome to the Cybersecurity Awareness Chatbot! 

👤 May I ask your name? John

 Welcome, John! 
Hello John! I'm your Cybersecurity Awareness Bot. I'm here to help you stay safe online.

You can ask me about:
  • Password safety
  • Phishing attacks
  • Safe browsing
  • Malware protection
  • Social media security
───══════════════════════════════════════════════════════════════════════════════════════════───


SAMPLE INTERACTION
💬 John > How are you?
🤖 Bot > I'm functioning optimally and ready to help you stay safe online!

💬 John > What is phishing?
🤖 Bot > Phishing attacks try to trick you into revealing sensitive information. Always verify the sender's email address.

💬 John > Tell me about passwords
🤖 Bot > Strong passwords: Use at least 12 characters with uppercase, lowercase, numbers, and symbols. Never reuse passwords!

💬 John > exit
🛡️ Stay safe online, John! 🛡️
Remember: Use strong passwords, avoid suspicious links, and keep your software updated.
👋 Goodbye and stay secure!



Topics You Can Ask About
Topic	             Example Questions
Password Safety:	"What makes a strong password?", "Tell me about password safety"
Phishing:	"What is phishing?", "How do I spot a phishing email?"
Safe Browsing:	"How do I browse safely?", "What is safe browsing?"
Malware:	"How do I protect against malware?", "What is a virus?"
Social Media:	"How do I stay safe on social media?"
General	"How are you?", "What's your purpose?", "Help"


CI/CD Status

GitHub Actions Workflow

This project uses GitHub Actions for Continuous Integration (CI). The workflow:

Builds the .NET project

Runs on every push and pull request to main/master

Ensures the code compiles successfully

https://github.com/RI-PTA-PROG6221-MIDYEAR-GR6/ri-pta-prog6221-midyear-gr6-part1-part2-part3-Tshepang298/tree/main/.github/workflows


# This workflow will build a .NET project
# For more information see: https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net

name: .NET

on:
  push:
    branches: [ "main" ]
  pull_request:
    branches: [ "main" ]

jobs:
  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v4
    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 8.0.x
    - name: Restore dependencies
      run: dotnet restore CybersecurityChatbot/CybersecurityChatbot.sln
    - name: Build
      run: dotnet build --no-restore CybersecurityChatbot/CybersecurityChatbot.sln
    - name: Test
      run: dotnet test --no-build --verbosity normal CybersecurityChatbot/CybersecurityChatbot.sln


Commit History


The repository contains a minimum of 6 meaningful commits:

Initial commit: Set up project structure and main files

Add Chatbot class: Implement core functionality and voice greeting

Implement ResponseSystem: Add cybersecurity topic responses

Add ConsoleUI: Implement formatting, colors, and typing effect

Add voice greeting: Include WAV audio file

Set up CI workflow: Configure GitHub Actions for automated builds

Update README: Add comprehensive documentation




Code Structure Explanation


1. Program.cs
Entry point of the application

Handles global exception handling

Instantiates and starts the Chatbot

2. Chatbot.cs
Main orchestration class

Manages the conversation flow

Plays the voice greeting

Handles the main loop

3. ConsoleUI.cs
Manages all user interface elements

Displays ASCII art header

Handles user input with validation

Provides colored output and typing effects

4. ResponseSystem.cs
Contains cybersecurity knowledge base

Uses keyword matching for responses

Provides fallback responses for unrecognized queries

Handles greetings and farewells



