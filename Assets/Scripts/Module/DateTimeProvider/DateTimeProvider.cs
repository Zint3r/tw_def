using System;

namespace Module.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public TimeSpan ClientServerDelta { get; private set; }

        public DateTime UtcNow => DateTime.UtcNow.Subtract(ClientServerDelta);

        public void SetCurrentServerTime(DateTime serverTime)
        {
            ClientServerDelta = DateTime.UtcNow - serverTime;
        }
    }
}