using eStore.Application.Common.Interfaces;

namespace eStore.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset UtcNow => DateTime.UtcNow;
    }
}
