using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileExtentionRenamer
{
    public class FileManip
    {
        public void FileExtentionRename(string currentFilePath, string newFileExtention)
        {
            //string [] directories = Directory.GetDirectories(currentFilePath.Substring(0,currentFilePath.LastIndexOf('\\')));
            File.Copy(currentFilePath, currentFilePath.Substring(0, currentFilePath.LastIndexOf("."))+newFileExtention);

        }

        public void RecursiveExtentionRename(string directory, string currentExtention, string newExtention)
        {
            string[] directories = Directory.GetDirectories(directory);
            string[] files = Directory.GetFiles(directory);
            foreach (string subDirectory in directories)
            {
                RecursiveExtentionRename(subDirectory, currentExtention, newExtention);
            }
            foreach(string file in files)
            {
                if (file.Substring(file.LastIndexOf("."),currentExtention.Length) == currentExtention)
                {
                    File.Copy(file, file.Substring(0, file.Length - currentExtention.Length) + newExtention);
                }
            }
               
        }
        
    }
}
