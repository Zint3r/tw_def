using System;

namespace Module.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        TimeSpan ClientServerDelta { get; }
        
        DateTime UtcNow { get; }
        
        void SetCurrentServerTime(DateTime serverTime);
    }
}