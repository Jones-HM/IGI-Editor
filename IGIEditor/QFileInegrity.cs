using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IGIEditor
{
    class FileIntegrity
    {
        static string qChecksFile = Path.Combine(QUtils.igiEditorQEdPath, "QChecks.dat");
        static string qChecksFileData = null;

        public static void RunFileIntegrityCheck(string processName = null, List<string> gameDirs = null)
        {
            var excludeList = new HashSet<string> { QUtils.customPatrolPathQEd, QUtils.customScriptPathQEd };
            bool qFilesValid = CheckDirIntegrity(gameDirs, excludeList, false);
            IGIEditorUI.editorRef.Enabled = qFilesValid;
        }

        public static bool CheckFileIntegrity(string qfilePath, bool showError = true)
        {
            var parentDir = Path.GetDirectoryName(qfilePath);
            var filePath = Path.Combine(parentDir.Substring(parentDir.LastIndexOf(Path.DirectorySeparatorChar) + 1), Path.GetFileName(qfilePath));

            if (!File.Exists(qChecksFile))
            {
                GenerateFileHash(qfilePath);
            }

            if (String.IsNullOrEmpty(qChecksFileData))
            {
                qChecksFileData = QCryptor.Decrypt(qChecksFile);
            }

            string md5Hash = GenerateMD5(qfilePath);
            if (!File.Exists(qChecksFile) || qChecksFileData.Length < 5)
            {
                if (showError)
                {
                    QUtils.ShowError("File integrity hashes generation error with length (0x4C80000)");
                }
                return false;
            }

            var fileHashesData = qChecksFileData.Split('\n');
            bool fileMatch = false;

            if (!qChecksFileData.Contains(qfilePath))
            {
                if (showError)
                {
                    QUtils.ShowError($"File '{filePath}' doesn't exist in checksum");
                }
                return false;
            }

            foreach (var hashDataLine in fileHashesData)
            {
                if (hashDataLine.Length < 1) continue;
                var hashData = hashDataLine.Split('=');

                var fileName = hashData[0].Trim();
                var fileHash = hashData[1].Trim();

                if (qfilePath == fileName)
                {
                    fileMatch = (fileHash == md5Hash);
                    break;
                }
            }

            if (!fileMatch && showError)
            {
                QUtils.ShowError($"File '{filePath}' has been modified externally");
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, $"File '{filePath}' has been modified externally");
                QUtils.AddLog(MethodBase.GetCurrentMethod().Name, $"Hash1: {md5Hash}");
            }
            return fileMatch;
        }

        public static bool CheckDirIntegrity(List<string> dirNames, HashSet<string> excludeList, bool showError = true)
        {
            if (!File.Exists(qChecksFile))
            {
                var status = GenerateDirHashes(dirNames);
            }

            bool checkIntegrity = true;
            foreach (string dirName in dirNames)
            {
                string[] allFiles = Directory.GetFiles(dirName, ".", SearchOption.AllDirectories);

                foreach (var file in allFiles)
                {
                    if (excludeList.Contains(Path.GetFileName(file)))
                    {
                        QUtils.AddLog(MethodBase.GetCurrentMethod().Name, $"Exclude file '{Path.GetFileName(file)}', pathFile '{file}'");
                        continue;
                    }

                    if (!CheckFileIntegrity(file, showError))
                    {
                        checkIntegrity = false;
                        if (!showError)
                        {
                            break;
                        }
                    }
                }
                if (!checkIntegrity)
                {
                    break;
                }
            }
            return checkIntegrity;
        }

        private static void GenerateFileHash(string fileName, bool append = false)
        {
            var fileHashes = GenerateMD5(fileName);
            var fileHashesData = $"{fileName} = {fileHashes}\n";
            if (append)
            {
                File.AppendAllText(qChecksFile, fileHashesData);
            }
            else
            {
                File.WriteAllText(qChecksFile, fileHashesData);
            }
        }

        public static async Task GenerateDirHashes(List<string> dirNames)
        {
            QUtils.FileIODelete(qChecksFile);
            foreach (string dirName in dirNames)
            {
                string[] allFiles = Directory.GetFiles(dirName, ".", SearchOption.AllDirectories);

                foreach (var file in allFiles)
                {
                    await Task.Run(() => GenerateFileHash(file, true));
                }
            }
            await Task.Run(() =>
            {
                QUtils.Sleep(1.5f);
                QCryptor.Encrypt(qChecksFile);
            });
        }

        private static string GenerateMD5(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    var hash = md5.ComputeHash(stream);
                    var stringBuilder = new StringBuilder();
                    for (int i = 0; i < hash.Length; i++)
                    {
                        stringBuilder.Append(hash[i].ToString("x2"));
                    }
                    return stringBuilder.ToString();
                }
            }
        }
    }
}
