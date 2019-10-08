using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManipMacros
{
    public class FileManip
    {
        /// <summary>
        /// Creates a copy of a file, but changes the extention
        /// </summary>
        /// <param name="currentFilePath">Full relative or absolute path including file name and extention</param>
        /// <param name="newFileExtention">New file extention (without he ".")</param>
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
                if (file.Substring(file.LastIndexOf(".")+1,currentExtention.Length) == currentExtention)
                {
                    File.Copy(file, file.Substring(0, file.Length - currentExtention.Length) + newExtention);
                }
            }
               
        }
        
    }
}
