using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UserStorage.Models;

namespace UserStorage.Managers
{
    public static class SerializationManager
    {
        public static void Serialise(string path, LinkedList<Person> users)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                foreach (var user in users)
                {
                    formatter.Serialize(fs, user);
                }
            }
        }

        public static LinkedList<Person> DeserializeUsers(string path)
        {
            LinkedList<Person> users = new LinkedList<Person>();
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                while (true)
                {
                    try
                    {
                        users.AddLast((Person) formatter.Deserialize(fs));
                    } //!!!!
                    catch (SerializationException)
                    {
                        break;
                    }
                }
            }

            return users;
        }

        public static LinkedList<Person> ReadUsersFromTxt(string path, int usersCount)
        {
            LinkedList<Person> users = new LinkedList<Person>();

            LinkedList<string> names = new LinkedList<string>();
            LinkedList<string> emails = new LinkedList<string>();

            LinkedList<DateTime> birthdays = new LinkedList<DateTime>();
            using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open)))
            {
                for (int fase = 0; fase < 3; fase++)
                {
                    int tempCount = usersCount;
                    switch (fase)
                    {
                        case 0:
                            while (tempCount-- > 0)
                            {
                                names.AddLast(reader.ReadLine());
                            }

                            break;
                        case 1:
                            while (tempCount-- > 0)
                            {
                                emails.AddLast(reader.ReadLine());
                            }

                            break;
                        case 2:
                            while (tempCount-- > 0)
                            {
                                string[] args = reader.ReadLine().Split('/');
                                DateTime birthday = new DateTime(int.Parse(args[2]), int.Parse(args[1]),
                                    int.Parse(args[0]));
                                birthdays.AddLast(birthday);
                            }

                            break;
                        default:
                            throw new ArgumentException("Something went wrong !");
                    }
                }

                using (var eN = names.GetEnumerator())
                using (var eE = emails.GetEnumerator())
                using (var eB = birthdays.GetEnumerator())
                {
                    while (eN.MoveNext() && eE.MoveNext() && eB.MoveNext())
                    {
                        var args = eN.Current.Split();
                        var email = eE.Current;
                        var birthDate = eB.Current;

                        var user = new Person(args[0], args[1], email, birthDate);
                        users.AddLast(user);
                    }
                }
            }

            return users;
        }
    }
}