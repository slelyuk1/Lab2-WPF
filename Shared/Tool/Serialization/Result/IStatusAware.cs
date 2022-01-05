using System;

namespace Shared.Tool.Serialization.Result
{
    public enum SerializationStatus
    {
        Success,
        Failure
    }

    public interface IStatusAware
    {
        SerializationStatus Status { get; }
    }
}