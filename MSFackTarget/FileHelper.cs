using System;
using System.IO;

namespace MSFackTarget
{
    public class FileHelper
    {
        //Virtual Method
        public virtual bool IsNull(string fileName)
        {
            return !File.Exists(fileName);
        }
        
        //Non Virtual Method
        public bool IsEmpty(string fileName)
        {
            return String.IsNullOrEmpty(File.ReadAllText(fileName));
        }

        //Static Method
        public static bool IsNullOrEmpty(string fileName)
        {
            return !File.Exists(fileName) || String.IsNullOrEmpty(File.ReadAllText(fileName));
        }

    }
}