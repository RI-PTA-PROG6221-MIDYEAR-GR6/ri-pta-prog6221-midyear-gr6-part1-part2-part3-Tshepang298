using CyberSecurityChatbot.WPF.Models;
using CyberSecurityChatbot.WPF.Services;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace CyberSecurityChatbot.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ResponseService _responseService = new();
        private readonly AudioService _audioService = new();
        private readonly ChatHistoryService _historyService = new();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _audioService.PlayGreeting();
            AddBotMessage("👋 Hello! I'm your Cybersecurity Bot. What's your name?");
            InputBox.Focus();
            RefreshTasks();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) => Send();

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) Send();
        }

        private void Send()
        {
            string input = InputBox.Text.Trim();
            if (string.IsNullOrEmpty(input)) return;

            AddUserMessage(input);
            InputBox.Clear();

            // Set Name
            if (string.IsNullOrEmpty(_responseService.Memory.UserName))
            {
                _responseService.Memory.UserName = input;
                AddBotMessage($"Nice to meet you, {input}! I can help with safety tips, tasks, and quizzes.");
                UpdateMemoryPanel();
                return;
            }

            // Handle Quiz mode
            if (_responseService.Quiz.Questions.Count > 0 && !_responseService.Quiz.IsFinished)
            {
                HandleQuizAnswer(input);
                return;
            }

            // Standard Bot Response
            string reply = _responseService.GetResponse(input);
            AddBotMessage(reply);
            UpdateMemoryPanel();

            // Check if a task was added to refresh the list
            if (input.ToLower().Contains("task") || input.ToLower().Contains("remind"))
            {
                RefreshTasks();
            }

            // Check if quiz just started
            if (input.ToLower().Contains("quiz") || input.ToLower().Contains("game"))
            {
                ShowQuizQuestion();
            }
        }

        // ---------- QUIZ LOGIC ----------
        private void ShowQuizQuestion()
        {
            var q = _responseService.Quiz.GetCurrentQuestion();
            QuizQuestionText.Text = q.Question;
            QuizOptionsPanel.Children.Clear();

            foreach (var opt in q.Options)
            {
                var btn = new Button
                {
                    Content = opt,
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10),
                    Background = new SolidColorBrush(Color.FromRgb(0x33, 0x41, 0x55)),
                    Foreground = new SolidColorBrush(Colors.White),
                    HorizontalContentAlignment = HorizontalAlignment.Left
                };
                btn.Click += (s, e) =>
                {
                    InputBox.Text = opt.Substring(0, 1); // Extract A, B, C
                    Send();
                };
                QuizOptionsPanel.Children.Add(btn);
            }
        }

        private void HandleQuizAnswer(string input)
        {
            bool isCorrect = _responseService.Quiz.SubmitAnswer(input, out string explanation);
            string resultMsg = isCorrect ? "✅ Correct! " : "❌ Incorrect. ";
            resultMsg += explanation;

            QuizResultText.Text = resultMsg;

            if (_responseService.Quiz.IsFinished)
            {
                QuizResultText.Text += $"\n🏆 Quiz Finished! Your score: {_responseService.Quiz.Score}/{_responseService.Quiz.Questions.Count}";
                AddBotMessage($"Quiz Completed! Score: {_responseService.Quiz.Score}/{_responseService.Quiz.Questions.Count}");
                _responseService.Logger.Log($"Quiz completed. Score: {_responseService.Quiz.Score}");
            }
            else
            {
                ShowQuizQuestion();
            }
        }

        // ---------- TASK LOGIC ----------
        private void RefreshTasks()
        {
            TasksList.Items.Clear();
            foreach (var t in _responseService.Db.GetTasks())
            {
                TasksList.Items.Add(t.DisplayText);
            }
        }

        // ---------- CHAT BUBBLES ----------
        private void UpdateMemoryPanel()
        {
            var m = _responseService.Memory;
            string line = "";
            if (!string.IsNullOrEmpty(m.UserName)) line += $"User: {m.UserName}\n";
            if (m.HasFavouriteTopic) line += $"Interested in: {m.FavouriteTopic}";
            MemoryDisplay.Text = line;
        }

        private void AddUserMessage(string text)
        {
            var msg = new ChatMessage { Sender = "You", Text = text, IsUser = true };
            _historyService.Save(msg);
            AddBubble(text, true);
        }

        private void AddBotMessage(string text)
        {
            var msg = new ChatMessage { Sender = "Bot", Text = text, IsUser = false };
            _historyService.Save(msg);
            AddBubble(text, false);
        }

        private void AddBubble(string text, bool isUser)
        {
            var bubble = new Border
            {
                Background = new SolidColorBrush(isUser ? Color.FromRgb(0x22, 0xD3, 0xEE) : Color.FromRgb(0x33, 0x41, 0x55)),
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12, 8, 12, 8),
                Margin = new Thickness(isUser ? 80 : 0, 4, isUser ? 0 : 80, 4),
                HorizontalAlignment = isUser ? HorizontalAlignment.Right : HorizontalAlignment.Left,
                MaxWidth = 520
            };

            bubble.Child = new TextBlock
            {
                Text = text,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(isUser ? Color.FromRgb(0x0F, 0x17, 0x2A) : Colors.White),
                FontSize = 13
            };

            ChatPanel.Children.Add(bubble);

            Dispatcher.BeginInvoke(new Action(() => ChatScroll.ScrollToEnd()), DispatcherPriority.Background);
        }
    }
}