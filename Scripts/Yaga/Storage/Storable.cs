using System.IO;
using System.Linq;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

namespace Yaga.Storage
{
    public static class DataDumper
    {
        public static void Store(object data, string key)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + key + ".dat",
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
