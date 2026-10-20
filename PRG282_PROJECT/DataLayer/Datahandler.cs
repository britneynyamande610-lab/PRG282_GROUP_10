using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PRG282_PROJECT
{
    class Datahandler
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllLines(filePath);
        }

        public void AppendAllText(string filePath, string content)
        {
            File.AppendAllText(filePath, content, new UTF8Encoding(false));
        }

        public void WriteAllText(string filePath, string content)
        {
            File.WriteAllText(filePath, content, new UTF8Encoding(false));
        }

        public void WriteAllLines(string filePath, IEnumerable<string> lines)
        {
            File.WriteAllLines(filePath, lines, new UTF8Encoding(false));
        }
    }
}
