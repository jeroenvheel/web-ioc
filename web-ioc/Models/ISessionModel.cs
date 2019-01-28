using System;

namespace web_ioc.Models
{
    public interface ISessionModel : IDisposable
    {
        bool Value { get; set; }
    }
}
