using System;

namespace Shared.Tool.Serialization.Result
{
    public class DefaultSerializationResult<TData> : ISerializationResult<TData> where TData : class
    {
        private readonly Exception? _cause;
        private readonly TData? _data;

        public DefaultSerializationResult(SerializationStatus status)
        {
            Status = status;
        }

        public DefaultSerializationResult(TData? data) : this(SerializationStatus.Success)
        {
            _data = data;
        }

        public DefaultSerializationResult(Exception? cause) : this(SerializationStatus.Failure)
        {
            _cause = cause;
        }

        public SerializationStatus Status { get; }

        public Exception? Cause
        {
            get
            {
                Exception? toReturn = null;
                RunDependingOnStatus(Status,
                    () => throw new InvalidOperationException($"Cause cannot be retrieved in {Status} status"),
                    () => toReturn = _cause
                );
                return toReturn;
            }
        }

        public TData? Data
        {
            get
            {
                var toReturn = default(TData);
                RunDependingOnStatus(Status,
                    () => toReturn = _data,
                    () => throw new InvalidOperationException($"Cause cannot be retrieved in {Status} status")
                );
                return toReturn;
            }
        }

        public static DefaultSerializationResult<TData> From<TOtherData>(
            ISerializationResult<TOtherData> toCopy,
            Func<TOtherData, TData?> dataConversionFunction
        )
        {
            DefaultSerializationResult<TData>? result = null;
            RunDependingOnStatus(toCopy.Status,
                () =>
                {
                    TOtherData? toConvert = toCopy.Data;
                    TData? convertedData = toConvert != null ? dataConversionFunction.Invoke(toConvert) : null;
                    result = new DefaultSerializationResult<TData>(convertedData);
                },
                () => result = new DefaultSerializationResult<TData>(toCopy.Cause)
            );

            return result ?? throw new InvalidOperationException($"Result is expected to be non null at this point for: {toCopy}");
        }

        public void Process(Action success, Action<Exception?> failure)
        {
            Process(_ => success.Invoke(), failure);
        }

        public void Process(Action<TData?> success, Action<Exception?> failure)
        {
            RunDependingOnStatus(
                Status,
                () => success.Invoke(Data),
                () => failure.Invoke(Cause)
            );
        }

        private static void RunDependingOnStatus(SerializationStatus status, Action success, Action failure)
        {
            switch (status)
            {
                case SerializationStatus.Success:
                    success.Invoke();
                    break;
                case SerializationStatus.Failure:
                    failure.Invoke();
                    break;
                default:
                    throw new InvalidOperationException($"Status {status} was not expected here");
            }
        }
    }
}