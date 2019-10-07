using System;
using System.IO;

namespace TextFileEncoder
{
    public static class FileEncoder
    {
        public static void Main(string[] args)
        { }
        public static void EncodeFileStandard(string path)
        {
            byte[] fileContents = File.ReadAllBytes(path);

            for (int i = 0; i < fileContents.Length; i++)
            {
                fileContents[i]++;
            }
            File.WriteAllBytes(path.Substring(0,path.LastIndexOf(".")) + ".encoded", fileContents);
        }

        public static void DecodeFileStandard(string path)
        {
            byte[] fileContents = File.ReadAllBytes(path);

            for (int i = 0; i < fileContents.Length; i++)
            {
                fileContents[i]--;
            }
            File.WriteAllBytes(path.Substring(0, path.LastIndexOf(".") + 1) + "-de.txt", fileContents);
        }
    }
}
