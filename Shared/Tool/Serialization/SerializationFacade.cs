using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using Shared.Tool.Serialization.Result;
using Shared.Tool.Serialization.Serializer;

// ReSharper disable MemberCanBePrivate.Global

namespace Shared.Tool.Serialization
{
    using RawDataDictionary = IDictionary<string, Tuple<Type, byte[]>>;
    using DataDictionary = IDictionary<string, object>;

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

        public ISerializationResult SerializeAll(ISerializer serializer, DataDictionary nameToObject)
        {
            var nameToData = new Dictionary<string, byte[]>();
            foreach (var nameAndObject in nameToObject)
            {
                using var memoryStream = new MemoryStream();
                _dataFormatter.Serialize(memoryStream, nameAndObject.Value);
                byte[] serializedData = memoryStream.ToArray();
                nameToData.Add(nameAndObject.Key, serializedData);
            }

            return serializer.WriteSerializedData(nameToData);
        }

        public ISerializationResult<DataDictionary> Deserialize(ISerializer serializer)
        {
            var rawSerializationResult = serializer.ReadSerializedData();
            return DefaultSerializationResult<DataDictionary>.From(rawSerializationResult, rawData =>
            {
                IDictionary<string, object> keyToObject = new Dictionary<string, object>();
                foreach (var entry in rawData)
                {
                    using var byteStream = new MemoryStream(entry.Value.Item2);
                    keyToObject.Add(entry.Key, _dataFormatter.Deserialize(byteStream));
                }

                return keyToObject;
            });
        }
    }
}