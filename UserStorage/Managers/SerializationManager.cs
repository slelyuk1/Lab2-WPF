using System;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorage.Models;
using UserStorage.Properties;

namespace UserStorage.Managers
{
    // todo refactor according to OOP
    public static class SerializationManager
    {
        private const string ResourceFileName = @".\Saved.resx";

        public static void Serialise(List<PersonInfo> users)
        {
            using var memoryStream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, users);
            byte[] bytes = memoryStream.ToArray();
            using var writer = new ResourceWriter(ResourceFileName);
            writer.AddResourceData("SavedPeople", nameof(List<PersonInfo>), bytes);
        }

        public static List<PersonInfo>? DeserializeUsers()
        {
            if (!File.Exists(ResourceFileName))
            {
                return null;
            }

            using var reader = new ResourceReader(ResourceFileName);
            reader.GetResourceData("SavedPeople", out string typeName, out byte[] bytes);

            if (Type.GetType(typeName) != typeof(List<PersonInfo>))
            {
                return null;
            }

            using var byteStream = new MemoryStream(bytes);
            IFormatter formatter = new BinaryFormatter();
            return (List<PersonInfo>) formatter.Deserialize(byteStream);
        }
    }
}