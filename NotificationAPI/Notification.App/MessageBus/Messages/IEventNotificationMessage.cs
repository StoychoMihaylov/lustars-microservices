namespace MessageExchangeContract
{
    using System;

    enum MessageType
    {
        Info = 1,
        Success = 2,
        Error = 3
    }

    public interface IEventNotificationMessage
    {
        Guid UserId { get; set; }

        int MessageType { get; set; }

        string Message { get; set; }
    }
}
