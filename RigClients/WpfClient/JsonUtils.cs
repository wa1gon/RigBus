using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient
{
    public class JsonUtils
    {
        static private Object syncRoot = new Object();
        static public void Save<T>(String filename, T jsonObject)
        {
            lock (syncRoot)
            {
                try
                {
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    using (StreamWriter file = File.CreateText(filename))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, jsonObject);
                        file.Close();
                    }
                }
                catch (Exception e)
                {
                    // this should not be fatal
                    Console.WriteLine("Exception: " + e.Message);
                }
            }
        }
        static public T Restore<T>(String fileName)
        {
            lock (syncRoot)
            {
                if (File.Exists(fileName) == false)
                {
                    return default(T);
                }
                using (StreamReader file = File.OpenText(fileName))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    T lc = (T)serializer.Deserialize(file, typeof(T));
                    file.Close();
                    return lc;
                }
            }
        }
    }

}
