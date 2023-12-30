using System.Text.RegularExpressions;

namespace NotificationChannelParser.Class
{
    public class NotificationChannelLogic
    {
        private readonly List<NotificationChannelModel> channelLists;

        public NotificationChannelLogic()
        {
            channelLists = new List<NotificationChannelModel>();
            InitializeChannels();
        }

        public void InitializeChannels()
        {
            channelLists.Add(new NotificationChannelModel("Backend", "BE"));
            channelLists.Add(new NotificationChannelModel("Frontend", "FE"));
            channelLists.Add(new NotificationChannelModel("Quality Assurance", "QA"));
            channelLists.Add(new NotificationChannelModel("Urgent", "Urgent"));
        }

        // Get List of matches tag from title
        public static List<Match> ParseNotificationTag(string title)
        {
            string pattern = @"\[(BE|FE|QA|Urgent)\]";

            MatchCollection matches = Regex.Matches(title, pattern);

            // Remove potential duplicate tag in the title
            List<Match> matchesList = matches
                .Cast<Match>()
                .GroupBy(match => match.Value)
                .Select(group => group.First())
                .ToList();

            if (matches.Count < 1)
            {
                // Do something
            }

            return matchesList;
        }

        // Remove the tag and get only the message
        public static string ParseNotificationMessage(string title)
        {
            string pattern = @"\[(.*?)\]";

            string message = Regex.Replace(title, pattern, " ").Trim();

            return message;
        }

        // Method for removing irrelevant tag from the original title
        public static string NewNotificationTitle(List<Match> matches, string title)
        {
            string newTitle = "";
            string message = ParseNotificationMessage(title);

            foreach (Match match in matches)
            {
                newTitle += match.Groups[0].Value;
            }
            newTitle += $" {message}";

            return newTitle;
        }

        // Method to assign Title/Message to the notification channel
        public void AssignToNotificationChannel(string title)
        {
            List<Match> matches = ParseNotificationTag(title);
            string newTitle = NewNotificationTitle(matches, title);

            foreach (var channel in channelLists)
            {
                foreach (var match in matches)
                {
                    string tag = match.Groups[1].Value;
                    if (channel.Tag == tag)
                    {
                        channel.TitleMessage.Add(newTitle);
                    }
                }
            }
        }

        public static string GetReceivedChannel(string title)
        {
            List<Match> matches = ParseNotificationTag(title);

            string output = "Received Channel: " + string.Join(", ", matches);

            return output;
        }

        // Method to check if title has been added properly (unrelated)
        public void DisplayTitleMessage()
        {
            Console.WriteLine("\nNotification Channels:\n");
            foreach (var channel in channelLists)
            {
                Console.WriteLine($"{channel.Name} ({channel.Tag}):");
                foreach (var title in channel.TitleMessage)
                {
                    Console.WriteLine($"  - {title}");
                }
                Console.WriteLine();
            }
        }
    }
}