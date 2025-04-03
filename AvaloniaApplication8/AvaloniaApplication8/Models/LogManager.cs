using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvaloniaApplication8.Models;

public class LogManager
{
    private readonly List<LogMessage> _messages = new();

    public int MessageCount => _messages.Count;

    public LogMessage this[int index]
    {
        get
        {
            if (index < 0 || index >= _messages.Count)
                throw new IndexOutOfRangeException();
            return _messages[index];
        }
    }

    public void AddMessage(LogMessage message)
    {
        _messages.Add(message);
    }

    public IEnumerable<LogMessage> GetMessagesByType(MessageType type)
    {
        return _messages.Where(m => m.Type == type);
    }

    public IEnumerable<LogMessage> GetMessagesByDateRange(DateTime start, DateTime end)
    {
        return _messages.Where(m => m.DateTime >= start && m.DateTime <= end);
    }

    public void SaveToFile(string filePath)
    {
        using var writer = new StreamWriter(filePath);
        foreach (var message in _messages)
        {
            writer.WriteLine(message.ToString());
        }
    }
}