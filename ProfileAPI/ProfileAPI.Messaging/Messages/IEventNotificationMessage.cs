namespace MessageExchangeContract
{
    enum MessageType
    {
        Info = 1,
        Success = 2,
        Error = 3
    }

    public interface IEventNotificationMessage
    {
        int MessageType { get; set; }

        string Message { get; set; }
    }
}
