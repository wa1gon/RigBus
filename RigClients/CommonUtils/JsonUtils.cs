#region -- Copyright
/*
   Copyright {2014} {Darryl Wagoner DE WA1GON}

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
#endregion

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wa1gon.CommonUtils
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
