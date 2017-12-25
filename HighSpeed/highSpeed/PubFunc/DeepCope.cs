using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace highSpeed.PubFunc
{
    public class DeepCope
    {
        public object DeserializeModel(byte[] msg)
        {
            //string str = Gzip.Decompress(msg);
            string str = Encoding.UTF8.GetString(msg);
            NetDataContractSerializer s = new NetDataContractSerializer();
            MemoryStream ms = new MemoryStream();
            StreamWriter sw = new StreamWriter(ms);
            sw.Write(str);
            sw.Flush();
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            Object obj = s.ReadObject(ms);
            sw.Close();

            return obj;
        }

        public byte[] SerializeModel(object model)
        {
            NetDataContractSerializer s = new NetDataContractSerializer();
            MemoryStream ms = new MemoryStream();
            StreamReader sr = new StreamReader(ms);
            s.WriteObject(ms, model);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            string ret = sr.ReadToEnd();
            sr.Close();
            //byte[] retb = Gzip.Compress(ret);
            byte[] retb = Encoding.UTF8.GetBytes(ret);
            return retb;
        }

        public object DeepCopy(object from)
        {
            var bytes = this.SerializeModel(from);
            var model = this.DeserializeModel(bytes);
            return model;
        }
    }
}
