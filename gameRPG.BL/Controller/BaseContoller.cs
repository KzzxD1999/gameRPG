using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace gameRPG.BL.Controller
{
    public class BaseContoller
    {
     
        public event EventHandler<string> MessagesEventSuccess;
        public event EventHandler<string> MessagesEventFail;
        protected void Save(string fileName, object obj)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(file, obj);
            }
        }
        protected T Load<T>(string fileName)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream file = new FileStream(fileName, FileMode.OpenOrCreate))
            {
                if(file.Length > 0 && binaryFormatter.Deserialize(file) is T items)
                {
                    return items;
                }
                else
                {
                    return default(T);
                }
            }

        }
        protected void Messages(string msg, bool isSuccess)
        {
            if (isSuccess)
            {
                MessagesEventSuccess?.Invoke(msg, null);
            }
            else
            {
                MessagesEventFail?.Invoke(msg, null);
        
            }
        }
    }
}
