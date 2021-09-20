using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorage.Models;

namespace UserStorage.Managers
{
    // todo refactor according to OOP
    public static class SerializationManager
    {
        private const string ResourceFileName = @".\Saved.resx";

        public static void Serialise(IList<PersonInfo> users)
        {
            using var memoryStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, users);
            byte[] bytes = memoryStream.ToArray();
            using var writer = new ResourceWriter(ResourceFileName);
            writer.AddResourceData("SavedPeople", nameof(IList<PersonInfo>), bytes);
        }

        public static IList<PersonInfo>? DeserializeUsers()
        {
            if (!File.Exists(ResourceFileName))
            {
                return null;
            }

            using var reader = new ResourceReader(ResourceFileName);
            reader.GetResourceData("SavedPeople", out string typeName, out byte[] bytes);

            if (typeName != nameof(IList<PersonInfo>))
            {
                return null;
            }

            using var byteStream = new MemoryStream(bytes);
            IFormatter formatter = new BinaryFormatter();
            return (IList<PersonInfo>) formatter.Deserialize(byteStream);
        }
    }
}