namespace MessageExchangeContract
{
    using MassTransit;

    public interface IUpdateUserProfile
    {
        MessageData<string> MessageData { get; set; }
    }
}
