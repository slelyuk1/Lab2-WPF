using System;

namespace Shared.Tool.Serialization.Result
{
    public interface ISerializationResult : IStatusAware
    {
        Exception? Cause { get; }

        void Process(Action success, Action<Exception?> failure);
    }

    public interface ISerializationResult<out T> : ISerializationResult
    {
        T? Data { get; }

        void Process(Action<T?> success, Action<Exception?> failure);
    }
}