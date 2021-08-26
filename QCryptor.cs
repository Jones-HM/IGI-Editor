/*QCryptor - is custom encryptor for QFiles of Project IGI game, all Q-Files are supported including .qsc .qvm
Dated: -8/22/2021
*/
using System.IO;
using System.Text;

namespace IGIEditor
{
    class QCryptor
    {
        static public void Encrypt(string inFile)
        {
            Encrypt(inFile, null);
        }

        static public void Encrypt(string inFile, string outFile)
        {
            var fileData = File.ReadAllBytes(inFile);
            string eFile = string.IsNullOrEmpty(outFile) ? inFile : outFile;

            QSerializeLib.WriteSerializeData(eFile, fileData);
            QSerialize.Encrypt(eFile, fileData);
        }

        static public string Decrypt(string fileName, bool __ignore__ = true)
        {
            var data = QSerialize.Decrypt(fileName);
            if (data != null)
            {
                return Encoding.UTF8.GetString(data);
            }
            return null;
        }

        static public void Decrypt(string inFile, string outFile)
        {
            string eFile = string.IsNullOrEmpty(outFile) ? inFile : outFile;

            var data = QSerialize.Decrypt(inFile, true);
            if (data != null)
                File.WriteAllBytes(eFile, data);
        }


        static public void EncryptDirectory(string dirName)
        {
            string[] allfiles = Directory.GetFiles(dirName, ".",
                              SearchOption.AllDirectories);
            var allFilesCount = allfiles.Length;

            foreach (var file in allfiles)
                Encrypt(file, null);

            QSerialize.encryptCount = QSerialize.encryptCount < 0 ? 0 : QSerialize.encryptCount;
            System.Console.WriteLine("Encrypted " + QSerialize.encryptCount + " files out of " + allFilesCount + " files");
        }

        static public void DecryptDirectory(string dirName)
        {
            string[] allfiles = Directory.GetFiles(dirName, ".",
                                         SearchOption.AllDirectories);
            var allFilesCount = allfiles.Length;

            foreach (var file in allfiles)
                Decrypt(file, null);

            QSerialize.decryptCount = QSerialize.decryptCount < 0 ? 0 : QSerialize.decryptCount;
            System.Console.WriteLine("Decrypted " + QSerialize.decryptCount + " files out of " + allFilesCount + " files");

        }
    }
}
