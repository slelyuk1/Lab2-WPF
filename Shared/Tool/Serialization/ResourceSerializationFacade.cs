using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;

namespace Shared.Tool.Serialization
{
    public class ResourceSerializationFacade : AbstractSerializationFacade
    {
        private readonly string _fileName;

        // todo make for many files
        public ResourceSerializationFacade(IFormatter dataFormatter, string fileName) : base(dataFormatter)
        {
            _fileName = fileName;
        }

        protected override IDictionary<string, Tuple<Type, byte[]>>? ReadSerializedData()
        {
            if (!File.Exists(_fileName))
            {
                return null;
            }

            using var reader = new ResourceReader(_fileName);
            IDictionaryEnumerator enumerator = reader.GetEnumerator();
            var nameToData = new Dictionary<string, Tuple<Type, byte[]>>();
            while (enumerator.MoveNext())
            {
                try
                {
                    string name = (string?) enumerator.Key ?? throw new InvalidOperationException("Key cannot be null");
                    reader.GetResourceData(name, out string typeName, out byte[] data);
                    nameToData.Add(name, Tuple.Create(Type.GetType(typeName), data));
                }
                catch (Exception e)
                {
                    throw new NotImplementedException("Handling such cases is not implemented", e);
                }
            }

            return nameToData;
        }

        protected override void WriteSerializedData(IDictionary<string, byte[]> nameToData)
        {
            using var writer = new ResourceWriter(_fileName);
            foreach (KeyValuePair<string, byte[]> nameAndData in nameToData)
            {
                byte[] data = nameAndData.Value;
                writer.AddResourceData(nameAndData.Key, data.GetType().FullName, data);
            }
        }
    }
}