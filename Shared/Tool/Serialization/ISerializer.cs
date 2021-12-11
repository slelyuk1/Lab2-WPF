using System;
using System.Collections.Generic;

namespace Shared.Tool.Serialization
{
    public interface ISerializer
    {
        IDictionary<string, Tuple<Type, byte[]>>? ReadSerializedData();
        void WriteSerializedData(IDictionary<string, byte[]> nameToData);
    }
}