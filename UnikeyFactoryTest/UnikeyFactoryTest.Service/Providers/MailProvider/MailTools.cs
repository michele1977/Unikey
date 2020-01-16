using System;
using System.IO;

namespace UnikeyFactoryTest.Service.Providers.MailProvider
{
    public class MailTools : IMailTools
    {
        public string ReadFile(string path)
        {
            if (!File.Exists(path))
                throw new Exception("Path not valids");
            StreamReader myReader = new StreamReader(path);
            var readed = myReader.ReadToEnd();
            return readed;
        }
    }
}
