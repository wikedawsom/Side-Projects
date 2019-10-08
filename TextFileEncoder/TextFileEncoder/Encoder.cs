using System;
using System.IO;

namespace TextFileEncoder
{
    public static class FileEncoder
    {
        /// <summary>
        /// Reads a file as an array of bytes, then adds the offset to each byte
        /// </summary>
        /// <param name="path">The fully qualified file name</param>
        /// <param name="offset">Optional byte value offset (default 1)</param>
        public static void EncryptFileStandard(string path, byte offset = 1)
        {
            byte[] fileContents = File.ReadAllBytes(path);

            for (int i = 0; i < fileContents.Length; i++)
            {
                fileContents[i] += offset;
            }
            File.WriteAllBytes(path.Substring(0,path.LastIndexOf(".")) + ".enc", fileContents);
        }

        /// <summary>
        /// Reads a file that was encoded using the EncodeFileStandard() method
        /// as an array of bytes, then subtracts the specified offset from each byte
        /// </summary>
        /// <param name="path">The fully qualified file name</param>
        /// <param name="offset">Optional byte value offset (default 1)</param>
        public static void DecryptFileStandard(string path, byte offset = 1)
        {
            byte[] fileContents = File.ReadAllBytes(path);

            for (int i = 0; i < fileContents.Length; i++)
            {
                fileContents[i] -= offset;
            }
            File.WriteAllBytes(path.Substring(0, path.LastIndexOf(".")) + "-dec.txt", fileContents);
        }

        /// <summary>
        /// Same principle as EncryptFileStandard(), except only reading the file
        /// one character at a time to avoid problems caused by too much RAM usage
        /// </summary>
        /// <param name="path">The fully qualified file name</param>
        /// <param name="offset">Specified value offset</param>
        public static void EncryptFileStream(string path, int offset = 1)
        {
            StreamWriter sw = new StreamWriter(path.Substring(0, path.LastIndexOf(".")) + ".enc");
            
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    int character = sr.Read();
                    character += offset;
                    sw.Write((char)character);
                }
            }
            sw.Close();
        }

        /// <summary>
        /// Same principle as DecryptFileStandard(), except only reading the file
        /// one character at a time to avoid problems caused by too much RAM usage
        /// </summary>
        /// <param name="path">The fully qualified file name</param>
        /// <param name="offset">Specified value offset</param>
        public static void DecryptFileStream(string path, int offset = 1)
        {
            StreamWriter sw = new StreamWriter(path.Substring(0, path.LastIndexOf(".")) + "-dec.txt");

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    int character = sr.Read();
                    character -= offset;
                    sw.Write((char)character);
                }
            }
            sw.Close();
        }
    }


}
