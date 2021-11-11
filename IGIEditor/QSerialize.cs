using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace IGIEditor
{
    class QSerialize
    {
        static DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        internal static int encryptCount = 0, decryptCount = 0;

        internal static void Encrypt(string fileName, object fileData, bool supress_err = false)
        {
            try
            {
                string hexKey = BitConverter.ToString(des.Key);
                string hexIV = BitConverter.ToString(des.IV);
                string privKey = Encoding.UTF8.GetString(des.Key);
                using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                using (var cryptoStream = new CryptoStream(fs, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(cryptoStream, fileData);
                }

                //Crypt the Crypto.
                AppendAllBytes(fileName, des.Key);
                AppendAllBytes(fileName, des.IV);
                encryptCount++;
            }
            catch (Exception ex)
            {
                if (!supress_err)
                {
                    if(ex.Message.Contains("Bad Data"))
                        throw new BadImageFormatException("Exception occured : Input file '" + fileName + "' is not serialized (Error:0x4DCCCCCC");
                    else
                    throw new Exception("Exception occured : " + ex.Message);
                }
                else
                    encryptCount--;
            }
        }

        internal static byte[] Decrypt(string fileName, bool suppressErr = false)
        {
            byte[] deserializedByte = null;
            try
            {
                des.Key = GetRgbKey(fileName);
                des.IV = GetIVKey(fileName);
                ResolveKeyData(fileName);

                string hexIV = BitConverter.ToString(des.IV);
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                using (var cryptoStream = new CryptoStream(fs, des.CreateDecryptor(), CryptoStreamMode.Read))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    deserializedByte = (byte[])formatter.Deserialize(cryptoStream);
                }
                decryptCount++;
            }
            catch (Exception ex)
            {
                if (!suppressErr)
                {
                    if (ex.Message.Contains("Bad Data"))
                        throw new BadImageFormatException("Exception occured : Input file '" + fileName + "' is not serialized (Error:0x4DCCCCCC");
                    else
                        throw new Exception("Exception occured : " + ex.Message);
                }
                else
                    decryptCount--;
            }
            finally
            {
                AppendAllBytes(fileName, des.Key);
                AppendAllBytes(fileName, des.IV);
            }
            return deserializedByte;
        }

        public static void WriteToBinaryFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            using (Stream stream = File.Open(filePath, append ? FileMode.Append : FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, objectToWrite);
            }
        }

        public static T ReadFromBinaryFile<T>(string filePath)
        {
            using (Stream stream = File.Open(filePath, FileMode.Open))
            {
                var binaryFormatter = new BinaryFormatter();
                return (T)binaryFormatter.Deserialize(stream);
            }
        }

        private static byte[] GetRgbKey(string fileName)
        {
            int fileLen = File.ReadAllBytes(fileName).Length;
            byte[] privKey = File.ReadAllBytes(fileName).Skip(fileLen - 16).Take(8).ToArray();
            string hexKey = BitConverter.ToString(privKey);
            return privKey;
        }

        private static byte[] GetIVKey(string fileName)
        {
            int fileLen = File.ReadAllBytes(fileName).Length;
            byte[] privKey = File.ReadAllBytes(fileName).Skip(fileLen - 8).Take(8).ToArray();
            string hexKey = BitConverter.ToString(privKey);
            return privKey;
        }

        private static void ResolveKeyData(string fileName)
        {
            int fileLen = File.ReadAllBytes(fileName).Length;
            byte[] newData = File.ReadAllBytes(fileName).Skip(0).Take(fileLen - 16).ToArray();
            File.WriteAllBytes(fileName, newData);
        }

        private static void AppendAllBytes(string path, byte[] bytes)
        {
            using (var stream = new FileStream(path, FileMode.Append))
            {
                stream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}