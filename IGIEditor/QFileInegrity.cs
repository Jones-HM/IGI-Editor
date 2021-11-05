using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace IGIEditor
{
    class FileInegrity
    {
        static string qChecksFile = QUtils.igiEditorTmpPath + @"\QChecks.dat", qChecksFileData = null;

        public static void RunFileInegrityCheck(string processName = null, List<string> gameDirs = null)
        {
            bool isQFilesValid = CheckDirInegrity(gameDirs, true, processName);
            IGIEditorUI.editorRef.Enabled = isQFilesValid;
        }

        public static bool CheckFileInegrity(string qfilePath, bool showError = true)
        {
            var parentDir = Directory.GetParent(qfilePath).ToString();
            var filePath = parentDir.Substring(parentDir.LastIndexOf(@"\") + 1) + @"\" + Path.GetFileName(qfilePath);

            if (!File.Exists(qChecksFile)) GenerateFileHash(qfilePath);

            if (String.IsNullOrEmpty(qChecksFileData))
                qChecksFileData = QCryptor.Decrypt(qChecksFile);

            string md5Hash = GenerateMD5(qfilePath);
            if (!File.Exists(qChecksFile) || qChecksFileData.Length < 5)
                throw new Exception("FileIntegrity hashes generation error with length (0x4C80000)");

            var fileHashesData = qChecksFileData.Split('\n');
            bool fileMatch = false;


            if (!qChecksFileData.Contains(qfilePath))
            {
                QUtils.ShowError("File '" + filePath + "' doesn't exist in QChecks sum");
                return fileMatch;
            }

            string fileName, fileHash = null;
            foreach (var hashDataLine in fileHashesData)
            {
                if (hashDataLine.Length < 1) continue;
                var hashData = hashDataLine.Split('=');

                fileName = hashData[0].Trim();
                fileHash = hashData[1].Trim();

                if (qfilePath == fileName)
                {
                    fileMatch = (fileHash == md5Hash);
                    break;
                }
            }

            if (!fileMatch && showError)
            {
                QUtils.ShowError("File '" + filePath + "' has been modified externally");
                QUtils.AddLog("CheckFileInegrity() File '" + filePath + "' has been modified externally");
                QUtils.AddLog("CheckFileInegrity() Hash1: " + md5Hash + "\tHash2: " + fileHash);
            }
            return fileMatch;
        }

        public static bool CheckDirInegrity(List<string> dirNames, bool showError = true, string exclude = "")
        {
            if (!File.Exists(qChecksFile))
                GenerateDirHashes(dirNames);

            bool checkInegrity = true;
            foreach (string dirName in dirNames)
            {
                string[] allFiles = Directory.GetFiles(dirName, ".", SearchOption.AllDirectories);

                foreach (var files in allFiles)
                {
                    if (!String.IsNullOrEmpty(exclude))
                        if (Path.GetFileName(files) == exclude) continue;

                    checkInegrity = CheckFileInegrity(files, showError);
                    if (!checkInegrity) return false;
                }
            }
            return checkInegrity;
        }

        private static void GenerateFileHash(string fileName, bool append = false)
        {
            var fileHashes = GenerateMD5(fileName);
            var fileHashesData = fileName + " = " + fileHashes + "\n";
            if (append) File.AppendAllText(qChecksFile, fileHashesData);
            else File.WriteAllText(qChecksFile, fileHashesData);
        }

        public static void GenerateDirHashes(List<string> dirNames)
        {
            File.Delete(qChecksFile);
            foreach (string dirName in dirNames)
            {
                string[] allFiles = Directory.GetFiles(dirName, ".", SearchOption.AllDirectories);

                foreach (var files in allFiles)
                {
                    GenerateFileHash(files, true);
                }
            }
            Thread.Sleep(1500);
            QCryptor.Encrypt(qChecksFile);
        }

        private static string GenerateMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }
    }
}
