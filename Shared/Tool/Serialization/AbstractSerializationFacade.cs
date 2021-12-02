using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Shared.Tool.Serialization
{
    // todo refactor according to OOP
    public abstract class AbstractSerializationFacade
    {
        private readonly IFormatter _dataFormatter;

        protected AbstractSerializationFacade(IFormatter dataFormatter)
        {
            _dataFormatter = dataFormatter;
        }

        public void Serialize<T>(string name, T value) where T : class
        {
            SerializeAll(new Dictionary<string, object> {{name, value}});
        }

        public void SerializeAll<T>(IDictionary<string, T> nameToObject) where T : class
        {
            var nameToData = new Dictionary<string, byte[]>();
            foreach (KeyValuePair<string, T> nameAndObject in nameToObject)
            {
                using var memoryStream = new MemoryStream();
                _dataFormatter.Serialize(memoryStream, nameAndObject.Value);
                byte[] serializedData = memoryStream.ToArray();
                nameToData.Add(nameAndObject.Key, serializedData);
            }

            WriteSerializedData(nameToData);
        }

        public T? Deserialize<T>(string name) where T : class
        {
            IDictionary<string, Tuple<Type, byte[]>>? nameToData = ReadSerializedData();
            if (nameToData == null)
            {
                return null;
            }

            if (!nameToData.TryGetValue(name, out Tuple<Type, byte[]> data))
            {
                return null;
            }

            using var byteStream = new MemoryStream(data.Item2);
            return (T) _dataFormatter.Deserialize(byteStream);
        }

        protected abstract IDictionary<string, Tuple<Type, byte[]>>? ReadSerializedData();
        protected abstract void WriteSerializedData(IDictionary<string, byte[]> nameToData);
    }
}