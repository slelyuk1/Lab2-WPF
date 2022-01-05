using System;
using System.Collections.Generic;
using Shared.Tool.Serialization.Result;

namespace Shared.Tool.Serialization.Serializer
{
    using DataDictionary = IDictionary<string, Tuple<Type, byte[]>>;

    public interface ISerializer
    {
        ISerializationResult<DataDictionary> ReadSerializedData();
        ISerializationResult WriteSerializedData(IDictionary<string, byte[]> nameToData);
    }
}