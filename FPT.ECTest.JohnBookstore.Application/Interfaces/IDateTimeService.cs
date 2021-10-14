using System;

namespace FPT.ECTest.JohnBookstore.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
