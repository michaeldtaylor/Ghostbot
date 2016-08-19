using System;
using System.Collections.Generic;
using Ghostbot.Infrastructure;

namespace Ghostbot.Modules.ClanWars.Model
{
    public class Event
    {
        public Event(int id, string title, string description, IEnumerable<Modifier> modifiers)
        {
            Id = id;
            Title = title;
            Description = description;
            Modifiers = modifiers;
        }

        public Event(string title, Uri uri = null)
        {
            Title = title;
            Uri = uri;

            var parts = uri?.PathAndQuery.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts?.Length > 0)
            {
                Id = int.Parse(parts[parts.Length - 1]);
            }
        }

        public Event(HtmlLink eventLink) : this(eventLink.Title, eventLink.Uri)
        {
        }

        public int Id { get; }
        public string Title { get; }
        public Uri Uri { get; }
        public string Description { get; }
        public IEnumerable<Modifier> Modifiers { get; }
    }
}