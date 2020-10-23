namespace WebGateway.Messaging
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/lustars/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterNewAccountQueue = "registernewaccount.auth.service";
        public const string RegisterNewUserProfileQueue = "registernewprofile.profile.service";
        public const string NotificationServiceQueue = "notificationservice.notification.service";
    }
}
