namespace NotificationChannelParser.Class
{
    public class NotificationChannelModel(string name, string tag)
    {
        public string Name { get; set; } = name;
        public string Tag { get; set; } = tag;
        public List<string> TitleMessage { get; set; } = new List<string>();
    }
}