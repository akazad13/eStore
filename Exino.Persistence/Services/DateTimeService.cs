using Exino.Application.Common.Interfaces;

namespace Exino.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset UtcNow => DateTime.UtcNow;
    }
}
