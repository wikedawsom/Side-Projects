﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileManipMacros
{
    public static class FileManip
    {
        /// <summary>
        /// Creates a copy of a file, but changes the extention (overrites existing files by default)
        /// </summary>
        /// <param name="currentFilePath">Full relative or absolute path including file name and extention</param>
        /// <param name="newFileExtention">New file extention (without he ".")</param>
        /// <param name="deleteOld">If true, delete old file after renaming</param>
        public static void FileExtensionRename(string currentFilePath, string newFileExtention, bool deleteOld, bool overrite = true)
        {
            //string [] directories = Directory.GetDirectories(currentFilePath.Substring(0,currentFilePath.LastIndexOf('\\')));
            File.Copy(currentFilePath, currentFilePath.Substring(0, currentFilePath.LastIndexOf("."))+newFileExtention, overrite);
            if (deleteOld)
            {
                File.Delete(currentFilePath);
            }
        }

        /// <summary>
        /// Creates a copy of every file in directory and children directories with matching extension type.
        /// Each copy has the new extension specified
        /// </summary>
        /// <param name="directory">Root directory to begin search in</param>
        /// <param name="currentExtention">Extension to change (without ".")</param>
        /// <param name="newExtention">New extension to be given to copied files (without ".")</param>
        /// <param name="deleteOld">If true, delete old file after renaming</param>
        public static void RecursiveExtensionRename(string directory, string currentExtention, string newExtention, bool deleteOld, bool overrite = true)
        {
            // Dives into each directory 1 by 1
            string[] directories = Directory.GetDirectories(directory);
            foreach (string subDirectory in directories)
            {
                RecursiveExtensionRename(subDirectory, currentExtention, newExtention, deleteOld, overrite);
            }

            // When there are no subdirectories in current directory, create renamed copies of all files with specified extension
            string[] files = Directory.GetFiles(directory);
            foreach (string file in files)
            {
                if (file.Substring(file.LastIndexOf(".")+1,currentExtention.Length) == currentExtention)
                {
                    File.Copy(file, file.Substring(0, file.Length - currentExtention.Length) + newExtention, overrite);
                    if (deleteOld)
                    {
                        File.Delete(file);
                    }
                }
            }
               
        }
        
    }
}
