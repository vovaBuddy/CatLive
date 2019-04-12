using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yaga.Storage
{
    public class StorableData <T> where T : new()
    {            

        private string key;
        public T content;
        static string path;

        private StorableData() { }
        public StorableData(string k)
        {
            key = k;
            Load();
            path = Application.persistentDataPath;
        }

        public void Store()
        {
            DataDumper.Store(content, key);
        }

        public void Load()
        {
            object obj = DataDumper.Load(key);
            content = (T)obj;
            if (content == null) content = new T();
        }


        private static class DataDumper
        {
            public static void Store(object data, string key)
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path + "/" + key + ".dat",
                                            FileMode.OpenOrCreate);
                bf.Serialize(file, data);
                file.Close();
            }

            public static object Load(string key)
            {
                if (File.Exists(Application.persistentDataPath + "/" + key + ".dat"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Open(Application.persistentDataPath + "/" + key + ".dat",
                                                FileMode.Open);
                    var data = bf.Deserialize(file);
                    file.Close();

                    return data;
                }

                else return null;
            }
        }
    }
}
