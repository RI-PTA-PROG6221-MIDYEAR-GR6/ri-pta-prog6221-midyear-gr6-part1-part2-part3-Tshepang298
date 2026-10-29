using System.Collections.Generic;
using System.Linq;

namespace CyberSecurityChatbot.WPF.Services
{
    public class QuizQuestion
    {
        public string Question { get; set; } = "";
        public List<string> Options { get; set; } = new();
        public string CorrectAnswer { get; set; } = "";
        public string Explanation { get; set; } = "";
    }

    public class QuizService
    {
        public List<QuizQuestion> Questions { get; private set; } = new();
        public int CurrentIndex { get; private set; } = 0;
        public int Score { get; private set; } = 0;

        public void StartQuiz()
        {
            CurrentIndex = 0;
            Score = 0;
            Questions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What should you do if you receive an email asking for your password?",
                    Options = new() { "A) Reply with your password", "B) Delete the email", "C) Report it as phishing" },
                    CorrectAnswer = "C", Explanation = "Reporting phishing helps prevent scams." },
                new QuizQuestion { Question = "True or False: You should use the same password for all accounts.",
                    Options = new() { "A) True", "B) False" },
                    CorrectAnswer = "B", Explanation = "Never reuse passwords across different accounts." },
                new QuizQuestion { Question = "What is a sign of a phishing email?",
                    Options = new() { "A) Urgent language", "B) Misspelled sender address", "C) Both A and B" },
                    CorrectAnswer = "C", Explanation = "Scammers use urgency and fake addresses." },
                new QuizQuestion { Question = "Which of these is a strong password?",
                    Options = new() { "A) password123", "B) T7#vL9!qP2@x", "C) YourBirthDate" },
                    CorrectAnswer = "B", Explanation = "Complex passwords with symbols are strongest." },
                new QuizQuestion { Question = "True or False: HTTPS means a website is 100% safe.",
                    Options = new() { "A) True", "B) False" },
                    CorrectAnswer = "B", Explanation = "HTTPS encrypts data, but scammers also use it." }
            };
        }

        public QuizQuestion GetCurrentQuestion() => Questions[CurrentIndex];

        public bool SubmitAnswer(string answer, out string explanation)
        {
            var q = Questions[CurrentIndex];
            explanation = q.Explanation;
            bool isCorrect = answer.StartsWith(q.CorrectAnswer);
            if (isCorrect) Score++;
            CurrentIndex++;
            return isCorrect;
        }

        public bool IsFinished => CurrentIndex >= Questions.Count;
    }
}