using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Shared.Tool.Serialization.Result;

namespace Shared.Tool.Serialization.Serializer
{
    using DataDictionary = IDictionary<string, Tuple<Type, byte[]>>;

    public class FileSerializer : ISerializer
    {
        private readonly string _fileName;

        public FileSerializer(string fileName)
        {
            _fileName = fileName;
        }

        public ISerializationResult<DataDictionary> ReadSerializedData()
        {
            try
            {
                using var reader = new ResourceReader(_fileName);
                IDictionaryEnumerator enumerator = reader.GetEnumerator();
                var nameToData = new Dictionary<string, Tuple<Type, byte[]>>();
                while (enumerator.MoveNext())
                {
                    string name = (string?) enumerator.Key ?? throw new NullReferenceException("Key cannot be null");
                    reader.GetResourceData(name, out string typeName, out byte[] data);
                    nameToData.Add(name, Tuple.Create(Type.GetType(typeName), data));
                }

                return new DefaultSerializationResult<DataDictionary>(nameToData);
            }
            catch (Exception e)
            {
                return new DefaultSerializationResult<DataDictionary>(e);
            }
        }

        public ISerializationResult WriteSerializedData(IDictionary<string, byte[]> nameToData)
        {
            try
            {
                using var writer = new ResourceWriter(_fileName);
                foreach (KeyValuePair<string, byte[]> nameAndData in nameToData)
                {
                    byte[] data = nameAndData.Value;
                    writer.AddResourceData(nameAndData.Key, data.GetType().FullName, data);
                }
            }
            catch (Exception e)
            {
                return new DefaultSerializationResult<object>(e);
            }

            return new DefaultSerializationResult<object>(SerializationStatus.Success);
        }
    }
}