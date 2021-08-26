using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace IGIEditor
{
    class QZip
    {
        internal static void Create(string zipFileName, string extension = ".zip", bool deleteDir = false, string comment = null)
        {
            var zip = ZipStorer.Create(zipFileName + extension, comment);
            zip.AddDirectory(ZipStorer.Compression.Store, Directory.GetCurrentDirectory() + @"\" + zipFileName, String.Empty);
            zip.Close();
            if (deleteDir) QUtils.DeleteWholeDir(zipFileName);
        }

        internal static ZipStorer Open(string zipFileName, FileAccess fileAccess = FileAccess.Read)
        {
            var zip = ZipStorer.Open(zipFileName, fileAccess);
            return zip;
        }

        internal static void Add(ZipStorer zip, string pathName, string zipFileName, string comment = null)
        {
            zip.AddFile(ZipStorer.Compression.Deflate, pathName, zipFileName, comment);
        }

        internal static bool Extract(string zipFile, string extractPath)
        {
            var zip = ZipStorer.Open(zipFile, FileAccess.Read);
            var dir = zip.ReadCentralDir();
            var absFileName = Path.GetFileNameWithoutExtension(zipFile);

            // Look for the desired file.
            foreach (ZipStorer.ZipFileEntry entry in dir)
            {
                var fileName = Path.GetFileName(entry.FilenameInZip);

                // File found, extract it
                if (fileName.Contains("qvm"))
                    zip.ExtractFile(entry, extractPath + @"\" + absFileName + @"\ai\" + fileName);
                else
                    zip.ExtractFile(entry, extractPath + @"\" + absFileName + @"\" + fileName);
            }
            zip.Close();
            return false;
        }

        internal static bool FilesExist(string zipFile, List<string> filesList)
        {
            bool fileExist = false;
            foreach (var file in filesList)
            {
                fileExist = FileExist(zipFile, file);
            }
            return fileExist;
        }

        internal static bool FileExist(string zipFile, string fileName, string wildCard = null)
        {
            var zip = ZipStorer.Open(zipFile, FileAccess.Read);
            var dir = zip.ReadCentralDir();
            bool fileExist = false;

            // Look for the desired wildcard.
            if (!String.IsNullOrEmpty(wildCard))
            {
                foreach (var entry in dir)
                {
                    if (Path.GetFileName(entry.FilenameInZip).Contains(wildCard))
                    {
                        fileExist = true;
                        break;
                    }
                }
            }

            else
            {
                // Look for the desired file.
                foreach (var entry in dir)
                {

                    if (Path.GetFileName(entry.FilenameInZip) == fileName)
                    {
                        fileExist = true;
                        break;
                    }
                }
            }

            zip.Close();
            return fileExist;
        }

    }
}
