using System;

namespace AvaloniaApplication8.Models;

public struct LogMessage
{
    public MessageType Type { get; set; }
    public DateTime DateTime { get; set; }
    public string Text { get; set; }

    public LogMessage(MessageType type, DateTime dateTime, string text)
    {
        Type = type;
        DateTime = dateTime;
        Text = text;
    }

    public override string ToString()
    {
        return $"[{DateTime}] {Type}: {Text}";
    }
}