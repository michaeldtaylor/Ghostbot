namespace Ghostbot.Modules.ClanWars.Model
{
    public class Modifier
    {
        public Modifier(string name, string description, string value)
        {
            Name = name;
            Description = description;
            Value = value;
        }

        public string Name { get; }
        public string Description { get; set; }
        public string Value { get; }
    }
}