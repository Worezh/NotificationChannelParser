using NotificationChannelParser.Class;

namespace NotificationChannelParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NotificationChannelLogic logic = new NotificationChannelLogic();
            WelcomeMessage();

            do
            {
                string title = GetTitleInput();
                logic.AssignToNotificationChannel(title);
                Console.WriteLine();

                Console.WriteLine(NotificationChannelLogic.GetReceivedChannel(title));
            } while (AskToContinue() == true);

            //logic.DisplayTitleMessage();
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("Welcome to the Notification Channel Parser App.");
            Console.WriteLine();
        }

        public static string GetTitleInput()
        {
            string output = "";
            bool isValidTitle = true;
            do
            {
                Console.Write("Enter a notification title: ");
                output = Console.ReadLine(); 

                if (!string.IsNullOrWhiteSpace(output))
                {
                    // Check if the title contains at least one tag
                    if (NotificationChannelLogic.ParseNotificationTag(output).Count > 0)
                    {
                        isValidTitle = true;
                    }
                    else
                    {
                        Console.WriteLine("Error: The notification title must contain at least one tag enclosed in square brackets.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: The notification title cannot be empty.");
                }

            } while (isValidTitle == true);

            return output;
        }

        public static bool AskToContinue()
        {
            Console.Write("Do you want to keep adding more notification title? (yes/no): ");
            string checkYesNo = Console.ReadLine();
            Console.WriteLine();

            if (checkYesNo.ToLower() == "yes")
            {
                Console.WriteLine("=======================================");
                return true;
            }
            else
            {
                Console.WriteLine("===The app has been stopped===");
                return false;
            }
        }
    }
}