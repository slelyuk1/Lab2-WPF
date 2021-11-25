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

        public ResourceSerializationFacade(IFormatter dataFormatter, string fileName) : base(dataFormatter)
        {
            _fileName = fileName;
        }

        protected override IDictionary<string, byte[]>? ReadSerializedData()
        {
            if (!File.Exists(_fileName))
            {
                return null;
            }

            using var reader = new ResourceReader(_fileName);
            IDictionaryEnumerator enumerator = reader.GetEnumerator();
            var nameToData = new Dictionary<string, byte[]>();
            while (enumerator.MoveNext())
            {
                string name = (string?) enumerator.Key ?? throw new InvalidOperationException("Key cannot be null");
                reader.GetResourceData(name, out string typeName, out byte[] data);
                // todo use typeName
                nameToData.Add(name, data);
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