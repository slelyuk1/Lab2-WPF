using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace Shared.Tool.Serialization
{
    public class SerializationFacade
    {
        private readonly IFormatter _dataFormatter;

        public SerializationFacade(IFormatter dataFormatter)
        {
            _dataFormatter = dataFormatter;
        }

        public void Serialize<T>(ISerializer serializer, string name, T value) where T : class
        {
            SerializeAll(serializer, new Dictionary<string, object> {{name, value}});
        }

        public void SerializeAll<T>(ISerializer serializer, IDictionary<string, T> nameToObject) where T : class
        {
            var nameToData = new Dictionary<string, byte[]>();
            foreach (KeyValuePair<string, T> nameAndObject in nameToObject)
            {
                using var memoryStream = new MemoryStream();
                _dataFormatter.Serialize(memoryStream, nameAndObject.Value);
                byte[] serializedData = memoryStream.ToArray();
                nameToData.Add(nameAndObject.Key, serializedData);
            }

            // todo maybe return some status
            serializer.WriteSerializedData(nameToData);
        }

        public IDictionary<string, object>? Deserialize(ISerializer serializer)
        {
            IDictionary<string, Tuple<Type, byte[]>>? nameToData = serializer.ReadSerializedData();
            if (nameToData == null)
            {
                return null;
            }

            IDictionary<string, object> result = new Dictionary<string, object>();
            foreach (KeyValuePair<string, Tuple<Type, byte[]>> entry in nameToData)
            {
                using var byteStream = new MemoryStream(entry.Value.Item2);
                result.Add(entry.Key, _dataFormatter.Deserialize(byteStream));
            }

            return result;
        }
    }
}