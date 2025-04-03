using System;
using System.Collections.Generic;
using System.Linq;
using AvaloniaApplication8.Models;

namespace AvaloniaApplication8.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly LogManager _logManager = new();
    private string _newMessageText = string.Empty;
    private MessageType _selectedMessageType = MessageType.Information;
    private DateTime _startDate = DateTime.Now.AddDays(-1);
    private DateTime _endDate = DateTime.Now;
    private MessageType _filterType = MessageType.Information;
    private IEnumerable<LogMessage> _filteredMessages = Array.Empty<LogMessage>();

    public string NewMessageText
    {
        get => _newMessageText;
        set => SetField(ref _newMessageText, value);
    }

    public MessageType SelectedMessageType
    {
        get => _selectedMessageType;
        set => SetField(ref _selectedMessageType, value);
    }

    public DateTime StartDate
    {
        get => _startDate;
        set => SetField(ref _startDate, value);
    }

    public DateTime EndDate
    {
        get => _endDate;
        set => SetField(ref _endDate, value);
    }

    public MessageType FilterType
    {
        get => _filterType;
        set
        {
            SetField(ref _filterType, value);
            UpdateFilteredMessages();
        }
    }

    public IEnumerable<LogMessage> FilteredMessages
    {
        get => _filteredMessages;
        private set => SetField(ref _filteredMessages, value);
    }

    public int MessageCount => _logManager.MessageCount;

    public IEnumerable<MessageType> MessageTypes => Enum.GetValues(typeof(MessageType)).Cast<MessageType>();

    public MainWindowViewModel()
    {
        // Добавим несколько тестовых сообщений
        _logManager.AddMessage(new LogMessage(MessageType.Error, DateTime.Now.AddHours(-2), "Ошибка при загрузке файла"));
        _logManager.AddMessage(new LogMessage(MessageType.Warning, DateTime.Now.AddHours(-1), "Недостаточно памяти"));
        _logManager.AddMessage(new LogMessage(MessageType.Information, DateTime.Now, "Приложение запущено"));
        
        UpdateFilteredMessages();
    }

    public void AddMessage()
    {
        if (!string.IsNullOrWhiteSpace(NewMessageText))
        {
            _logManager.AddMessage(new LogMessage(SelectedMessageType, DateTime.Now, NewMessageText));
            NewMessageText = string.Empty;
            OnPropertyChanged(nameof(MessageCount));
            UpdateFilteredMessages();
        }
    }

    public void FilterByDateRange()
    {
        FilteredMessages = _logManager.GetMessagesByDateRange(StartDate, EndDate)
            .Where(m => m.Type == FilterType)
            .ToList();
    }

    public void SaveLogs()
    {

    }

    private void UpdateFilteredMessages()
    {
        FilteredMessages = _logManager.GetMessagesByType(FilterType).ToList();
    }
}